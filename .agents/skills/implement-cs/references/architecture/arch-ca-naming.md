---
paths:
  - "**/*.cs"
  - "**/*.asmdef"
---

> **背景・設計意図の詳細は `corpus/engineering/architecture/csharp-unity/` を参照。**

# Clean Architecture 命名規約・ディレクトリ構成

## アセンブリ・名前空間命名

### アセンブリ命名パターン

```
Hecres.<Layer>.<Module>.<CleanArchLayer>
```

サブカテゴリの追加は `<CleanArchLayer>` の後にドット区切りで追加（例: `Domain.Entities`, `Domain.Repositories`）。

### 名前空間命名パターン

```
Hecres.<Layer>.<Module>.<CleanArchLayer>.<Subcategory>.<FunctionalArea>
```

標準 Subcategory:

| Subcategory | 配置される層 | 内容 |
|------------|-------------|------|
| `Entities` | Domain | エンティティクラス |
| `ValueObjects` | Domain | 値オブジェクト |
| `Repositories` | Domain(IF) / Infrastructure(実装) | リポジトリ |
| `Services` | Domain / UseCase | ドメインサービス / アプリケーションサービス |
| `Managers` | Presentation / Infrastructure | マネージャー |
| `Presenters` | Presentation | MVRP Presenter |
| `Interfaces` | Domain | インターフェース定義 |
| `Bases` | 各層 | 基底クラス |

## 型命名

### インターフェース

- 必ず `I` プレフィックスを付与
- 役割を表す動詞 + 名詞の組み合わせを推奨

### 基底クラス

- 抽象クラスには `Base` 接尾辞を付与（`Abstract` プレフィックスは使わない）

### Provider / Operator の命名関係

```
IXxxProvider ← 読み取り（同期/非同期プロパティ + Observable）
    ↑ 継承
IXxxOperator ← 書き込み（状態変更メソッド + 読み取り）
```

### 列挙型・値オブジェクト

```csharp
// 列挙型: 型名自体が明確な場合は Type 不要
BattleTeamSide
AppCurrencyType          // 種類を示すため Type 付与

// 値オブジェクト: ドメイン用語そのまま
PriceTag
AssetFilePath

// JSON 変換用: XxxForJson
Vector3ForJson
```

## 接尾辞セマンティクス

> **相互参照**: 接尾辞の詳細な定義（典型的な層、状態保持、選択フロー）は `glossary/gls-naming.md` を参照。

## コンテキスト間の命名規則

同一のドメイン概念がコンテキストをまたぐ際は、同じクラス名を使わず、コンテキスト固有の属性・状態をクラス名に埋め込む。

```
Foundation.MasterData（原型）
  ItemData                        ← マスターデータ定義

Foundation.UserData（所持・編集）
  InInventoryItemData             ← 「インベントリ内の」アイテム

App.InGame（ゲームプレイ）
  ItemActivateCommand             ← アイテム「発動コマンド」
```

## ファイル・ディレクトリ命名

- 1ファイル1型が原則。ファイル名 = 型名（拡張子 `.cs`）
- Partial class: `ClassName.FunctionalArea.cs`
- `Bases/` ディレクトリ: 基底クラスの格納
- `Interfaces/` ディレクトリ: インターフェース定義の格納
- `Generated/` ディレクトリ: 自動生成コード（編集禁止）

### モジュール内の標準ディレクトリ構成

```
<Module>/
├── Domain/
│   └── Runtime/Scripts/
│       ├── Entities/
│       │   └── Bases/
│       ├── ValueObjects/
│       ├── Repositories/Interfaces/
│       └── Services/Interfaces/
├── UseCase/
│   └── Runtime/Scripts/
├── Infrastructure/
│   └── Runtime/Scripts/
│       ├── Managers/
│       └── Repositories/
├── Presentation/
│   └── Runtime/Scripts/
│       ├── Managers/Bases/
│       └── Presenters/Bases/
├── CompositionRoot/
│   └── Runtime/Scripts/
└── SequenceRoot/
    └── Runtime/Scripts/
```

## Partial class の使用方針

クラス分割ではなく Partial class での分割を選択する場合がある。

**Partial class を選択すべきケース**:
- クラス分割すると接続処理が複雑化するだけでメリットが弱い場合
- クラスが各カテゴリの集約の役割を担っておりコード量は多いが、分割してもコード量の削減が見込めない場合

```
AudioManager.cs              ← 基本構造・コンストラクタ
AudioManager.Music.cs        ← BGM 制御機能
AudioManager.Sfx.cs          ← 効果音機能
AudioManager.Debug.cs        ← デバッグ機能
```

**クラス分割を検討すべきケース**:
- 各関心領域が独立した責務を持ち、単体でテスト・再利用が可能な場合
- 処理が本当に複雑であり、クラス分割により各クラスの責務が明確に軽くなる場合
