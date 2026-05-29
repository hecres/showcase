---
paths:
  - "**/*.cs"
---

> **背景・設計意図の詳細は `corpus/engineering/architecture/csharp-unity/` を参照。**

# リアクティブ・非同期パターン規約

## Reactive-first 原則

状態の公開は Observable / ReactiveProperty を第一選択とする。

- 状態が変化しうるもの → `ReactiveProperty<T>` / `ReadOnlyReactiveProperty<T>`
- イベント（発生の事実のみ重要）→ `Subject<T>` / `Observable<T>`
- 一度だけ取得すればよいもの → 同期プロパティまたは `UniTask<T>`

## 非同期安全原則（UniTask + CancellationToken）

非同期処理は UniTask を使用し、原則として CancellationToken を引数で受け取り伝搬する。

```csharp
// 原則: CancellationToken を受け取り、冒頭でチェック
public async UniTask InitializeAsync(CancellationToken token)
{
    token.ThrowIfCancellationRequested();
    await LoadDataAsync(token);
}
```

**例外 -- キャンセル制御の責務を呼び出し先が持つ場合**:
シーン遷移・解放などライフサイクル級の処理は、引数に CancellationToken を設けず内部で `destroyCancellationToken` 等を使用する。これは「呼び出し元にキャンセルの責務を渡さない」意図的な設計判断である。

| 条件 | CancellationToken |
|------|-------------------|
| 通常の非同期処理（データロード、UI 表示等） | 引数で受け取る（原則） |
| シーン遷移・解放などライフサイクル級の処理 | 引数に設けず内部で管理 |
| イベントハンドラからの後処理（fire-and-forget） | `destroyCancellationToken` + `.Forget()` |

## 非同期パターンの判断

コールバック（`Action` パラメータ）を非同期パターンとして採用しない。非同期処理は UniTask、イベント通知は Observable/ReactiveProperty に統一する。

```
Q1: 処理の完了を待つ必要があるか？
├── YES → Q2: 複数回発火するか？
│         ├── YES → Observable（SubscribeAwait で非同期購読）
│         └── NO  → UniTask
└── NO  → Q3: 値の変化を監視するか？
          ├── YES → ReactiveProperty / Observable
          └── NO  → void メソッド（Fire-and-forget は .Forget() で明示）
```

## MVRP パターン（Model-View-Reactive-Presenter）

MVP の派生パターン。R3 と UniTask を組み合わせたリアクティブ UI 管理手法。neuecc 氏（R3/UniTask 作者）が提唱した。

### データフロー

```
Model ──Observable──▶ Presenter ──メソッド呼び出し──▶ View
Model ◀──状態更新── Presenter ◀──ユーザー入力通知── View
```

### 各要素の責務

| 要素 | 責務 | 制約 |
|------|------|------|
| Model | ビジネスロジックと状態管理 | View・Presenter を知らない |
| View | UI 表示とユーザー入力受付（MonoBehaviour） | ビジネスロジックを持たない |
| Presenter | Model と View の仲介（MvrpPresenterBase 継承） | ドメインロジックを持たない |

### ライフサイクル

```
1. Presenter 生成（DI）
2. InitializeModelLinkAsync(model, token) → Model との紐づけ
3. LinkMvrpRxAsync(token) → RX 購読開始
4. ユーザー操作 / Model 更新 → リアクティブに UI 更新
5. UnlinkMvrpRx() → RX 購読解除（MvrpRxToken キャンセル）
6. Dispose → リソース解放
```

### 設計ガイドライン

- **1 Presenter = 1 View**: View と 1:1 で対応し、複数 View をまとめない
- **必ず AddTo(MvrpRxToken) で管理**: 購読解除漏れを防止
- **ドメインロジックを Presenter に書かない**: Model に委譲

### MvrpLinker

Model と Presenter の紐づけを一元管理する静的ユーティリティクラス。`InitializeModelLinkAsync`（Model 参照の設定）→ `LinkMvrpRxAsync`（リアクティブ購読の構築）を順に実行する。

### token vs MvrpRxToken の注意

`OnLinkMvrpRxAsync` の引数 `token` ではなく `MvrpRxToken` に `AddTo` すること。`token` はメソッド実行のキャンセル用であり、購読ライフサイクルとは異なる。

## R3 + UniTask によるリアクティブ・非同期処理

### Subject vs ReactiveProperty の選択

```
「現在の値」に意味があるか？
├── YES → ReactiveProperty<T>（購読開始時に現在値が即座に流れる）
└── NO  → Subject<T>（イベント発生の事実のみ。購読開始前の値は流れない）
```

### 購読ライフサイクル管理（AddTo の使い分け）

| パターン | 用途 | 層 |
|---------|------|-----|
| `.AddTo(cancellationToken)` | CancellationToken で制御 | Domain, UseCase |
| `.AddTo(this)` | MonoBehaviour/LifetimeScope に紐づけ | Presentation, CompositionRoot |
| `.AddTo(disposables)` | CompositeDisposable で手動管理 | 明示的な解除が必要な場合 |
| `MvrpRxToken` を使用 | Presenter 固有のトークン | MVRP Presenter |

### AwaitOperation の選択

| Operation | 説明 | 用途例 |
|-----------|------|-------|
| `Sequential` | 順番に実行 | 順序保証が必要な処理 |
| `Drop` | 実行中は新しいリクエストを無視 | UI ボタン連打防止 |
| `Switch` | 新しいリクエストで前のをキャンセル | 検索入力 |
| `Parallel` | 並列実行 | 独立した処理 |

### ベストプラクティス

- **必ず AddTo で管理**: `observable.Subscribe(...).AddTo(token);`
- **Subject は private に**: 外部には `Observable<T>` として公開
- **長い購読チェーンを避ける**: 分割して名前をつける
- **コールバック（`Action` パラメータ）は非同期パターンとして採用しない**: 非同期処理は UniTask、イベント通知は Observable/ReactiveProperty に統一

## ライフサイクル管理

### 初期化パターン

```csharp
// 標準: InitializeAsync(CancellationToken)
public virtual async UniTask InitializeAsync(CancellationToken token)
{
    token.ThrowIfCancellationRequested();
    await LoadDataAsync(token);
}

// MonoBehaviour の初期化
private void Start()
{
    InitializeInternalAsync(destroyCancellationToken).Forget();
}
```

### 破棄パターン

```csharp
public void Dispose()
{
    disposeStream.OnNext(Unit.Default);   // 破棄イベントを通知
    disposeStream.OnCompleted();          // ストリーム完了
    disposeStream.Dispose();             // Subject 自体を破棄
    rxCts.Cancel();                      // すべての Rx 購読を解除
}
```

### よくある落とし穴

| 問題 | 対策 |
|------|------|
| メモリリーク | `.Subscribe()` に必ず `.AddTo()` を付与 |
| 二重初期化 | 初期化済みフラグまたは設計で防止 |
| キャンセル未対応 | 全非同期メソッドに token を伝搬 |
| Dispose 後のアクセス | `IsDestroyed` チェック |
| Fire-and-forget 警告 | `.Forget()` で明示的に無視を宣言 |
