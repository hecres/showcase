using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Frameworks.HecResource.Domain.Repositories.ResourceLoaders.Interfaces
{
    /// <summary>
    /// リソース取得機能を提供するインターフェース
    /// </summary>
    public interface IHecResourceLoader
    {
        /// <summary>
        /// 指定アドレスのアセットが存在するかどうか確認します。
        /// </summary>
        /// <param name="address">確認するアドレス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>true: 存在する / false: 存在しない</returns>
        UniTask<bool> ExistsAssetAsync(string address, CancellationToken token);

        /// <summary>
        /// 指定アドレスのアセットを読み込みます。
        /// </summary>
        /// <param name="address">読み込むアドレス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込まれたアセット</returns>
        UniTask<TObject> LoadAssetAsync<TObject>(string address, CancellationToken token);
    }
}
