# 用語集

開発中ゲームで用いる用語を、カテゴリ別の用語集として定義しています。<br/>
各用語集は `| 英語 | 日本語 | 説明 | 使用例 |` の4列で構成されます。

> [!NOTE]<br/>
> 本ページは用語集全体からの**抜粋**です。<br/>
> カテゴリ索引で体系の全体像を示し、設計判断や記法が表れる代表用語集のみ実体を掲載しています。<br/>リンクのない項目は非掲載とし、収録語のみを索引に残しています。

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

> [!NOTE]<br/>
> リンク付きの用語集が掲載中の代表例です。

### 汎用（commons）

PJ横断で用いる共通語彙。アーキテクチャ・データ・演出・設定などの分野別に定義します。

| 用語集 | 主な収録語 |
|---|---|
| Clean Architecture | Interactor（UseCase層サービス） |
| DDD | Entity / ValueObject / Aggregate / Service / Repository |
| [MVRP パターン](commons/gls-arch-mvrp.md) | Model / View / Presenter / MvrpRxToken |
| [Hec独自パターン](commons/gls-pattern-hec.md) | Provider / Operator（読み取り・読み書きの分離） |
| [マスターデータ](commons/gls-data-masterdata.md) | MasterData / Surface / Appearance / AbilityData |
| ユーザーデータ | ProgressData / InventoryData / WalletData |
| JSONデータ | ForJson（シリアライズ専用データ） |
| エラー | ErrorCode / ErrorType / ErrorNumber |
| 演出 | Marker / Performer / Socket / Spawner |
| [設定・パラメータ](commons/gls-settings.md) | Preference / Config / Settings / Tuning / Cosmetic |
| ストア | Store / StoreItem / PriceTag |
| オーディオ | Music / Sfx / Vfx |
| UI | OperationGuide（操作案内） |
| プリミティブ | Angle / Direction / Id / Position / Range 等 |
| 単位 | Seconds / Ratio |

### C#（csharp）

C# 言語そのものに関する語彙。

| 用語集 | 主な収録語 |
|---|---|
| 非同期 | CancellationToken |

### Unity（unity）

Unity エンジンの公式機能と、採用ライブラリの語彙。

| 用語集 | 主な収録語 |
|---|---|
| Unity 公式機能 | Addressables |
| R3 | Observable / CompositeDisposable / Sticky（Reactive） |
| UniTask | UniTask（非同期） |
| VContainer | Container（DI） |
| EnhancedScroller | セル仮想化スクロール |
| uPalette | ColorEntry / ColorTheme（カラー・テーマ管理） |

### HecCore（hec-core）

Unity に依存しない本PJ独自基盤（Core 層）の語彙。

| 用語集 | 主な収録語 |
|---|---|
| セキュリティ | Crypted（チート対策の暗号化ラッパー） |

### HecFrameworks（hec-frameworks）

Unity に依存する本PJ独自基盤（Frameworks 層）の語彙。

| 用語集 | 主な収録語 |
|---|---|
| HecApp | AppSequence / SceneSequence / SequenceRoot |
| [HecTalk](hec-frameworks/gls-hecfrm-talk.md) | TalkSession / Talker / Participant / Utterance |
| HecUI | TransitionableUi / FocusableUi |
