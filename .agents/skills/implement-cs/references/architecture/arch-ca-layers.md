---
paths:
  - "**/*.asmdef"
  - "**/*.cs"
---

> **背景・設計意図の詳細は `corpus/engineering/architecture/csharp-unity/` を参照。**

# Clean Architecture レイヤー構造・配置規約

## アセンブリ管理

### アセンブリ命名パターン

```
Hecres.<Layer>.<Module>.<CleanArchLayer>
```

| セグメント | 説明 |
|-----------|------|
| `Hecres` | プロジェクトルート（固定） |
| `<Layer>` | Core, Frameworks, Project |
| `<Module>` | モジュール名 |
| `<CleanArchLayer>` | Domain, UseCase, Infrastructure, Presentation, CompositionRoot, SequenceRoot |

### asmdef の重要な設定

| 設定 | 推奨値 | 理由 |
|------|--------|------|
| `autoReferenced` | `false` | 明示的な参照のみ許可 |
| `rootNamespace` | asmdef 名と一致 | 名前空間の一貫性 |
| `references` | 最小限 | 不要な依存を避ける |

### 外部ライブラリの依存制約

| ライブラリ | 参照可能な層 |
|-----------|-------------|
| R3 | Domain 以上の全層 |
| UniTask | Domain 以上の全層 |
| VContainer | CompositionRoot のみ |
| UnityEngine | Presentation, Infrastructure, CompositionRoot |

### 循環参照の防止策

1. **インターフェースの活用**: 下位層にインターフェースを定義し、上位層で実装
2. **イベント駆動**: 直接参照の代わりに R3 の Subject を活用
3. **DI による解決**: CompositionRoot でバインディングし、実行時に依存関係を解決

### 新規アセンブリ追加時のチェックリスト

- 命名規則に従っている
- 層間の依存規約を守っている
- 不要な参照がない
- 循環参照がない
- rootNamespace が設定されている

## 設計判断の優先順位

設計判断が競合した場合の解決順序:

```
1. 基本原則（一貫性、Reactive-first、読み書き分離、非同期安全、IF駆動、Domain純粋性）
2. 既存コードの慣習（既に確立されたパターンに従う）
3. パターン文書の使用ガイドライン
4. SOLID 原則などの一般的な設計原則
```

## クリーンアーキテクチャ副層の依存方向

```
Presentation ──┐
               │
Infrastructure ├──▶ UseCase ──▶ Domain
               │
SequenceRoot ──┤
               │
CompositionRoot┘
```

- **Domain**: 他の層に依存しない（最も安定した層）
- **UseCase**: Domain のみに依存
- **Infrastructure / Presentation**: UseCase と Domain に依存
- **SequenceRoot**: UseCase, Domain, Presentation に依存
- **CompositionRoot**: 全層を参照可能（DI 設定のため）
- **逆方向の依存は禁止**

## 層の省略判断

| 条件 | 省略可能な層 |
|------|------------|
| 外部システムとの統合がない | Infrastructure |
| UI を持たない | Presentation |
| シーン遷移を管理しない | SequenceRoot |
| ユースケースが単純（Domain 直接操作で十分） | UseCase |
| DI 設定が親スコープで完結する | CompositionRoot |

## 層配置の判断

新しいコードがどの層に配置されるかの判断ツリー。

```
Q1: 他のプロジェクトでも再利用できるか？
├── YES → Q2: Unity に依存するか？
│         ├── YES → Q3: ドメイン固有のロジックか？
│         │         ├── YES → Frameworks 層
│         │         └── NO  → Core 層（HecUnity）
│         └── NO  → Core 層（HecCSharp）
└── NO  → Q4: プロジェクト固有のゲームロジックか？
          ├── YES → Project 層
          └── NO  → もう一度 Q1 を検討（本当に再利用不可か？）
```

### Project 層内の配置判断

