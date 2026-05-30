namespace Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Errors.ErrorTypes
{
    /// <summary>
    /// APIリクエストエラータイプの列挙体
    /// </summary>
    public enum HecApiErrorType
    {
        /// <summary>
        /// Hecres実装のクライアント処理で発生したエラー
        /// </summary>
        HecClientHandling = 1000,

        /// <summary>
        /// Hecres実装のローカルセーブ処理で発生したエラー
        /// </summary>
        HecLocalSaveHandling = 2000
    }
}
