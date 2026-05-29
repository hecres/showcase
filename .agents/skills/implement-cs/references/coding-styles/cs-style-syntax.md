---
paths:
  - "**/*.cs"
---

# C# 構文スタイル

構文の選択・修飾子・キーワード使用に関するルールを定める。

---

## 1. var の使用

型が明白かどうかによらず、ローカル変数には `var` を使用する。

```csharp
// ✅ 良い例
var list = new List<int>();
var count = 0;
var result = Calculate(input);

// ❌ 悪い例
List<int> list = new List<int>();
int count = 0;
```

---

## 2. 修飾子

### 2.1 順序

```
public, private, protected, internal, file, new, static, abstract, virtual, sealed, readonly, override, extern, unsafe, volatile, async, required
```

### 2.2 this. 修飾

`this.` 修飾は使用しない（フィールド・プロパティ・メソッド・イベント全て）。

#### 例外1: 拡張メソッドの呼び出し

拡張メソッドをインスタンス内から呼び出す場合は `this.` が必須（C#言語仕様）。

```csharp
// ✅ 良い例 — 拡張メソッドは this. が必要
private IAnimator Animator => animatorCache ??= this.GetComponentSafely<IAnimator>();

// ❌ 悪い例 — コンパイルエラー CS0103
private IAnimator Animator => animatorCache ??= GetComponentSafely<IAnimator>();
```

#### 例外2: コンストラクタ引数とフィールドの同名衝突

コンストラクタの引数名が同名フィールドと衝突する場合は、`this.` を使ってフィールドを明示する。引数名のリネーム（`xxxValue` 等）は行わない。

```csharp
// ✅ 良い例 — コンストラクタの同名衝突は this. で解消
public MyService(ILogger logger)
{
    this.logger = logger;
}
```

理由:
- 呼び出し側で named-argument を使っている場合のリネーム影響が大きい
- 引数名から `Value` 等の意味のないサフィックスを取り除くことで、引数の意図が明確に保たれる
- `this.x = x;` は C# の一般的な慣例で、衝突解消より読みやすさを優先する

### 2.3 アクセス修飾子の明示

クラスおよび構造体の全メンバー（メソッド・フィールド・プロパティ・イベント・ネスト型）には、**アクセス修飾子を必ず明示する**。C# のデフォルト（`private`）に依存した省略は行わない。

```csharp
// ✅ 良い例 — private を明示
private void HandleCategoryChanged(string category)
{
}

private readonly ICatalog catalog;

private int selectedIndex = -1;

// ❌ 悪い例 — private を省略
void HandleCategoryChanged(string category)
{
}

readonly ICatalog catalog;

int selectedIndex = -1;
```

#### 理由

- 可視性が一目で判別でき、レビュー時に意図しない公開を見落としにくい
- `public` / `private` / `protected` / `internal` が混在するクラスで、アクセスレベルの確認に推論が不要になる
- コードの意図を明示する習慣として統一する

---

## 3. 式本体（Expression Body）

| 要素 | スタイル |
|------|---------|
| アクセサ（get/set） | 式本体を優先する |
| 演算子 | 式本体を優先する |
| メソッド | ブロック本体を使用する |
| コンストラクタ | ブロック本体を使用する |

```csharp
// ✅ 良い例 — アクセサの式本体
public int Health { get => health; private set => health = value; }

// ✅ 良い例 — 演算子の式本体
public static MyId operator +(MyId a, MyId b) => new(a.Value + b.Value);

// ✅ 良い例 — メソッドはブロック本体
public bool IsAlive()
{
    return health > 0;
}

// ✅ 良い例 — コンストラクタはブロック本体
public MyService(ILogger logger)
{
    this.logger = logger;
}
```

---

## 4. 組み込み型の使用

ローカル変数・パラメータ・メンバー・メンバーアクセスでは、フレームワーク型名ではなく組み込み型キーワードを使用する。

```csharp
// ✅ 良い例
int count = 0;
string name = "test";

// ❌ 悪い例
Int32 count = 0;
String name = "test";
```

---

## 5. default リテラル

型が明白かどうかによらず、`default(T)` ではなく `default` を使用する。

```csharp
// ✅ 良い例
CancellationToken token = default;
return default;

// ❌ 悪い例
CancellationToken token = default(CancellationToken);
```

---

## 6. switch 式の優先

`switch` 文（statement）ではなく `switch` 式（expression）を使用する。

```csharp
// ✅ 良い例
var label = dangerLevel switch
{
    DangerLevel.Normal => normalLabel,
    DangerLevel.Danger => dangerLabel,
    _                  => throw new ArgumentOutOfRangeException()
};
```

```csharp
// ❌ 悪い例
string label;
switch (dangerLevel)
{
    case DangerLevel.Normal:
        label = normalLabel;
        break;
    case DangerLevel.Danger:
        label = dangerLabel;
        break;
    default:
        throw new ArgumentOutOfRangeException();
}
```

---

## 7. `#region` の使用制限

`#region` は演算子オーバーロードのグルーピングにのみ使用する。フィールド・メソッド等のコード整理には使用しない。

```csharp
// ✅ 良い例 — 演算子のみ
#region Operators

public static MyId operator +(MyId a, MyId b) => new(a.Value + b.Value);
public static MyId operator -(MyId a, MyId b) => new(a.Value - b.Value);

#endregion
```

---

## 8. LINQ の優先

データの変換・フィルタリングには LINQ を使用する。`foreach` はコレクションへの副作用（状態変更・非同期呼び出し等）がある場合のみ使用する。`.ForEach()` 拡張メソッドは使用しない。

```csharp
// ✅ 変換 → LINQ
var items = source.Select(x => new ItemData(x, service));
var valid = source.Where(x => x.IsValid);

// ✅ 副作用 → foreach
foreach (var item in items)
{
    await item.InitializeAsync(token);
}
```

```csharp
// ❌ 悪い例 — 変換に foreach
var result = new List<ItemData>();
foreach (var x in source)
{
    result.Add(new ItemData(x, service));
}
```

---

## 9. 文字列の生成

文字列の生成には文字列補間（`$""`）を使用する。`+` 連結は使用しない。`StringBuilder` はループ内での大量結合など、パフォーマンスが要求される場面にのみ使用する。
