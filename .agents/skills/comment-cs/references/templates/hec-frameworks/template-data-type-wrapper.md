# 型ラッパー テンプレート

型ラップ基底（`StringDataTypeWrapperBase` / `IntDataTypeWrapperBase`）を継承するクラスの固定コメント。
改変せずそのまま使用すること。

## クラス summary

「〜の型クラス」

## メソッド

### `IsValid` メソッド

```csharp
/// <summary>
/// 値が有効かどうかを返します。
/// </summary>
/// <param name="value">検証対象</param>
/// <returns>true: 有効 / false: 無効</returns>
public static bool IsValid(string value)
```

### `NormalizeValue` メソッド

```csharp
/// <summary>
/// 入力値を前処理します。
/// </summary>
/// <param name="value">処理対象</param>
/// <returns>前処理後の値</returns>
private static string NormalizeValue(string value)
```
