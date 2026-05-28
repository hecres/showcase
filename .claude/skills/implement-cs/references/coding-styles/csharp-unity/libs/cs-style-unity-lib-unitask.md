---
paths:
  - "**/*.cs"
---

# UniTask コーディングルール

UniTask 固有のコーディングルールを定める。

---

## 1. Fire-and-Forget 非同期パターン

戻り値を待機しない非同期呼び出しには `.Forget()` を使用する。キャンセルが想定される場合は `.SuppressCancellationThrow()` を前置する。

```csharp
// ✅ 通常の fire-and-forget
item.SwitchAsync(isVisible, destroyCancellationToken).Forget();

// ✅ キャンセル抑制付き
HighlightMomentAsync(destroyCancellationToken).SuppressCancellationThrow().Forget();
```

