---
paths:
  - "**/*.cs"
---

> **背景・設計意図の詳細は `corpus/engineering/architecture/csharp-unity/` を参照。**

# DI・ライフサイクル管理規約

## VContainer による DI

### LifetimeScope とバインディング

```csharp
protected override void Configure(IContainerBuilder builder)
{
    // インターフェースへのバインド
    builder.Register<MyService>(Lifetime.Singleton).As<IMyService>();

    // インスタンス登録
    builder.RegisterInstance(existingInstance);

    // MonoBehaviour の登録
    builder.RegisterComponent(myComponent);
}
```

### ライフタイム

| Lifetime | 説明 |
|----------|------|
| `Singleton` | アプリケーション全体で1つ |
| `Scoped` | LifetimeScope ごとに1つ |
| `Transient` | 解決ごとに新規生成 |

### 注入方法

```csharp
// 推奨: コンストラクタインジェクション（非 MonoBehaviour）
public class MyService
{
    private readonly IRepository repository;
    public MyService(IRepository repository) => this.repository = repository;
}

// 推奨: フィールドインジェクション（MonoBehaviour）
[field: Inject]
protected IHecEventSystem HecEventSystem { get; }
```

## バインドパターンの判断

| パターン | 条件 |
|---------|------|
| InstantiateAndBind | コードで new するオブジェクト。初期化パラメータが必要 |
| FindAndBind | シーンに事前配置された MonoBehaviour |
| Register | ステートレスなサービス。DI コンテナがライフタイムを管理 |
| RegisterInstance | 外部で生成済みのインスタンスを登録 |

### InstantiateAndBind パターン

オブジェクト生成と DI 登録を1ステップで行う。初期化順序の制御、相互依存の解決、可読性の向上に効果がある。

```csharp
public static MyService InstantiateAndBind(IContainerBuilder builder)
{
    var instance = new MyService();
    builder.RegisterInstance(instance).As<IMyService>();
    return instance;
}
```

### FindAndBind パターン

シーン内の既存オブジェクトを検出して登録する。シーンに事前配置された MonoBehaviour（カメラ、Canvas など）の登録に使用。

## シングルトンパターン

### Singleton vs DI Singleton の判断

```
VContainer の LifetimeScope が利用可能な状態か？
├── YES → DI Singleton（VContainer の Lifetime.Singleton）を使う
└── NO  → MonoBehaviour が必要か？
          ├── YES → HecUnityMonoBehaviourSingletonBase を使う
          └── NO  → SingletonBase を使う（極力避ける）
```

| 観点 | シングルトン | DI (Singleton) |
|------|-------------|----------------|
| アクセス | 静的 (`Instance`) | 注入 |
| テスト | モック困難 | モック容易 |
| 依存関係 | 暗黙的 | 明示的 |
| ライフサイクル | 自己管理 | コンテナ管理 |

**NonMonoBehaviour シングルトン**: `SingletonBase<T>` を継承。遅延初期化。スレッドセーフではない。

**MonoBehaviour シングルトン**: `HecUnityMonoBehaviourSingletonBase<T>` を継承。`DontDestroyOnLoad` で永続化。重複防止付き。

## CompositionRoot 階層

```
HecMainSequenceLifetimeScopeBase     ← Frameworks 基底
    ↓ 継承
ProjectMainSequenceLifetimeScope     ← Project 層メインスコープ
    ├── WiringLifetimeScope          ← モジュール間の結線
    ├── Scene1LifetimeScope          ← シーン固有スコープ
    └── Scene2LifetimeScope          ← シーン固有スコープ
```

- 子スコープは親スコープの全登録にアクセス可能
- 子スコープの登録は親からは不可視

### 動的親スコープ

シーンロード時に `LifetimeScope.EnqueueParent` で親を動的設定し、子シーンの LifetimeScope が親スコープの登録にアクセスできるようにする。

## シーンシーケンスシステム

画面は「シーンシーケンス」として管理され、Addressables で非同期ロードされる。

### 3層構造

| 層 | 基底クラス | 責務 | 配置 |
|----|-----------|------|------|
| Model | `HecSceneSequenceBase` | ビジネスロジック、状態管理 | UseCase |
| Manager | `SceneSequenceManagerBase` | ライフサイクル管理、シーン遷移制御 | SequenceRoot |
| Presenter | `SceneSequenceUiPresenterBase` | UI 生成、イベント発行 | Presentation |

Model は VContainer の `[Inject]` を使わない。`ManualDiConfig` と Manager からの明示的なコンストラクタ引数で依存を受け取る。

### TArgs パターン

シーン間のデータ受け渡しは `SceneSequenceManagerArgsBase` を継承した型安全な Args クラスで行う。

### スタック管理

| CanStack | 動作 |
|----------|------|
| `true` | 前のシーンをメモリに保持。戻る操作が可能 |
| `false` | 前のシーンをすべてアンロードし完全に遷移 |

### シーン遷移フロー

```
1. 画面遷移演出開始
2. ダイアログ UI クリーンアップ
3. CanStack = false の場合、既存シーンをすべてアンロード
4. Addressable でシーンを非同期ロード（親 LifetimeScope 設定あり）
5. ロードしたシーンから ISceneSequenceManager を検出
6. スタックに Push
7. Manager.InitializeSequenceAsync(args, token)
8. Manager.ActivateSequenceAsync(token)
9. Manager.OnPostActivateSequenceAsync()（フォーカス更新、遷移演出終了）
```

### Manager 初期化フロー

```
InitializeSequenceAsync(args, token)
├── 1. SequenceManagerArgs = args（引数保存）
├── 2. SequenceModel = await CreateModelAsync(token)（Model 生成）
├── 3. await SequenceModel.InitializeAsync(token)（Model 初期化）
├── 4. SequenceUiPresenter = await CreateSequenceUiPresenterAsync(token)（Presenter 生成）
├── 5. OnDestroy 購読設定
└── 6. BackSceneSequenceRequested 購読設定
```

## 手動 DI（ManualDiConfig）

UseCase 層のクラス（SceneSequence 等）は VContainer の `[Inject]` を使わず、コンストラクタで全依存を明示的に受け取る。ランタイムで決定するパラメータは各クラスのネスト型 `ManualDiConfig` にまとめる。

```csharp
// UseCase 層 -- VContainer に依存しない純粋なコンストラクタ
public SomeSequence(
    ManualDiConfig config,                          // ランタイムデータ
    IApiRequester apiRequester,                     // インフラ依存
    IMasterDataGetter masterDataGetter
) : base(config, apiRequester, masterDataGetter) { ... }

// SequenceRoot 層（Manager）-- VContainer 管理下で生成・注入
var config = new SomeSequence.ManualDiConfig(editTarget, inventory);
return new SomeSequence(config, ApiRequester, MasterDataGetter);
```

## 新規シーン追加時に必要なファイル

1. **Args**: `XxxManagerArgs : SceneSequenceManagerArgsBase`
2. **Model**: `XxxSequence : ProjectSceneSequenceBase<XxxSequence.ManualDiConfig>`
3. **Manager**: `XxxManager : SceneSequenceManagerBase<XxxManagerArgs, XxxSequence, XxxUiPresenter>`
4. **Presenter**: `XxxUiPresenter : SceneSequenceUiPresenterBase<XxxSequence>`
5. **LifetimeScope**: 必要な場合
6. **Unity Scene**: Addressable に登録されたシーンアセット
