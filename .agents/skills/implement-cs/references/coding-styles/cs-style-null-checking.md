---
paths:
  - "**/*.cs"
---

# C# Null チェック

null チェック構文の選択に関するルールを定める。

---

## 1. null ガードと null 許容

- **null 非許容の参照型引数**: `if (param == null) throw new ArgumentNullException(nameof(param));` で null ガードを行う
- **null 許容の参照型引数**: null 許容であることを明示し、null ガードは行わない
- **`?? throw` パターン**: nullチェック対象をそのまま変数・フィールドに格納する場合に使用する。後続で同じ引数を複数回参照する場合はガード節（`if (param == null) throw ...`）を使用する

```csharp
// ✅ コンストラクタの null チェック — 格納と同時に検証
public MyService(ILogger logger, IFactory factory)
{
    this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
}

// ✅ メソッドの null ガード — 後続で複数回参照
public void Process(DataModel data)
{
    if (data == null) throw new ArgumentNullException(nameof(data));
    // data を複数回使用する本処理
}
```
