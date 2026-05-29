/**
 * Codex PreToolUse hook: apply_patch/Edit/Write で .cs ファイルを直接編集しようとした場合にブロックする。
 *
 * C# コードの編集は implement-cs 系スキル経由で行うルールを補助的に強制する。
 * Codex の hooks は完全な強制境界ではないため、強制力の底上げ用ガードとして扱う。
 *
 * 判定ロジック:
 *   - apply_patch の場合は patch 文字列から対象ファイルを抽出する
 *   - Edit/Write 互換入力の場合は tool_input.file_path を見る
 *   - transcript 末尾から [skill-started: <skill>] と [skill-completed: <skill>] を対応付け、
 *     未完了の許可スキルがあれば編集を許可する
 *
 * Exit codes:
 *   0 - 許可（対象外、または許可スキルコンテキスト内）
 *   2 - ブロック（.cs ファイルへの直接編集を検出）
 */

import { readFileSync } from "node:fs";

/** .cs 編集を許可するスキル名 */
const ALLOWED_SKILLS = [
  "implement-cs",
  "implement-cs-prototype",
  "comment-cs",
  "uloop-execute-dynamic-code",
];

function readStdin() {
  return new Promise((resolve) => {
    let input = "";
    process.stdin.setEncoding("utf8");
    process.stdin.on("data", (chunk) => (input += chunk));
    process.stdin.on("end", () => resolve(input));
  });
}

/**
 * Codex/Claude の transcript エントリからモデル可視テキストを抽出する。
 */
function extractTextFromEntry(entry) {
  const candidates = [
    entry?.message,
    entry?.payload,
    entry?.payload?.message,
  ];

  return candidates.map(extractText).filter(Boolean).join("\n");
}

function extractText(value) {
  if (!value) return "";
  if (typeof value === "string") return value;

  const parts = [];

  if (typeof value.text === "string") parts.push(value.text);
  if (typeof value.output_text === "string") parts.push(value.output_text);
  if (typeof value.content === "string") parts.push(value.content);

  if (Array.isArray(value.content)) {
    for (const item of value.content) {
      const text = extractText(item);
      if (text) parts.push(text);
    }
  }

  if (value.type === "tool_result") {
    const text = extractText(value.content);
    if (text) parts.push(text);
  }

  return parts.join("\n");
}

function findStartedSkills(text) {
  return ALLOWED_SKILLS.filter(
    (skill) =>
      text.includes(`[skill-started: ${skill}]`) ||
      text.includes(`[skill-start: ${skill}]`) ||
      text.includes(`Launching skill: ${skill}`) ||
      text.includes(`skills/${skill}`) ||
      text.includes(`skills\\${skill}`)
  );
}

function findCompletedSkills(text) {
  return ALLOWED_SKILLS.filter((skill) =>
    text.includes(`[skill-completed: ${skill}]`)
  );
}

/**
 * transcript を末尾から遡り、未完了の許可スキル内かを判定する。
 */
function isInsideAllowedSkill(transcriptPath) {
  if (!transcriptPath) return false;

  try {
    const content = readFileSync(transcriptPath, "utf8");
    const lines = content.trim().split("\n").filter(Boolean);

    const unmatchedCompletions = Object.fromEntries(
      ALLOWED_SKILLS.map((skill) => [skill, 0])
    );

    for (let i = lines.length - 1; i >= 0; i--) {
      let entry;
      try {
        entry = JSON.parse(lines[i]);
      } catch {
        continue;
      }

      const text = extractTextFromEntry(entry);
      if (!text) continue;

      for (const skill of findCompletedSkills(text)) {
        unmatchedCompletions[skill]++;
      }

      for (const skill of findStartedSkills(text)) {
        if (unmatchedCompletions[skill] > 0) {
          unmatchedCompletions[skill]--;
        } else {
          return true;
        }
      }
    }
  } catch {
    // transcript 読み取り失敗時はブロック側に倒す。
  }

  return false;
}

function isCsPath(filePath) {
  return normalizePath(filePath).toLowerCase().endsWith(".cs");
}

function normalizePath(filePath) {
  return String(filePath || "").trim().replace(/\\/g, "/");
}

function getEditedFiles(toolName, toolInput) {
  if (toolInput?.file_path) {
    return [toolInput.file_path];
  }

  if (toolName !== "apply_patch") {
    return [];
  }

  const patch = String(toolInput?.command || toolInput?.patch || "");
  const files = [];
  const patterns = [
    /^\*\*\* (?:Add|Update|Delete) File: (.+)$/gm,
    /^\*\*\* Move to: (.+)$/gm,
  ];

  for (const pattern of patterns) {
    let match;
    while ((match = pattern.exec(patch)) !== null) {
      files.push(match[1].trim());
    }
  }

  return files;
}

function deny(filePaths) {
  const targets = filePaths.map((path) => `  - ${path}`).join("\n");
  process.stderr.write(
    [
      "[スキル使用ガード] .cs ファイルの直接編集をブロックしました。",
      "",
      "対象ファイル:",
      targets,
      "",
      "C# コードの編集は以下のいずれかのスキル経由で行ってください。",
      "",
      "  - implement-cs           本採用品質の実装（スタイル・コメント・レビュー含む）",
      "  - implement-cs-prototype 動作優先の軽量プロト実装（スタイル・コメント・レビュー省略）",
      "  - comment-cs             XML ドキュメントコメントの付与・修正",
      "",
      "Codex hook は補助ガードです。スキル開始時に [skill-started: <skill-name>]、",
      "完了時に [skill-completed: <skill-name>] が transcript に出ている場合は許可します。",
      "",
    ].join("\n")
  );
  process.exit(2);
}

async function main() {
  const input = await readStdin();
  const data = JSON.parse(input || "{}");
  const toolName = data.tool_name || "";

  if (!["apply_patch", "Edit", "Write"].includes(toolName)) {
    process.exit(0);
  }

  const toolInput = data.tool_input || {};
  const editedFiles = getEditedFiles(toolName, toolInput);
  const csFiles = editedFiles.filter(isCsPath);

  if (csFiles.length === 0) {
    process.exit(0);
  }

  if (isInsideAllowedSkill(data.transcript_path || "")) {
    process.exit(0);
  }

  deny(csFiles.map(normalizePath));
}

main().catch((error) => {
  process.stderr.write(`[スキル使用ガード] hook 実行に失敗しました: ${error.message}\n`);
  process.exit(2);
});
