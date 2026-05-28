---
paths:
  - "**/*.cs"
---

# UiAnimator パターン

Animator によるステート切替を持つ UI では、`AnimatorBatchBase` を継承した UiAnimator ラップクラスを使用する。

---

## 1. 構成

| 要素 | 命名 | 説明 |
|------|------|------|
| インターフェース | `I{Name}UiAnimator` | `SetIs{State}(bool)` 等のメソッドを定義 |
| 具象クラス | `{Name}UiAnimator` | `AnimatorBatchBase` 継承。`SetBool` / `SetTrigger` 等をラップ |

---

## 2. View クラスからの取得

View クラス（`{Name}Ui`）では `[SerializeField]` ではなく、`GetComponentSafely` によるキャッシュ付きプロパティで取得する。

```csharp
private I{Name}UiAnimator Animator => animatorCache ??= this.GetComponentSafely<I{Name}UiAnimator>();
private I{Name}UiAnimator animatorCache;
```

`[SerializeField]` によるワイヤリングは行わない。

---

## 3. 参照実装

`HighlightUi` + `HighlightUiAnimator`（`Assets/Hecres/Frameworks/HecUI.Toolkit/`）
