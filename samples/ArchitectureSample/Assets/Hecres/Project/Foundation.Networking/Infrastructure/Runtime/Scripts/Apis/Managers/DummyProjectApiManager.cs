using System;
using Hecres.Project.Foundation.Networking.Domain.Repositories.Apis.Managers.Interfaces;
using Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Quests;
using VContainer;

namespace Hecres.Project.Foundation.Networking.Infrastructure.Apis.Managers
{
    /// <summary>
    /// プロジェクトAPIの管理クラス（ダミー実装）
    /// </summary>
    /// <remarks>
    /// 本サンプルではバックエンド通信の代替として、各API疎通クラスが固定データを返却します。
    /// </remarks>
    public partial class DummyProjectApiManager : IProjectApiRequester
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DummyProjectApiManager()
        {
            getQuestList = new DummyGetQuestList();
            sendQuestReport = new DummySendQuestReport();
        }

        /// <summary>
        /// このクラスのインスタンスを生成し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>生成されたインスタンス</returns>
        public static DummyProjectApiManager InstantiateAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = new DummyProjectApiManager();
            builder.RegisterInstance(instance).As<IProjectApiRequester>();

            return instance;
        }
    }
}
