using System.Collections.Generic;

namespace Hecres.Frameworks.HecMasterData.Domain.Repositories.DataTables.Interfaces
{
    /// <summary>
    /// マスターデータ行データの取得インターフェース
    /// </summary>
    public interface IMasterDataRowGetter<TId, TValue>
    {
        /// <summary>
        /// 指定キーに対応するデータが存在するかどうかを取得します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>true: データが存在する / false: データが存在しない</returns>
        bool ContainsKey(TId key);

        /// <summary>
        /// 指定キーに対応するデータを取得します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>取得したデータ</returns>
        TValue GetValue(TId key);

        /// <summary>
        /// 指定キーに対応するデータを取得します。
        /// </summary>
        /// <remarks>keyがnullの場合はnullを返します。</remarks>
        /// <param name="key">キー</param>
        /// <returns>取得したデータ</returns>
        TValue GetValueCanBeNullKey(TId key);

        /// <summary>
        /// KeyValueのペアすべてを取得します。
        /// </summary>
        /// <returns>全てのペア</returns>
        IEnumerable<KeyValuePair<TId, TValue>> GetPairs();
    }
}
