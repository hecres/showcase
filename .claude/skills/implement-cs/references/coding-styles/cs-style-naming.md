---
paths:
  - "**/*.cs"
---

# C# 命名規約

コード内の識別子命名に関する規約を定める。

---

## 1. キャッシュ付きプロパティのバッキングフィールド

private プロパティが遅延初期化やキャッシュ取得を行い、バッキングフィールドに結果を保持するパターンでは、バッキングフィールド名に **`Cache` サフィックス**を付ける。フィールドを直接使用せずプロパティ経由でアクセスすべきことを明示するため。

```csharp
// ✅ 良い例
private BehaviorTree CachedBehaviorTree => behaviorTreeCache ? behaviorTreeCache : behaviorTreeCache = GetBehaviorTree();
private BehaviorTree behaviorTreeCache;

private Material MainGaugeMaterial => mainGaugeMaterialCache ? mainGaugeMaterialCache : mainGaugeMaterialCache = mainGauge.material;
private Material mainGaugeMaterialCache;
```

```csharp
// ❌ 悪い例 — Cache サフィックスがないため、フィールドとプロパティの区別が曖昧
private BehaviorTree CachedBehaviorTree => behaviorTree ? behaviorTree : behaviorTree = GetBehaviorTree();
private BehaviorTree behaviorTree;
```

---

## 2. 識別子の命名スタイル

プロジェクト共通の命名規則を定義する。

### PascalCase を使用する要素

| 要素 | 例 |
|------|-----|
| 型（class, struct, enum, delegate） | `UserManager`, `AlignmentType` |
| 名前空間 | `MyProject.App.Main.UseCase` |
| インターフェース（`I` プレフィックス付き） | `IHealthProvider` |
| 型パラメータ（`T` プレフィックス付き） | `TConfig` |
| メソッド | `CalculateDamage` |
| ローカル関数 | `ValidateInput` |
| プロパティ | `CurrentHealth` |
| イベント | `OnItemAdded` |
| enum メンバー | `Alpha`, `Neutral` |
| public / private 定数 | `MaxHealth`, `DefaultTimeout` |
| public インスタンスフィールド | `Position` |
| public static フィールド | `Instance` |
| public / private static readonly フィールド | `Empty`, `SharedInstance` |

### camelCase を使用する要素

| 要素 | 例 |
|------|-----|
| private インスタンスフィールド | `currentHealth` |
| private static フィールド | `instanceCount` |
| ローカル変数 | `damageResult` |
| ローカル定数 | `maxRetry` |
| パラメータ | `cancellationToken` |

### 禁止事項

- private フィールドにアンダースコアプレフィックス（`_field`）は使用しない

---

## 3. ループ変数・ラムダ引数・ローカル変数の命名

1文字や意味のない省略名を避け、**元のコレクション名や型から意味が分かる名前**を付ける。

### foreach / for

ループ変数はコレクション名の単数形を使用する。

```csharp
// ✅ 良い例
foreach (var renderer in allRenderers)

// ❌ 悪い例 — 1文字の省略名
foreach (var r in allRenderers)
```

### ラムダ引数

```csharp
// ✅ 良い例 — 意味のある名前、または汎用名 x
.Select(item => item.DataId)
.Where(renderer => renderer.enabled)
.Select(x => x.DataId)

// ✅ 良い例 — 引数を使用しない場合は `_`
.Subscribe(_ => RefreshView())
ResetStream.Subscribe(_ => OnReset())

// ✅ 良い例 — 複数の未使用引数はアンダースコアを増やして区別
SomeEvent.Subscribe((_, __) => OnEvent())
ThreeArgEvent.Subscribe((_, __, ___) => OnEvent())

// ❌ 悪い例 — x 以外の1文字省略名
.Select(c => c.DataId)
.Where(r => r.enabled)

// ❌ 悪い例 — 引数を使用しないのに名前を付ける
.Subscribe(x => RefreshView())
```

引数を使用しないラムダでは `_` を使う。これにより「この引数は意図的に未使用」であることが読み手に明示される。複数の未使用引数がある場合は `_`, `__`, `___` のようにアンダースコアの数を増やして区別する。

### ローカル変数（分割代入・辞書アクセス等）

辞書の KeyValuePair を分割する場合も、意味のある名前を付ける。

```csharp
// ✅ 良い例
var renderer = kvp.Key;
var material = kvp.Value;

// ❌ 悪い例 — 省略名
var r = kvp.Key;
var m = kvp.Value;
```

### 許容される汎用名

| 名前 | 用途 |
|------|------|
| `i`, `j`, `k` | インデックスカウンタ（`for (var i = 0; ...)`） |
| `item` | foreach のループ変数（`foreach (var item in items)`） |
| `x` | ラムダ引数（`.Select(x => x.DataId)`） |
| `_`, `__`, `___` | 未使用のラムダ引数。複数ある場合はアンダースコア数で区別（`.Subscribe((_, __) => OnEvent())`） |

上記以外の1文字・省略名は使用しない。

---

## 4. `Async` サフィックス

`Async` サフィックスは **`Task` / `ValueTask` / `UniTask` など awaitable を返す非同期メソッドにのみ** 付与する。`void` を返す同期メソッドには付けない。

```csharp
// ✅ 良い例 — UniTask を返す非同期メソッド
public async UniTask ShowAsync(CancellationToken token)

// ✅ 良い例 — Task を返す非同期メソッド
private static async Task ExportAndRefreshAsync(CancellationToken token)

// ✅ 良い例 — 同期メソッドにはサフィックスなし
private void ExportAndRefresh()
```

```csharp
// ❌ 悪い例 — void を返すのに Async サフィックス
private void ExportAndRefreshAsync()
```

