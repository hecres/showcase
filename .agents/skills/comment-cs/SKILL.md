---
name: comment-cs
description: C#コードにXMLドキュメントコメントを付与・修正する。コメント追加・コメント整備を依頼されたときに使用する
---

# C# コメント付与・修正

C# コードに XML ドキュメントコメントを規約に従って付与・修正する。
未コメント要素への新規付与と、既存コメントの規約違反修正の両方を行う。

## 参照

### コメント規約

- [対象範囲](references/cs-comments-scope.md)
- [文体・語尾ルール](references/cs-comments-style.md)
- [タグ別ルール](references/cs-comments-tags.md)
- [型summaryの構文パターン](references/cs-comments-type-summary.md)
- [用語統一規則](references/cs-comments-terminology.md)

### 固定テンプレート

- [Unity イベントメソッド](references/templates/template-unity.md)
- [汎用メソッド](references/templates/template-common.md)
- [UI 遷移メソッド](references/templates/template-ui.md)
- [非同期メソッド](references/templates/template-async.md)
- [暗号化ラッパー](references/templates/hec-frameworks/template-crypted.md)
- [JSON変換用クラス](references/templates/hec-frameworks/template-for-json.md)

## 実行原則

- **スキル開始時に `[skill-started: comment-cs]` を出力する。** Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの開始を検知するために使用する

## 手順

- [ ] 1. 対象ファイルを読み込む
- [ ] 2. 上記の参照ファイルをすべて読み込む
- [ ] 3. 対象範囲ルールに基づき、コメント必須要素を洗い出す
- [ ] 4. 未コメント要素を特定し、新規コメントを付与する
  - 固定テンプレートに該当するメソッドにはテンプレートをそのまま適用する
  - それ以外の要素には文体・タグ・型summary・用語ルールに従って記述する
- [ ] 5. 既存コメントを検証し、規約違反があれば修正する
  - 文体・句点の誤り（メソッドが常体になっている等）
  - 型summaryの構文パターン不適合
  - 固定テンプレートとの不一致
  - 用語・送り仮名の表記ゆれ
  - 禁止範囲への不要なコメント付与は削除しない（既存コメントは内容が正しい限り保持）
- [ ] 6. `/review-cs-comment` でレビューし、指摘があれば修正する（最大3回）
- [ ] 7. セルフレビューを実施する

## セルフレビュー

- [ ] `/review-cs-comment` を実行し、指摘に対応したか
- [ ] 解消しきれない `/review-cs-comment` の指摘がある場合、ユーザーに報告したか

## サブスキルとして呼び出された場合

他スキル（`/implement-cs` 等）から呼び出された場合、スキル完了マーカーの出力後に **停止せず呼び出し元スキルの次の手順に即座に進む。** ユーザー入力を待たないこと。

## スキルマーカー

開始時点で以下のマーカーを出力する。マーカーは Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの開始を検知するために使用される。

```
[skill-started: comment-cs]
```

セルフレビューが完了した時点で、以下のマーカーを出力する。マーカーはフック `guard-cs-direct-edit` と Codex hook `guard-cs-direct-edit-codex` がスキルコンテキストの終了を検知するために使用される。

```
[skill-completed: comment-cs]
```
