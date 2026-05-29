---
name: implement-cs
description: C#コードの新規実装・追加実装を行う。C#の実装作業を依頼されたときに使用する
---

# C# 実装

C# コードの新規実装・追加実装を規約に従って行う。

## 参照

### コーディングスタイル

- [構文スタイル](references/coding-styles/cs-style-syntax.md)
- [命名規則](references/coding-styles/cs-style-naming.md)
- [ディレクトリ命名](references/coding-styles/cs-style-naming-dir.md)
- [クラス接尾辞](references/coding-styles/cs-style-naming-suffixes.md)
- [ブレーススタイル](references/coding-styles/cs-style-braces.md)
- [空行](references/coding-styles/cs-style-blank-lines.md)
- [改行](references/coding-styles/cs-style-line-breaks.md)
- [インデント](references/coding-styles/cs-style-tabs-indents.md)
- [using/namespace](references/coding-styles/cs-style-using.md)
- [コレクション型](references/coding-styles/cs-style-collections.md)
- [nullチェック](references/coding-styles/cs-style-null-checking.md)
- [ガード節](references/coding-styles/cs-style-guard-clauses.md)
- [エラーハンドリング](references/coding-styles/cs-style-error-handling.md)
- [ファイルレイアウト](references/coding-styles/cs-style-file-layout.md)

### Unity 固有スタイル

- [Unity エラーハンドリング](references/coding-styles/csharp-unity/cs-style-unity-error-handling.md)
- [SerializedField](references/coding-styles/csharp-unity/cs-style-unity-serialized-field.md)
- [EditorWindow](references/coding-styles/csharp-unity/cs-style-unity-editor-window.md)
- [UiAnimatorパターン](references/coding-styles/csharp-unity/cs-style-unity-ui-animator.md)
- [R3](references/coding-styles/csharp-unity/libs/cs-style-unity-lib-r3.md)
- [UniTask](references/coding-styles/csharp-unity/libs/cs-style-unity-lib-unitask.md)

### アーキテクチャ

- [レイヤー構成](references/architecture/arch-ca-layers.md)
- [アーキテクチャ命名](references/architecture/arch-ca-naming.md)
- [DIコンテナ](references/architecture/arch-di-container.md)
- [リアクティブ](references/architecture/arch-rx-reactive.md)
- [DDDドメイン](references/architecture/arch-ddd-domain.md)

### 実装後検証

- [実装後検証（uloop）](references/uloop-post-impl-verification.md)

## 実行原則

- **スキル開始時に `[skill-started: implement-cs]` を出力する。** Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの開始を検知するために使用する
- **各手順間で停止しない。** スキル完了（完了報告の提示）まで一気に実行する。ユーザーへの確認が必要な場面以外で手順を区切って待機しないこと
- **新規 .cs ファイル作成後に uloop で Prefab 操作（AddComponent 等）を行う場合、事前に `uloop compile --force-recompile true --wait-for-domain-reload true` を実行する。** Domain Reload が完了するまで新しい型は uloop から認識されない。.meta ファイルもコンパイル時に自動生成される
- **インターフェースメソッドを削除・リネームする場合、事前に Grep で全参照箇所（実装クラス・呼び出し元・デバッグコード）を特定し、漏れなく対応する。** Grep 対象はインターフェース名ではなくメソッド名で検索すること
- **依頼された方法で完了できないと判断した場合、代替手段を即実行せずユーザーに確認する。** 依頼内容と逆方向の回避策（例: 「AをBに合わせて」→ できないから B を A に合わせる）を無断で実行しない

## 手順

- [ ] 1. 実装対象の仕様・要件を確認する
- [ ] 2. 上記の参照ファイルをすべて読み込む
- [ ] 3. 関連する既存コードを読み込み、設計・パターンを把握する
- [ ] 4. コードを実装する
- [ ] 5. `/comment-cs` でコメントを付与・修正する
- [ ] 6. `/uloop-compile` でコンパイルチェックを実施する（PlayMode 中でスキップされた場合は `uloop control-play-mode --action Stop` で停止してから再実行）
- [ ] 7. 実装後検証を実施する（該当項目をAIが判断し自発的に実行）
- [ ] 8. `/review-cs` でレビューし、指摘があれば修正する（最大3回）
- [ ] 9. セルフレビューを実施する
- [ ] 10. 完了報告を作成し、ユーザーに提示する

## セルフレビュー

- [ ] `/uloop-compile` でコンパイルエラー0件を確認したか
- [ ] `/review-cs` を実行し、指摘に対応したか
- [ ] 解消しきれない `/review-cs` の指摘がある場合、ユーザーに報告したか

## 完了報告

[完了報告テンプレート](templates/completion-report.md) に従い、報告書を作成しユーザーに提示する。

## スキルマーカー

開始時点で以下のマーカーを出力する。マーカーは Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの開始を検知するために使用される。

```
[skill-started: implement-cs]
```

完了報告の提示が完了した時点で、以下のマーカーを出力する。マーカーはフック `guard-cs-direct-edit` と Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの終了を検知するために使用される。

```
[skill-completed: implement-cs]
```
