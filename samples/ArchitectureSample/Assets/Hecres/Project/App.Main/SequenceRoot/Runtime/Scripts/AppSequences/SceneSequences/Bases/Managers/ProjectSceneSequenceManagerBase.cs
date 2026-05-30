using Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases;
using Hecres.Project.Foundation.MasterData.Domain.Repositories.Managers.Interfaces;
using Hecres.Project.Foundation.Networking.Domain.Repositories.Apis.Managers.Interfaces;
using VContainer;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers
{
    /// <summary>
    /// シーンシーケンスの管理クラスの基底
    /// </summary>
    /// <remarks>
    /// API リクエストやマスターデータ取得など、各シーンシーケンス管理クラスで共通利用するプロジェクト固有依存を集約します。
    /// </remarks>
    /// <typeparam name="TArgs">シーンシーケンス管理引数の型</typeparam>
    /// <typeparam name="TModel">シーケンスModelの型</typeparam>
    /// <typeparam name="TUiPresenter">シーケンスUIPresenterの型</typeparam>
    public abstract class ProjectSceneSequenceManagerBase<TArgs, TModel, TUiPresenter> : SceneSequenceManagerBase<TArgs, TModel, TUiPresenter>
        where TArgs : ProjectSceneSequenceManagerArgsBase
        where TModel : HecSceneSequenceBase
        where TUiPresenter : ISceneSequenceUiPresenter
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// APIのリクエストインターフェース
        /// </summary>
        [field: Inject]
        protected IProjectApiRequester ApiRequester { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// マスターデータの取得インターフェース
        /// </summary>
        [field: Inject]
        protected IProjectMasterDataGetter MasterDataGetter { get; }
    }
}
