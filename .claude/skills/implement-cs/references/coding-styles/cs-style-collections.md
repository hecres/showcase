---
paths:
  - "**/*.cs"
---

# C# コレクション型

公開APIにおけるコレクション型の選択ルールを定める。

---

## 1. 公開APIのコレクション型

公開プロパティ・戻り値には `IReadOnlyList<T>` / `IReadOnlyDictionary<K,V>` を使用する。可変コレクション（`List<T>`, `Dictionary<K,V>`）を直接公開しない。

```csharp
// ✅ 良い例
public IReadOnlyList<ItemData> Items => items;
private readonly List<ItemData> items = new();

public IReadOnlyDictionary<PageType, PageView> PageTable => pageTableCache ??= CreatePageTable();
```

```csharp
// ❌ 悪い例
public List<ItemData> Items => items;
```

