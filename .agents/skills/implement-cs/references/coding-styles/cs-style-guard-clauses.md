---
paths:
  - "**/*.cs"
---

# C# ガード節

メソッド先頭・コンストラクタでのガード節に関するルールを定める。

---

## 1. ガード節の優先

例外処理は try-catch ではなくガード節（早期 return / throw）を優先する。

```csharp
// ✅ メソッドのガード節
public void Process(string input)
{
    if (string.IsNullOrEmpty(input)) return;
    // ... 本処理
}
```

```csharp
// ❌ 悪い例 — ガード節で済む場面で try-catch
try
{
    Process(input);
}
catch (ArgumentException)
{
    return;
}
```

---

## 2. ガード節の記述順序

メソッド先頭のガード節は以下の順序で記述する。

1. `token.ThrowIfCancellationRequested()`（CancellationToken を引数で受け取るメソッドでは **必須**）
2. null ガード（`if (param == null) throw new ArgumentNullException(...)`）
3. その他のガード節（値の範囲チェック等）

```csharp
// ✅ 良い例
public async Task ProcessAsync(DataModel data, CancellationToken token)
{
    token.ThrowIfCancellationRequested();
    if (data == null) throw new ArgumentNullException(nameof(data));
    // ... 本処理
}
```

```csharp
// ❌ 悪い例 — ThrowIfCancellationRequested がない / 順序が逆
public async Task ProcessAsync(DataModel data, CancellationToken token)
{
    if (data == null) throw new ArgumentNullException(nameof(data));
    // token.ThrowIfCancellationRequested() が欠落
    // ... 本処理
}
```
