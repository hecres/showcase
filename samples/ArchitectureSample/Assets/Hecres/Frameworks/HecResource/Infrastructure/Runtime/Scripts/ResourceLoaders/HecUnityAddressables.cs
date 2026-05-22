using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecResource.Infrastructure.ResourceLoaders.Interfaces;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Hecres.Frameworks.HecResource.Infrastructure.ResourceLoaders
{
    /// <summary>
    /// 独自にカスタマイズしたAddressablesクラス
    /// </summary>
    public class HecUnityAddressables : IHecAddressablesResourceLoader
    {
        /// <summary>
        /// 指定アドレスのアセットが存在するかどうか確認します。
        /// </summary>
        /// <param name="address">確認するアドレス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>true: 存在する / false: 存在しない</returns>
        public async UniTask<bool> ExistsAssetAsync(string address, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var handle = new AsyncOperationHandle<IList<IResourceLocation>>();
            try
            {
                handle = Addressables.LoadResourceLocationsAsync(address);
                var location = await handle.WithCancellation(token);
                var exists = location != null && location.Any();
                return exists;
            }
            finally
            {
                // Location読み込みでもReleaseが必要
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
        }

        /// <summary>
        /// 指定アドレスのアセットを読み込みます。
        /// </summary>
        /// <param name="address">読み込むアドレス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込まれたアセット</returns>
        public async UniTask<TObject> LoadAssetAsync<TObject>(string address, CancellationToken token)
        {
            // Unity提供のAddressables.LoadAssetAsyncは内部の例外がtry-catchできない
            if (!await ExistsAssetAsync(address, token))
            {
                throw new InvalidKeyException(address, typeof(string));
            }

            var result = await Addressables.LoadAssetAsync<TObject>(address).WithCancellation(token);
            return result;
        }

        /// <summary>
        /// 指定ロケーションのアセットを読み込みます。
        /// </summary>
        /// <param name="location">読み込むロケーション</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込まれたアセット</returns>
        public async UniTask<TObject> LoadAssetAsync<TObject>(IResourceLocation location, CancellationToken token)
        {
            var result = await Addressables.LoadAssetAsync<TObject>(location).WithCancellation(token);
            return result;
        }
    }
}
