---
paths:
  - "**/*.cs"
---

# C# 波括弧のレイアウト

波括弧（ブレース）の配置と使用に関するルールを定める。

---

## 1. 配置スタイル

**Allman スタイル**: 開き `{` は新しい行に配置する。

```csharp
// ✅ 良い例
public void Execute()
{
    if (condition)
    {
        Process();
    }
}

// ❌ 悪い例 — K&R スタイル
public void Execute() {
    if (condition) {
        Process();
    }
}
```

---

## 2. 制御文のブレース要否

| 構文 | ルール |
|------|--------|
| `if` / `else` | 本体が同一行に収まる場合は省略可、それ以外は必須 |
| `for` / `foreach` / `while` | 本体が同一行に収まる場合は省略可、それ以外は必須 |
| `do-while` / `fixed` / `lock` / `using` | 常に必須 |

**「本体が同一行」の定義**: 制御キーワードと本体が同じ行に収まる場合のみ省略可。本体が次行に折り返す場合はブレース必須。

**`if`/`else` の一貫性ルール**: `if` と `else` のいずれか一方がブレースを使用する場合、もう一方もブレースを使用する。

```csharp
// ✅ 良い例 — 本体が同一行
if (isValid) return;
if (other == null) throw new ArgumentNullException(nameof(other));

// ✅ 良い例 — 本体が次行（ブレース必須）
foreach (var key in destroyedKeys)
{
    activeTouchCounts.Remove(key);
}

// ✅ 良い例 — if/else の一貫性
if (count <= 1)
{
    Remove(key);
    Notify();
}
else
{
    Decrement(key);
}

// ❌ 悪い例 — 本体が次行でブレースなし
foreach (var key in destroyedKeys)
    activeTouchCounts.Remove(key);

// ❌ 悪い例 — if/else でブレースが不一致
if (count <= 1)
{
    Remove(key);
    Notify();
}
else
    Decrement(key);

// ❌ 悪い例 — 複数行でブレースなし
if (isValid)
    Process();
    Notify(); // if の外
```
