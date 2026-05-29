# 暗号化ラッパー テンプレート

暗号化型（`CryptedStringBase` / `CryptedIntBase`）を継承するクラスの固定コメント。
改変せずそのまま使用すること。

## クラス summary

「〜クラスの基底」（基底クラスの通常ルールに従う）

## メソッド

### `IsValid` メソッド

```csharp
/// <summary>
/// 値が有効かどうかを返します。
/// </summary>
/// <param name="plainValue">検証対象（平文）</param>
/// <returns>true: 有効 / false: 無効</returns>
protected override bool IsValid(string plainValue)
```

### `ProcessPlainValue` メソッド

```csharp
/// <summary>
/// 入力値を前処理します。
/// </summary>
/// <param name="plainValue">処理対象（平文）</param>
/// <returns>前処理後の値</returns>
protected override string ProcessPlainValue(string plainValue)
```
