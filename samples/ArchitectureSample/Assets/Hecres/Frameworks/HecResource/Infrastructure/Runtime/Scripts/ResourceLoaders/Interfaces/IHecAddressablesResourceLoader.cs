using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecResource.Domain.Repositories.ResourceLoaders.Interfaces;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Hecres.Frameworks.HecResource.Infrastructure.ResourceLoaders.Interfaces
{
    /// <summary>
    /// リソース取得機能を提供するインターフェース
    /// </summary>
    public interface IHecAddressablesResourceLoader : IHecResourceLoader
    {
        /// <summary>
        /// 指定ロケーションのアセットを読み込みます。
        /// </summary>
        /// <param name="location">読み込むロケーション</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込まれたアセット</returns>
        UniTask<TObject> LoadAssetAsync<TObject>(IResourceLocation location, CancellationToken token);
    }
}
