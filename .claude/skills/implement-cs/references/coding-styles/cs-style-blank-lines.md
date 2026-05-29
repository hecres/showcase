---
paths:
  - "**/*.cs"
---

# C# 空白行

コード内の空行に関するルールを定める。

---

## 1. 基本ルール

- コード内の連続空行: 最大 **1行**
- 宣言内の連続空行: 最大 **1行**
- 単一行フィールド間: 空行なし（グルーピング可）
- プロパティ・メソッド・クラス定義の前後: 1行
- 属性: 各属性を個別行に配置する
- 属性の同一行配置閾値: 60文字以内なら同一行可

---

## 2. ガード節の空行

**異なる種類のガード節間**および**ガード節と後続処理の間**に空行を1行入れる。同種のガード節は詰める。

### 2.1 ガード節の種類分類

| 種類 | パターン | 例 |
|------|---------|-----|
| ThrowIf | `token.ThrowIfCancellationRequested()` | — |
| null ガード | `if (x == null) throw/return` | `if (a == null) throw new ArgumentNullException(...)` |
| その他ガード | 上記以外の `if (...) throw/return` | `if (!list.Any()) throw new ArgumentException()` |

### 2.2 同種判定ルール

- **ThrowIf 同士**: 同種（詰める）
- **null ガード同士**: 同種（詰める）。throw / return が混在しても `== null` チェックであれば同種
- **その他ガード同士**: 出力する例外タイプが同じなら同種（詰める）。異なれば異種（空行を入れる）
- **異なる種類間**: 空行を1行入れる

### 2.3 `?? throw` はガード節ではない

コンストラクタ等の `field = param ?? throw new ArgumentNullException(...)` はインライン検証であり、独立したガード節ではない。前後に空行を入れない。

```csharp
// ✅ 良い例 — ?? throw はインライン検証。空行を入れない
DataId = dataId ?? throw new ArgumentNullException(nameof(dataId));
Position = position;
Rotation = rotation;
OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId));
```

### 2.4 例

```csharp
// ✅ 良い例 — 異なる種類のガード節間・後続処理前に空行、同種は詰める
token.ThrowIfCancellationRequested();

if (a == null) throw new ArgumentNullException(nameof(a));
if (b == null) throw new ArgumentNullException(nameof(b));

await Process(a, b, token);
```

```csharp
// ✅ 良い例 — null ガードとその他ガードの間に空行。同じ例外タイプのその他ガード同士は詰める
if (weights == null) throw new ArgumentNullException(nameof(weights));

if (!weights.Any()) throw new ArgumentException();
if (weights.All(item => item <= 0)) throw new ArgumentException();

var randomValue = Random.value;
```

```csharp
// ✅ 良い例 — 単一ガード節の後に空行
if (cachedMaterial != null) return;

cachedMaterial = await LoadMaterialAsync(token);
```

```csharp
// ❌ 悪い例 — ガード節と後続処理が詰まっている
token.ThrowIfCancellationRequested();
if (a == null) throw new ArgumentNullException(nameof(a));
if (b == null) throw new ArgumentNullException(nameof(b));
await Process(a, b, token);
```

```csharp
// ❌ 悪い例 — 同じ例外タイプのガード間に不要な空行
if (!weights.Any()) throw new ArgumentException();

if (weights.All(item => item <= 0)) throw new ArgumentException();
```

