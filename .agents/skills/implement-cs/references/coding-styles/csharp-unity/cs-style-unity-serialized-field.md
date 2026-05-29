---
paths:
  - "**/*.cs"
---

# SerializeField リネーム規約

`[SerializeField]` 付きフィールドのリネーム時に、Unity シリアライズ参照の整合性を維持するためのルール。

---

## 1. 対象範囲

`[SerializeField]` 属性が付与された private フィールドのリネームが発生した場合、本規約を適用する。

---

## 2. 必須対応

SerializeField フィールド名を変更する場合、以下を **すべて** 実施すること。

| # | 対応内容 | 説明 |
|---|---------|------|
| 1 | C# フィールド名の変更 | ソースコード上のフィールド宣言および参照箇所を変更する |
| 2 | Prefab / Scene ファイルのフィールド名変更 | `.prefab` / `.unity` ファイル内のシリアライズキー（YAML上のフィールド名）を新しい名前に書き換える |
| 3 | GameObject 名の確認・変更 | Prefab / Scene 内で旧フィールド名に基づく GameObject 名（`m_Name`）がある場合、新しい名前に合わせて変更する |

---

## 3. Prefab / Scene ファイルの編集手順

1. `Grep` で旧フィールド名を `.prefab` / `.unity` ファイルから検索する
2. 該当箇所の YAML キー名を新しいフィールド名に書き換える
3. 同一 GameObject の `m_Name` が旧名に基づいている場合は併せて変更する
4. `fileID` 等の参照値は **変更しない**（フィールド名のみ書き換える）

---

## 4. 注意事項

- Prefab / Scene 側を変更しないと、Unity Inspector 上でフィールド参照が外れ、再アサインが必要になる
- `m_Name`（GameObject 名）の変更は必須ではないが、Hierarchy 上の視認性のために旧名が残っている場合は変更を推奨する
- Prefab Variant が存在する場合は、Base Prefab と Variant の両方を確認すること
