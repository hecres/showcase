# MVRP パターン用語集

| 英語 | 日本語 | 説明 | 使用例 |
|------|--------|------|------|
| MVRP | MVRP | Model-View-Reactive Presenterパターン。R3によるリアクティブバインディングでModelとViewを接続する設計 | — |
| Model | Model | ドメイン層のデータモデル。状態を保持しReactivePropertyで変更を通知する層 | — |
| View | View | Modelの状態を画面に反映するプレゼンテーション層。MonoBehaviourで実装 | — |
| Presenter | Presenter | ModelとViewをリアクティブに接続する仲介役。POCOで実装 | `MvrpPresenterBase` |
| MvrpPresenterBase | MvrpPresenter基底 | Presenterの共通基底クラス。購読ライフサイクル管理を担う | — |
| MvrpRxToken | MvrpRx購読トークン | Presenterに紐づく購読管理用トークン。Observable購読は必ず`AddTo(MvrpRxToken)`する | — |
