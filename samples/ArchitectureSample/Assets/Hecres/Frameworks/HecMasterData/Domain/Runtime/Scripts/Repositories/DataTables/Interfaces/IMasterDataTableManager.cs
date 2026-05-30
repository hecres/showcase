using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Frameworks.HecMasterData.Domain.Repositories.DataTables.Interfaces
{
    /// <summary>
    /// マスターデータテーブルの管理インターフェース
    /// </summary>
    public interface IMasterDataTableManager<TId, TValue> : IMasterDataRowGetter<TId, TValue>
    {
        /// <summary>
        /// マスターデータを読み込みます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込みの非同期操作</returns>
        UniTask LoadAsync(CancellationToken token);
    }
}
