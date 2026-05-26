namespace Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Errors.ErrorNumbers
{
    /// <summary>
    /// Hecres実装のクライアント処理で発生したエラー番号の列挙体
    /// </summary>
    public enum HecClientErrorNumber
    {
        /// <summary>
        /// APIリクエストがタイムアウトした
        /// </summary>
        /// <remarks>
        /// APIリクエスト送信後、設定時間応答がなかった場合のエラーです。エラー原因としては、サーバーがダウンしている、ネットワークが不安定などが考えられます。
        /// </remarks>
        ApiRequestTimeout = 1
    }
}
