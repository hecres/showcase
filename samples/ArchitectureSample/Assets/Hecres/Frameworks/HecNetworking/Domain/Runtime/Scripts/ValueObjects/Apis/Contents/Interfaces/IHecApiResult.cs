using Hecres.Core.HecError;

namespace Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Interfaces
{
    /// <summary>
    /// APIリクエスト結果の状態取得インターフェース
    /// </summary>
    public interface IHecApiResult
    {
        /// <summary>
        /// APIリクエストが成功したかどうか
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// APIリクエストエラー
        /// </summary>
        /// <remarks>エラーが発生していない場合はnullを返します。</remarks>
        HecError Error { get; }
    }
}
