---
paths:
  - "**/*.cs"
---

# C# 改行と折り返し

本規約はAIがコードを生成・編集する際に適用する。人間側のコーディング規約とは独立したルールであり、人間メンバーへの順守は求めない。

---

## 1. 行の長さ

- 上限: **300文字**
- 300文字以内に収まる場合は改行しない
- AIのデフォルト（~130文字）で早期改行しないこと

### 理由

AIは学習データの傾向から80〜130文字付近で自動的に改行する。しかし本プロジェクトでは、可読性よりも「不要な改行による差分肥大化」を問題視する。300文字以内であれば1行に収めることで、diffの行数が減り、レビューコストが下がる。

### AIが早期改行しやすい構文パターン

以下は300文字以内であれば必ず1行で書くこと。

#### 変数・定数・フィールド宣言

```csharp
// ✅ 良い例
public const string DefaultOutputDirectory = "Assets/MyProject/Tools/Builder/Editor/Output";
private readonly MyBuilder builder = new MyBuilder(config, materialManager, catalog);

// ❌ 悪い例 — 代入演算子の後で改行
public const string DefaultOutputDirectory =
    "Assets/MyProject/Tools/Builder/Editor/Output";
private readonly MyBuilder builder =
    new MyBuilder(config, materialManager, catalog);
```

#### 単一引数のメソッド呼び出し・コンストラクタ

```csharp
// ✅ 良い例
var basePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(BuilderPaths.MirrorPrefabPath);
throw new InvalidOperationException($"ベースPrefabが見つかりません: {BuilderPaths.MirrorPrefabPath}");

// ❌ 悪い例 — 引数を次行に送っている
var basePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(
    BuilderPaths.MirrorPrefabPath);
throw new InvalidOperationException(
    $"ベースPrefabが見つかりません: {BuilderPaths.MirrorPrefabPath}");
```

---

## 2. メソッド引数の改行

- 行長が許す限り **1行に収める**
- 300文字を超える場合のみ、各引数を個別行に分割し、閉じ `)` を独立行にする

### 理由

引数を中途半端にグループ化して2〜3行に分割すると、引数の追加・削除で隣接行にも差分が出る。1行か完全分割の二択にすることで、変更時の差分を最小化する。

### 正例・誤例

```csharp
// ✅ 良い例 — 1行に収まる場合
return new DetailSequence(config, sessionOperator, apiRequester, dataGetter);

// ❌ 悪い例 — 中途半端なグループ分割
return new DetailSequence(
    config, sessionOperator, apiRequester,
    dataGetter);
```

300文字超えの場合:

```csharp
// ✅ 良い例 — 完全分割
return new DetailSequence(
    config,
    sessionOperator,
    apiRequester,
    dataGetter
);
```

---

## 3. チェーンドットの垂直揃え

- メソッドチェーンを改行する前に、まず1章の300文字ルールを確認する。チェーン全体が300文字以内に収まるなら1行で書くこと
- 300文字を超えて改行が必要な場合、式の **最初の `.` に垂直揃え** する
- 固定4スペースインデントにしない

### 理由

垂直揃えにすることで、チェーンの起点と後続メソッドの関係が視覚的に明確になる。4スペースインデントでは、ラムダ本体のインデントとチェーンドットのインデントが混同しやすい。

### 正例・誤例

```csharp
// ✅ 良い例 — 最初の`.`に垂直揃え
source.Items
      .WhereNotNull()
      .SelectMany(item => item.Children)
      .ToList();

listView.ItemClicked
        .Select(value => ...)
        .Subscribe(value => ...)
        .AddTo(disposables);

// ❌ 悪い例 — 4スペースインデント
source.Items
    .WhereNotNull()
    .SelectMany(item => item.Children)
    .ToList();

listView.ItemClicked
    .Select(value => ...)
    .Subscribe(value => ...)
    .AddTo(disposables);
```

---

## 4. ラムダ本体の閉じ方と後続チェーン

- ラムダ本体を `{ }` で囲む場合、閉じ `})` と後続チェーンメソッドは **別行** に分ける
- 後続チェーンメソッドはチェーンドットの垂直揃え（3章）に従う

### 理由

`}).AddTo(disposables);` のように閉じ括弧と後続メソッドを1行にまとめると、ラムダ本体の終了と後続チェーンの境界が曖昧になる。別行にすることで、ラムダ本体のスコープ終了が視覚的に明確になる。

### 正例・誤例

```csharp
// ✅ 良い例 — `})` と後続チェーンが別行
source.ItemAdded
      .Subscribe(item =>
      {
          processor.HandleNewItem(item);
      })
      .AddTo(disposables);

// ❌ 悪い例 — `}).AddTo()` が同一行
source.ItemAdded.Subscribe(item =>
{
    processor.HandleNewItem(item);
}).AddTo(disposables);
```

