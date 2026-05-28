# タグ別ルール

## タグの改行スタイル

タグごとに以下のスタイルを使い分ける（Microsoft公式・.NET Runtime 等の主要OSSに準拠）。

| タグ | スタイル | 理由 |
|------|---------|------|
| `<summary>` | 常に複数行 | 説明が長くなる、複数文を含むことがある |
| `<remarks>` | 常に複数行 | 補足説明が長くなる |
| `<param>` | 原則単一行 | `name` 属性 + 短い説明という構造で1行に収まる |
| `<returns>` | 原則単一行 | 戻り値の説明は通常短い |
| `<exception>` | 原則単一行 | `cref` 属性 + 発生条件の短い説明 |

```csharp
/// <summary>
/// 指定IDのユーザーを取得します。
/// </summary>
/// <param name="userId">取得対象のユーザーID</param>
/// <param name="token">キャンセル用のトークン</param>
/// <returns>該当ユーザー。存在しない場合は <c>null</c></returns>
public async UniTask<User?> GetUserAsync(int userId, CancellationToken token)
```

短い説明であっても `<summary>` / `<remarks>` は複数行に統一する。

### 複数行内の明示的改行（`<br/>`）

複数行で記述する `<summary>` / `<remarks>` 等で、論理的に行を区切りたい箇所には `<br/>` を行末に付与する。IDE Tooltip 上で改行が反映され、意図した可読性が保たれる（XML仕様上、ソースの改行はTooltip表示時に空白として連結されるため、明示しない限り改行されない）。

```csharp
/// <summary>
/// アバターの中心位置（足元基準・姿勢非追従）<br/>
/// テレポートVFXなど、姿勢に左右されない安定した中心点が必要な用途に使用する
/// </summary>
```

#### 付与する/しないの判断

| ケース | `<br/>` | 例 |
|--------|---------|----|
| 1文を可読性のためソース上で折り返しているだけ | 不要 | 長文の途中改行（連続テキスト扱いでOK） |
| 独立した補足文・複数の論点を並べる | 付与 | 概要 → 詳細 / 補足、列挙的記述 |
| 末尾行 | 不要 | 最後の行には付けない |

### `<param>` / `<returns>` / `<exception>` を複数行で書くケース

以下に該当する場合は複数行で記述してよい。

- 説明文が長く、単一行だと横に伸びて可読性が落ちる
- 複数文・複数段落（`<para>`）を含む
- `<list>` 等の構造化要素を含む

```csharp
/// <param name="options">
/// 取得オプション。<c>null</c> の場合は既定値が適用される。
/// <list type="bullet">
///   <item><c>IncludeDeleted</c>: 論理削除済みも含めるか</item>
///   <item><c>MaxCount</c>: 最大取得件数</item>
/// </list>
/// </param>
```

## `<summary>`

1〜2文の簡潔な説明。

## `<remarks>`

`summary` の補足が必要な場合のみ使用。敬体 + 句点。

```csharp
/// <summary>
/// ダメージを計算します。
/// </summary>
/// <remarks>
/// 属性相性と補正を加味して最終ダメージを算出します。
/// </remarks>
```

## `<param>`

常体言い切り、句点なし。

```csharp
/// <param name="token">キャンセル用のトークン</param>
/// <param name="wantShow">表示へ切り替えるかどうか</param>
```

## `<returns>`

常体言い切り、句点なし。
内容が自明な場合は省略可。

**`bool` 戻り値の場合**: `true` / `false` の意味を列挙する。

```csharp
/// <returns>true: 有効 / false: 無効</returns>
```

**`Tuple` 戻り値の場合**: `T1`, `T2`, ... を要素ごとに説明する。

```csharp
/// <returns>T1: 計算結果 / T2: 補正値</returns>
```