```
Q1: ゲームプレイに直結するか？
├── YES → Q2: メインシーンかインゲームシーンか？
│         ├── メインシーン → App.Main
│         ├── インゲームシーン → App.InGame
│         └── 両方にまたがる → Q3 へ
└── NO  → Q3: データ管理系か？
          ├── マスターデータ → Foundation.MasterData
          ├── ユーザーデータ → Foundation.UserData
          ├── ネットワーク通信 → Foundation.Networking
          └── 複数モジュール共有 → Foundation.Shared

Q3（両方にまたがる場合）:
├── インターフェース定義のみ → Port
├── 具体実装の結線 → Wiring
└── 共有データ型 → Foundation.Shared
```

## クリーンアーキテクチャ副層の判断

モジュール内部で、コードをどの副層に配置するかの判断。

```
Q1: このコードの責務は何か？

├── ビジネスルール・ドメインモデル
│   └── Domain 層
│       ├── 識別子を持つ → Entities
│       ├── 属性で同一性が決まる → ValueObjects
│       ├── データアクセスの抽象 → Repositories/Interfaces
│       └── エンティティに属さないロジック → Services/Interfaces

├── ユースケース（ビジネスルールの組み合わせ）
│   └── UseCase 層

├── 外部システムとの統合（DB、API、ファイル）
│   └── Infrastructure 層

├── UI・ユーザーインタラクション
│   └── Presentation 層

├── DI 設定・依存関係の結線
│   └── CompositionRoot 層

└── シーン / シーケンスのライフサイクル管理
    └── SequenceRoot 層
```

## モジュール境界の判断

```
Q1: 既存モジュールの責務範囲に含まれるか？
├── YES → 既存モジュールを拡張
└── NO  → Q2: 独立した asmdef が必要か？
          ├── YES（独自の依存関係 / 他モジュールから参照される / ビルド時間の分離が有効）
          │   → 新規モジュールを作成
          └── NO → 既存の最も近いモジュールに追加
```

## インターフェース駆動原則

モジュール間の依存は具象クラスではなくインターフェースを介する。

```
OK: UseCase 層が Domain/Repositories/Interfaces の IXxxRepository に依存
    → Infrastructure 層が IXxxRepository を実装
    → CompositionRoot で DI バインド

NG: UseCase 層が Infrastructure 層の XxxRepository 具象クラスを直接参照
```

## マスターデータ Getter の層別許容範囲

| 層・要素 | 許容 | 理由 |
|---------|------|------|
| Entity / ValueObject | NG | Entity は自身の状態管理に閉じるべき |
| Domain Service | 条件付き | 必要なデータのみメソッド引数で受け取る |
| UseCase | OK | マスターデータの参照・組み合わせはアプリケーションロジック |
| Presenter | 許容 | 表示のためのマスターデータ解決は Presenter の責務内 |
| View | 条件付き許容 | 特定マスターデータ取得 IF の Inject 可。巨大ファサードは禁止 |

## エラー処理

### 方針

- **例外ベース**: 予期しないエラーは例外で伝搬
- **Result 型は不使用**

### エラー処理コードの層別配置

| 層 | エラー処理の役割 |
|----|----------------|
| Domain | エラーは投げるだけ（例外のキャッチはしない） |
| UseCase | ビジネスエラーを判定し、適切に伝搬 |
| Infrastructure | 外部システムエラーを捕捉し、Domain 例外に変換 |
| Presentation | エラー UI を表示 |

### CancellationToken の例外処理

- メソッド冒頭で `token.ThrowIfCancellationRequested()` を呼ぶ
- `OperationCanceledException` は捕捉しない（自然に伝搬させる）

## 判断が困難な場合の原則

1. **既存の類似モジュールと同じ方法で実装する** -- 一貫性原則の適用
2. **迷ったら制約の強い選択を取る** -- Provider（読み取り）で足りるなら Operator にしない
3. **判断根拠を設計書に記録する** -- 「なぜこのパターンを選んだか」を明記する
