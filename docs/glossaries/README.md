# 用語集

開発中ゲームで用いる用語を、カテゴリ別の用語集として定義しています。<br/>
各用語集は `| 英語 | 日本語 | 説明 | 使用例 |` の4列で構成されます。

## 役割

本用語集は次の役割を担います。

- **ユビキタス言語の定義**<br/>
  同じ概念には常に同じ英語名・日本語訳・意味を与え、コード・仕様書・設計議論を通じて語のゆれ（同一概念に複数の訳語、同一訳語に複数の英語名）をなくします。<br/>
  命名そのものを設計上の合意として共有します。
- **実装用語の日本語・英語 対応の定義**<br/>
  ドメインの概念に加えて、コードに現れる技術用語・ライブラリ用語も収録し、日本語の概念に対応する英語の命名を一意に引ける対応表として機能します。

> [!NOTE]<br/>
> 本PJは一人での開発のため、コンテキスト境界や職種による分離を設けず、すべてを一貫した単一の用語集に統合しています。

## カテゴリ

カテゴリは「どこで使う語か（再利用性・依存先）」で分かれています。<br/>
上位の3つ（汎用・C#・Unity）はPJ横断の語彙、下位の2つ（HecCore・HecFrameworks）は[レイヤー構成](../game-design/architectures/layer-structure.md)のメタ層 Core / Frameworks に対応します。

### 汎用（commons）

PJ横断で用いる共通語彙。アーキテクチャ・データ・演出・設定などの分野別に定義します。

| 用語集 | 主な収録語 |
|---|---|
| [Clean Architecture](commons/gls-arch-clean.md) | Interactor（UseCase層サービス） |
| [DDD](commons/gls-arch-ddd.md) | Entity / ValueObject / Aggregate / Service / Repository |
| [MVRP パターン](commons/gls-arch-mvrp.md) | Model / View / Presenter / MvrpRxToken |
| [Hec独自パターン](commons/gls-pattern-hec.md) | Provider / Operator（読み取り・読み書きの分離） |
| [マスターデータ](commons/gls-data-masterdata.md) | MasterData / Surface / Appearance / AbilityData |
| [ユーザーデータ](commons/gls-data-userdata.md) | ProgressData / InventoryData / WalletData |
| [JSONデータ](commons/gls-data-json.md) | ForJson（シリアライズ専用データ） |
| [エラー](commons/gls-errors.md) | ErrorCode / ErrorType / ErrorNumber |
| [演出](commons/gls-performance.md) | Marker / Performer / Socket / Spawner |
| [設定・パラメータ](commons/gls-settings.md) | Preference / Config / Settings / Tuning / Cosmetic |
| [ストア](commons/gls-store.md) | Store / StoreItem / PriceTag |
| [オーディオ](commons/gls-audio.md) | Music / Sfx / Vfx |
| [UI](commons/gls-ui.md) | OperationGuide（操作案内） |
| [プリミティブ](commons/gls-primitives.md) | Angle / Direction / Id / Position / Range 等 |
| [単位](commons/gls-units.md) | Seconds / Ratio |

### C#（csharp）

C# 言語そのものに関する語彙。

| 用語集 | 主な収録語 |
|---|---|
| [非同期](csharp/gls-cs-async.md) | CancellationToken |

### Unity（unity）

Unity エンジンの公式機能と、採用ライブラリの語彙。

| 用語集 | 主な収録語 |
|---|---|
| [Unity 公式機能](unity/gls-unity-official.md) | Addressables |
| [R3](unity/libs/gls-unity-lib-r3.md) | Observable / CompositeDisposable / Sticky（Reactive） |
| [UniTask](unity/libs/gls-unity-lib-unitask.md) | UniTask（非同期） |
| [VContainer](unity/libs/gls-unity-lib-vcontainer.md) | Container（DI） |
| [EnhancedScroller](unity/libs/gls-unity-lib-enhanced-scroller.md) | セル仮想化スクロール |
| [uPalette](unity/libs/gls-unity-lib-upalette.md) | ColorEntry / ColorTheme（カラー・テーマ管理） |

### HecCore（hec-core）

Unity に依存しない本PJ独自基盤（Core 層）の語彙。

| 用語集 | 主な収録語 |
|---|---|
| [セキュリティ](hec-core/gls-heccr-security.md) | Crypted（チート対策の暗号化ラッパー） |

### HecFrameworks（hec-frameworks）

Unity に依存する本PJ独自基盤（Frameworks 層）の語彙。

| 用語集 | 主な収録語 |
|---|---|
| [HecApp](hec-frameworks/gls-hecfrm-app.md) | AppSequence / SceneSequence / SequenceRoot |
| [HecTalk](hec-frameworks/gls-hecfrm-talk.md) | TalkSession / Talker / Participant / Utterance |
| [HecUI](hec-frameworks/gls-hecfrm-ui.md) | TransitionableUi / FocusableUi |
</content>
