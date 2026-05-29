---
paths:
  - "**/*.cs"
---

> **背景・設計意図の詳細は `corpus/engineering/architecture/csharp-unity/` を参照。**

# DDD ドメインモデリング規約

## Domain 層の純粋性

Domain 層は Unity フレームワーク機構（コンポーネントシステム、DI 属性等）への依存を持たない。

**許可されるもの**:
- 純粋な C# コード、`System.*` 名前空間
- `CancellationToken`（`System.Threading` 由来）
- `UniTask`（非同期処理が Domain に浸透しているため例外的に許可）
- `R3` の `ReactiveProperty` / `Observable`（Reactive-first 原則に基づく）
- `Vector3` / `Vector2` / `Quaternion` 等の UnityEngine 値型（ゲームドメインでは座標・方向・回転が本質的構成要素であり、独自型でラップすると演算子オーバーロードや数学関数の再実装コストが過大なため）

**禁止されるもの**:
- `MonoBehaviour` / `ScriptableObject` 等のコンポーネント・ライフサイクル型
- `VContainer` の属性やクラス（`[Inject]`, `[SerializeField]` による DI 等）
- `GameObject` / `Transform` 等のシーングラフ型
- その他の Unity パッケージ / 外部パッケージ

## 読み書き分離（Provider/Operator 原則）

読み取り専用インターフェース（Provider）と書き込みインターフェース（Operator）を分離するパターン。CQRS の読み書き分離の考え方をインターフェース設計に応用したもので、CQRS そのものとは異なる（CQRS はリクエストオブジェクトの分離、本パターンはインターフェースの公開範囲の制限）。

- 外部から参照するだけのコードには Provider（読み取り専用）インターフェースのみを渡す
- 状態変更が必要なコードにのみ Operator（読み書き）インターフェースを渡す
- Operator は Provider を継承する（Operator を持つ = Provider も使える）

### Provider/Operator 設計ガイドライン

- **最小権限の原則**: 可能な限り Provider を使用し、書き込みが必要な場合のみ Operator
- **キャストの禁止**: `(IXxxOperator)provider` のようなキャストは避ける
- エンティティに複数の独立した状態領域がある場合、領域ごとに Processor を作成

## データ所属の判断

```
Q1: データはランタイムで変化するか？
├── NO（ビルド時/配信時に確定）
│   ├── ゲームコンテンツの定義か？ → MasterData
│   ├── アプリ動作の設定か？ → Config
│   └── バランス調整用パラメータか？ → Tuning
└── YES（ランタイムで変化する）
    ├── ユーザーの操作で変化する → Q2
    └── ゲームプレイ中にのみ変化する → Domain Entity（インメモリ）

Q2: データを永続化する必要があるか？
├── YES → ユーザー固有の設定か？
│         ├── YES → Preference
│         └── NO  → UserData
└── NO  → Domain Entity
```

## インターフェース粒度の判断

```
Q1: 利用者によって必要なメソッドが異なるか？
├── YES → 分割する
│         ├── 読み取りのみの利用者がいる → Provider/Operator 分離
│         ├── 機能的に独立した関心領域がある → 機能別分割
│         └── 一部の利用者にのみ見せたい → ISP に基づき分割
└── NO  → 分割しない（過度な分割は複雑さを増す）
```

## リポジトリ ガイドライン

- インターフェースではドメイン言語を使用（技術的な用語を避ける）
- 非同期メソッドには `Async` サフィックス
- 必ず `CancellationToken` を受け取る
- Infrastructure 層に配置し、Domain 層からはインターフェース経由でアクセス
