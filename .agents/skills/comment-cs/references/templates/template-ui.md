# UI 遷移メソッド テンプレート

`IFocusableUi` / `ITransitionableUi` を実装するクラスの固定コメント。
改変せずそのまま使用すること。

## プロパティ

| プロパティ | summary |
|-----------|---------|
| `OnHide` | UIの非表示完了時に通知 |
| `OnShow` | UIの表示完了時に通知 |
| `State` | 現在の表示ステータス |

## メソッド

| メソッド | summary |
|----------|---------|
| `ShowAsync` | UIを表示します。 |
| `ShowSoon` | UIを即時表示します。 |
| `HideAsync` | UIを非表示化します。 |
| `HideSoon` | UIを即時非表示化します。 |
| `SwitchAsync` | UIを指定の表示状態に切り替えます。 |
| `SwitchSoon` | UIを指定の表示状態に即時切り替えます。 |
| `FlipAsync` | UIの表示/非表示状態を反転させます。 |
| `FlipSoon` | UIの表示/非表示状態を即時反転させます。 |
| `FocusUi` | 本UIをフォーカスします。 |
| `DeFocusUi` | 本UIからフォーカスを外します。 |

## 使用例

```csharp
/// <summary>
/// UIを表示します。
/// </summary>
/// <param name="token">キャンセル用のトークン</param>
public async UniTask ShowAsync(CancellationToken token)
{
    // ...
}

/// <summary>
/// UIを指定の表示状態に切り替えます。
/// </summary>
/// <param name="wantShow">表示へ切り替えるかどうか</param>
/// <param name="token">キャンセル用のトークン</param>
public async UniTask SwitchAsync(bool wantShow, CancellationToken token)
{
    // ...
}
```
