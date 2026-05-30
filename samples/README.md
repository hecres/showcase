# 実装サンプル

本ディレクトリには、技術設計資料で紹介した内容を Unity プロジェクトとして具体化したサンプルを配置しています。<br/>
アーキテクチャ、依存関係の分離、UI と Model の接続方法など、設計資料で説明した方針がコード上でどのように表れるかご確認いただければと思います。

> [!NOTE]<br/>
> 設計確認を主眼に置いているため、画面演出やビジュアル表現は作り込んでいません。<br/>
> ギャラリーの動画と異なり見た目は非常に淡白となります。

## ゲームの起動方法

1. Unity Hub から `samples/ArchitectureSample` を Unity 6（6000.4.1f1）で開く
2. `Assets/Hecres/Project/App.Main/SequenceRoot/Runtime/AddressableAssets/Local/Scenes/Main.unity` を開く
3. Unity Editor の Play ボタンで再生する

`Main` シーンから開始すると、タイトル画面を起点に各画面へ遷移します。

## 画面遷移

```mermaid
flowchart LR
    Main["Main"] --> TitleScreen["TitleScreen"]
    TitleScreen --> Home["Home"]
    Home --> QuestSelect["QuestSelect"]
    QuestSelect --> QuestExecution["QuestExecution"]
    QuestExecution --> QuestResult["QuestResult"]
    QuestResult --> Home
```

## 本サンプルで確認できる技術

| 観点 | 確認できること | 主な確認箇所 |
|---|---|---|
| クリーンアーキテクチャ | Domain / UseCase / Presentation / CompositionRoot などの責務分離と依存方向 | `Assets/Hecres/Project/App.Main/` |
| DDD | ドメイン層を中心にした Entity / ValueObject / Repository interface などの表現 | `Assets/Hecres/Project/*/Domain/Runtime/Scripts/` |
| MVRP | Model と View を Presenter が仲介し、UI 入力や状態反映を分離する構成 | `Assets/Hecres/Core/HecUnity/Presentation/Runtime/Scripts/DesignPatterns/Mvrp/MvrpLinker.cs` |
| DI | VContainer による依存登録と、シーン単位の Composition Root | `Assets/Hecres/Project/App.Main/CompositionRoot/Runtime/Scripts/AppSequences/` |
