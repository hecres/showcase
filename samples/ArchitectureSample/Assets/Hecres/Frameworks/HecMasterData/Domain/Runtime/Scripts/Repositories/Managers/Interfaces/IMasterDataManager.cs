using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Frameworks.HecMasterData.Domain.Repositories.Managers.Interfaces
{
    /// <summary>
    /// マスターデータの管理インターフェース
    /// </summary>
    public interface IMasterDataManager
    {
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期操作</returns>
        UniTask InitializeAsync(CancellationToken token);

        /// <summary>
        /// すべてのマスターデータテーブルを読み込みます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期操作</returns>
        UniTask LoadAllDataTableAsync(CancellationToken token);
    }
}
