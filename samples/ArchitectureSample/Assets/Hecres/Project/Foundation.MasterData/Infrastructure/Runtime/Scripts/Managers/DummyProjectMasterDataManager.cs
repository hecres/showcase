using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecMasterData.Domain.Repositories.Managers.Interfaces;
using Hecres.Project.Foundation.MasterData.Domain.Repositories.Managers.Interfaces;
using Hecres.Project.Foundation.MasterData.Infrastructure.DataTables;
using VContainer;

namespace Hecres.Project.Foundation.MasterData.Infrastructure.Managers
{
    /// <summary>
    /// プロジェクトマスターデータの管理クラス（ダミー実装）
    /// </summary>
    /// <remarks>
    /// 本サンプルではマスターデータCSVの代替として、各テーブルがコンストラクタで固定データを保持します。
    /// </remarks>
    public partial class DummyProjectMasterDataManager : IProjectMasterDataGetter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DummyProjectMasterDataManager()
        {
            questMasterDataTable = new DummyQuestMasterDataTable();
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <remarks>本サンプルでは何も行いません。</remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期操作</returns>
        public UniTask InitializeAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// すべてのマスターデータテーブルを読み込みます。
        /// </summary>
        /// <remarks>
        /// 本サンプルでは各テーブルがコンストラクタで構築済みのため、 <see cref="IMasterDataTableManager{TId, TValue}.LoadAsync"/> は no-op です。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込み処理の非同期操作</returns>
        public async UniTask LoadAllDataTableAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await UniTask.WhenAll(
                questMasterDataTable.LoadAsync(token)
            );
        }

        /// <summary>
        /// このクラスのインスタンスを生成し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>生成されたインスタンス</returns>
        public static DummyProjectMasterDataManager InstantiateAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = new DummyProjectMasterDataManager();
            builder.RegisterInstance(instance)
                   .As<IMasterDataManager>()
                   .As<IProjectMasterDataGetter>()
                   .As<IQuestMasterDataGetter>();

            return instance;
        }
    }
}
