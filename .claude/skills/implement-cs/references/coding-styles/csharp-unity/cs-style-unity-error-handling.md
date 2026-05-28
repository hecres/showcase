---
paths:
  - "**/*.cs"
---

# Unity エラーハンドリング

`cs-error-handling.md` の Unity 固有拡張ルールを定める。

---

## 1. 許容異常系（Unity 固有）

### 適用対象

アセット内部のデータ不備（AnimationEvent未設定、AnimatorController内のステート欠落等）。アセット自体は読み込めているが内部データに不備がある場合で、演出やビジュアルが欠落するだけでゲームロジックの整合性は維持される。

### 対応

**必ず `Debug.LogError` でエラーログを出力する**。ログメッセージの先頭に `[許容異常系]` プレフィックスを付与する。

```csharp
// ✅ 良い例 — 許容異常系: リソース不備によるエラー報告 + 処理継続
if (animationEventCount == 0)
{
    // 許容異常系: イベント未設定は想定外だが演出スキップで継続
    Debug.LogError($"[許容異常系] AnimationEventが未設定です: {animationClip.name}");
    return;
}

var interval = duration / animationEventCount;
```

### 適用外（例外を投げる）

ロジックの不整合（状態遷移の矛盾、インデックス範囲外、前提条件の未充足等）は例外を投げて即座に検知する。

---

## 2. 防御的コードの検証テーブル（Unity 拡張）

`cs-error-handling.md` §5 の検証テーブルに加え、以下の Unity 固有の出自を追加する。

| 出自 | null の可能性 | 対応 |
|------|-------------|------|
| `[SerializeField]` フィールド（`[CanBeNull]` なし） | なし（非null前提） | チェックを除去する |
| `[SerializeField] [CanBeNull]` フィールド | あり | チェックは妥当 |
| `[Inject]` フィールド | あり（DI未登録） | チェックは妥当 |

