---
paths:
  - "**/*.cs"
---

# ディレクトリ命名規約

ディレクトリ命名に関する普遍的なルール。

---

## 1. ディレクトリ名の単数形・複数形

ディレクトリ名は**原則として複数形**を使用する。

### 正例・誤例

```
// ✅ 良い例 — 複数形
Builders/
Catalogs/
Controllers/
Exports/
Performers/
Previews/
Commons/
Inputs/

// ❌ 悪い例 — 単数形（例外に該当しない場合）
Builder/
Catalog/
Controller/
Export/
Performer/
Preview/
Common/
Input/
```

### 例外

以下の場合は単数形を使用する。

| 例外 | 理由 | 例 |
|------|------|-----|
| enum値によるディレクトリ | enum メンバー名との1対1対応のため（2章参照） | `Core/`, `Head/`, `LeftShoulder/` |
| 抽象概念・不可算名詞 | 英語の性質上、複数形が不自然 | `Core/`, `Persistence/` |
| 複数インスタンスが想定されない固有名 | プロジェクト名・ツール名など、唯一の実体を指す名称 | `Project/`, `MyBuilder/` |

### 判断基準

「そのディレクトリ内に同種のファイルが複数格納される」場合は複数形を使用する。ディレクトリ名が固有のシステムや概念そのものを指す場合は単数形を使用する。

---

## 2. enum値によるディレクトリ命名

enum のメンバー名をディレクトリ名として使用する場合、**enum文字列をそのまま使用し、複数形にしない**。

### 正例・誤例

```
// ✅ 良い例 — enum メンバー名そのまま
Core/
Head/
LeftShoulder/
Legs/

// ❌ 悪い例 — 複数形
Cores/
Heads/
LeftShoulders/
Legss/        ← 不規則複数形の問題が発生する
```

### 理由

- 英語の不規則複数形（`Legs` → `Legss`?、`Sheep` → `Sheeps`?）への対応コストが高い
- enum メンバー名との1対1対応が保証されるため、コード上で `enum.ToString()` で直接パスを構築できる
- 命名の曖昧さが排除される

### 適用範囲

この規則は、enum値に基づくディレクトリ全般に適用する。アセットディレクトリ・リソースディレクトリ・出力先ディレクトリ等、用途を問わない。

---

## 3. 型種別によるサブディレクトリ配置

特定の型種別に該当するファイルは、親ディレクトリ直下ではなく専用のサブディレクトリに配置する。名前空間もディレクトリ構造に一致させる。

| 型種別 | サブディレクトリ | 対象 | 名前空間例 |
|--------|-----------------|------|-----------|
| 基底クラス | `Bases/` | `abstract class` および `Base` 接尾辞を持つクラス | `...Presenters.Bases` |
| インターフェース | `Interfaces/` | `interface`（`I` プレフィックス） | `...Items.Interfaces` |
| 拡張メソッド | `Extensions/` | `static class` の拡張メソッド群 | `...Transforms.Extensions` |

### ディレクトリ構造の例

```
Presenters/
├── Bases/
│   └── PresenterBase.cs
├── Interfaces/
│   └── IPresenter.cs
├── Extensions/
│   └── PresenterExtensions.cs
└── ItemPresenter.cs          ← 具象クラスは親ディレクトリに配置
```

### 補足

- `Interfaces/` 配下はさらにサブディレクトリで分類してよい（例: `Interfaces/Operators/`、`Interfaces/Providers/`）
- ベンダーコード（サードパーティライブラリの取り込み）はこの規則の対象外
- `partial` クラスの分割ファイル（`ClassName.PartialName.cs`）は代表ファイルと同じディレクトリに配置する

---

## 4. 名前空間とディレクトリ構造の一致

名前空間はディレクトリ階層と一致させる。

```
// ✅ 良い例
// ファイルパス: src/MyProject/Domain/Sessions/Session.cs
namespace MyProject.Domain.Sessions

// ✅ 良い例
// ファイルパス: src/MyProject/App/Main/UseCase/Quests/QuestSequence.cs
namespace MyProject.App.Main.UseCase.Quests
```

```
// ❌ 悪い例 — ディレクトリ構造と不一致
// ファイルパス: src/MyProject/App/Main/UseCase/Quests/QuestSequence.cs
namespace MyProject.App.Main.Quests
```
