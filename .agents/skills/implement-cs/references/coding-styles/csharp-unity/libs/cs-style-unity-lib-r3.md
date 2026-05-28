---
paths:
  - "**/*.cs"
---

# R3 コーディングルール

R3（Reactive Extensions）固有のコーディングルールを定める。

---

## 1. Subscribe/SubscribeAwait ハンドラのメソッド抽出

`Subscribe` / `SubscribeAwait` のラムダ本体が複雑になった場合、名前付きメソッドに切り出す。

### 判断基準

以下のいずれかに該当する場合にメソッド化する。

- ラムダ本体が複数の `await` を含む
- ラムダ本体が条件分岐を含む
- ラムダ本体が10行を超える

### メソッド化の方法

- ラムダを `async (value, token) => await XxxAsync(value, token)` の委譲形式にする
- 切り出し先メソッドの引数はラムダ引数と同じ型・同じ順序
- メソッド名はハンドラの意図を表す名前にする（`PerformXxxAsync` 等）
- 切り出し先メソッドは呼び出し元の直後に配置する

```csharp
// ✅ 良い例 — 複雑なハンドラをメソッドに切り出し
Model.SomeEvent
     .SubscribeAwait(async (context, token) => await PerformSomeEventAsync(context, token), AwaitOperation.Sequential)
     .AddTo(disposables);

// ✅ 良い例 — 単純なハンドラはインラインのまま
Model.SomeEvent
     .Where(value => value.IsValid)
     .SubscribeAwait(async (value, token) => await UniTask.WhenAll(items.Select(item => item.ProcessAsync(value, token))), AwaitOperation.Drop)
     .AddTo(disposables);
```

---

## 2. Observable パターン

### 準正常系の通知

設計上想定されるエラー条件は、Observable ストリームで呼び出し元に通知する。

```csharp
// ✅ 良い例 — 準正常系: 設計上想定される状態を Observable で通知
if (headIndex.Value >= allItems.Count)
{
    exhaustedStream.OnNext(Unit.Default);
    return null; // 準正常系: 全消費時は通知して終了
}
```
