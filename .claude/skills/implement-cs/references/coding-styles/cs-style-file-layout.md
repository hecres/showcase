---
paths:
  - "**/*.cs"
---

# C# ファイルレイアウト

クラス内のメンバー定義順序を規定する。

---

## 1. 全体構造

クラス内メンバーは**フィールド・プロパティ領域**（上部）と**メソッド領域**（下部）に大別される。

### フィールド・プロパティ領域

以下の順序で配置する。

| 順 | カテゴリ | 例 |
|----|---------|-----|
| 1 | ネスト型（enum, class, struct） | `private enum State` |
| 2 | public 定数・public static readonly | `public const int MaxLevel = 99;` |
| 3 | private 定数・private static readonly | `private const float MinIntensity = -10f;` |
| 4 | public プロパティ | `public IDataProvider DataProvider => dataProvider;` |
| 5 | public イベント | `public event Action<string> OnItemChanged;` |
| 6 | private プロパティ（キャッシュ付きアクセサ等） | `private Material CachedMaterial { get { ... } }` |
| 7 | private フィールド（属性なし） | `private Material cachedMaterialCache;` |

異なるカテゴリの境界には**空行を1行**入れる。同一カテゴリ内の連続したフィールドは空行なしで詰める。

ただしカテゴリ7（private フィールド）では、属性の種類ごとにグループ分けし、**グループ間には空行を1行**入れる。

| グループ | 例 |
|---------|-----|
| `[SerializeField]` 付き | `[SerializeField] private float speed;` |
| `[Inject]` 付き | `[Inject] private IObjectResolver resolver;` |
| 属性なし | `private int count;` |

```csharp
// ✅ 良い例 — カテゴリ間に空行、属性グループ間にも空行
private enum State { None, Processing, Completed }

public const int MaxLevel = 99;
public static readonly Vector3 DefaultScale = Vector3.one;

private const float MinIntensity = -10f;

public IDataProvider DataProvider => dataProvider;
public bool IsCompleted => state == State.Completed;

private Material CachedMaterial => cachedMaterialCache ??= LoadMaterial();

[SerializeField] private float speed;
[SerializeField] private int maxCount;

[Inject] private IObjectResolver resolver;

private Material cachedMaterialCache;
private State state;
```

```csharp
// ❌ 悪い例 — カテゴリ間の空行がない、属性グループが混在
private enum State { None, Processing, Completed }
public const int MaxLevel = 99;
public static readonly Vector3 DefaultScale = Vector3.one;
private const float MinIntensity = -10f;
public IDataProvider DataProvider => dataProvider;
public bool IsCompleted => state == State.Completed;
private Material CachedMaterial => cachedMaterialCache ??= LoadMaterial();
[SerializeField] private float speed;
[Inject] private IObjectResolver resolver;
private Material cachedMaterialCache;
private State state;
```

### メソッド領域

メソッドは**ライフサイクル順**に配置する。アクセス修飾子（public / protected / private）によるグルーピングは行わない。

---

## 2. メソッドの配置規則

### 2.1 ライフサイクル順

メソッドはクラスのライフサイクルに沿って配置する。

```
初期化 → 更新 → 破棄
```

例:

```
InitializeAsync          ← 初期化
  └→ SetupDependencies   ← 初期化の子
BindEvents               ← イベント紐づけ（初期化）
  └→ BindItemEvents      ← イベント紐づけの子
UnbindEvents             ← イベント解除（破棄寄り）
Update                   ← 更新ループ
ProcessItem              ← 外部呼び出し
UpdateCachedPositions    ← 複数箇所から呼ばれるヘルパー
```

### 2.2 呼び出し元と呼び出し先の順序

**本ルールは private メソッドに適用する。** public メソッドは外部から独立して呼ばれるため、内部の呼び出し関係で定義位置を変えない。

メソッドAがメソッドB, C, Dを呼び出す場合、**Aの直後にB → C → Dを呼び出し順に並べる**。

```csharp
// ✅ 良い例
void PrepareObjectsAsync(...)
{
    await PrepareFieldObjectsAsync(...);
    await PrepareFormationObjectsAsync(...);
}

void PrepareFieldObjectsAsync(...) { ... }

void PrepareFormationObjectsAsync(...) { ... }
```

```csharp
// ❌ 悪い例 — 呼び出し順と定義順が逆
void PrepareFormationObjectsAsync(...) { ... }

void PrepareFieldObjectsAsync(...) { ... }

void PrepareObjectsAsync(...)
{
    await PrepareFieldObjectsAsync(...);
    await PrepareFormationObjectsAsync(...);
}
```

### 2.3 外部呼び出しメソッドの配置

クラス内部のメソッドから呼ばれず、**外部からのみ呼ばれるメソッド**は、内部ライフサイクルチェーンの後に配置する。外部呼び出しメソッドが複数ある場合は、外部から呼ばれる順序に従う。

```
InitializeAsync          ← 内部ライフサイクル
  └→ SetupDependencies
BindEvents
  └→ BindItemEvents
UnbindEvents
Update
GetTargetTransform       ← 外部呼び出し（内部チェーンの後）
ProcessItemAsync         ← 外部呼び出し
UpdateCachedPositions    ← 共有ヘルパー（§2.4）
```

### 2.4 共有ヘルパーの配置

メソッドXとYがそれぞれメソッドZを呼び出している場合、**Zは先に定義されている側（XまたはY）の後に配置する**。

```csharp
// ✅ 良い例 — Xが先に定義されているので、ZはXの後
void X() { Z(); }
void Z() { ... }
void Y() { Z(); }
```

---

## 3. メンバー定義順と処理順の一致

メンバー（フィールド・プロパティ）の定義順序と、以下の順序を一致させる。

- コンストラクタ引数の順序
- コンストラクタ内での代入順序
- 初期化メソッド・破棄メソッド等の処理順序

メンバー側と引数側の型が異なる場合（インターフェースと実装型など）でも、対応関係があれば順序を揃える。

```csharp
// ✅ 良い例 — メンバー定義順・引数順・代入順が一致
public class AppearanceData
{
    public MotionType MotionType { get; }
    public WeaponType WeaponType { get; }
    public EffectId ChargeEffectId { get; }
    public EffectId DischargeEffectId { get; }

    public AppearanceData(MotionType motionType, WeaponType weaponType, EffectId chargeEffectId, EffectId dischargeEffectId)
    {
        MotionType = motionType;
        WeaponType = weaponType;
        ChargeEffectId = chargeEffectId;
        DischargeEffectId = dischargeEffectId;
    }
}
```

```csharp
// ❌ 悪い例 — 引数順がメンバー定義順と不一致
public AppearanceData(EffectId chargeEffectId, MotionType motionType, EffectId dischargeEffectId, WeaponType weaponType)
```

コンストラクタに限らず、キャッシュ取得・クリア・Dispose 等の処理でも同様に、メンバー定義順と処理順を一致させる。

```csharp
// ✅ 良い例 — フィールド定義順とキャッシュ取得順・クリア順が一致
private IHealthHandler[] healthHandlers;
private IInitializeHandler[] initializeHandlers;
private IMoveHandler[] moveHandlers;

private void CacheHandlers()
{
    healthHandlers = GetComponentsInChildren<IHealthHandler>();
    initializeHandlers = GetComponentsInChildren<IInitializeHandler>();
    moveHandlers = GetComponentsInChildren<IMoveHandler>();
}

private void ClearHandlersCache()
{
    healthHandlers = null;
    initializeHandlers = null;
    moveHandlers = null;
}
```

