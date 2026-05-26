using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecMasterData.Domain.Entities.DataRows.Interfaces;
using Hecres.Frameworks.HecMasterData.Domain.Repositories.DataTables.Interfaces;
using Hecres.Frameworks.HecMasterData.Domain.ValueObjects.DataTables.DataTypes;

namespace Hecres.Frameworks.HecMasterData.Infrastructure.DataTables.Bases
{
    /// <summary>
    /// マスターデータテーブルの管理クラスの基底
    /// </summary>
    /// <remarks>
    /// 本サンプルではJSON/Loaderを介さず、派生クラスがコンストラクタで <see cref="pairs"/> を初期化する設計とします。
    /// </remarks>
    public abstract class MasterDataTableBase<TId, TValue> : IMasterDataTableManager<TId, TValue>
        where TValue : class, IMasterDataRow<TId>
    {
        /// <summary>
        /// マスターデータのテーブル名
        /// </summary>
        protected MasterDataTableName TableName { get; } = new(typeof(TValue).Name);

        /// <summary>
        /// データテーブル本体
        /// </summary>
        /// <remarks>派生クラスがコンストラクタで初期化します。</remarks>
        protected IReadOnlyDictionary<TId, TValue> pairs;

        /// <summary>
        /// マスターデータを読み込みます。
        /// </summary>
        /// <remarks>
        /// 本サンプルでは派生クラスのコンストラクタで <see cref="pairs"/> がすでに構築されているため、何も行いません。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込みの非同期操作</returns>
        public virtual UniTask LoadAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 指定キーに対応するデータが存在するかどうかを取得します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>true: データが存在する / false: データが存在しない</returns>
        public bool ContainsKey(TId key)
        {
            return key != null && pairs.ContainsKey(key);
        }

        /// <summary>
        /// 指定キーに対応するデータを取得します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>取得したデータ</returns>
        public virtual TValue GetValue(TId key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            return pairs[key];
        }

        /// <summary>
        /// 指定キーに対応するデータを取得します。
        /// </summary>
        /// <remarks>keyがnullの場合はnullを返します。</remarks>
        /// <param name="key">キー</param>
        /// <returns>取得したデータ</returns>
        public virtual TValue GetValueCanBeNullKey(TId key)
        {
            return key == null ? null : pairs[key];
        }

        /// <summary>
        /// KeyValueのペアすべてを取得します。
        /// </summary>
        /// <returns>全てのペア</returns>
        public IEnumerable<KeyValuePair<TId, TValue>> GetPairs() => pairs;
    }
}
