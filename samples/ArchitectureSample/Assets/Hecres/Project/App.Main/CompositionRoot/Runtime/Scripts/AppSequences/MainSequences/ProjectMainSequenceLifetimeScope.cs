using Hecres.Frameworks.HecApp.CompositionRoot.AppSequences.MainSequences.Bases;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.MainSequences.Managers;
using Hecres.Project.Foundation.MasterData.Infrastructure.Managers;
using Hecres.Project.Foundation.Networking.Infrastructure.Apis.Managers;
using VContainer;

namespace Hecres.Project.App.Main.CompositionRoot.AppSequences.MainSequences
{
    /// <summary>
    /// メインシーケンス内におけるオブジェクトの生成/保持/破棄サイクルを管理するDIスコープクラス
    /// </summary>
    /// <remarks>
    /// メインシーケンス管理に加え、クエスト選択処理が依存するマスターデータ取得・APIリクエストの
    /// ダミー実装をバインドします。
    /// </remarks>
    public class ProjectMainSequenceLifetimeScope : HecMainSequenceLifetimeScopeBase
    {
        /// <summary>
        /// 依存関係をバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            DummyProjectMasterDataManager.InstantiateAndBind(builder);
            DummyProjectApiManager.InstantiateAndBind(builder);
        }

        /// <summary>
        /// メインシーケンス管理の依存関係をバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        protected override void FindAndBindMainSequenceManager(IContainerBuilder builder)
        {
            ProjectMainSequenceManager.FindAndBind(builder);
        }
    }
}
