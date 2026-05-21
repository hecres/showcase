using Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers
{
    /// <summary>
    /// シーンシーケンスの管理クラスの基底
    /// </summary>
    /// <remarks>
    /// 本サンプルでは API リクエストやマスターデータ取得等のプロジェクト固有依存は持たず、
    /// フレームワーク基底クラスの機能のみで動作します。
    /// </remarks>
    /// <typeparam name="TArgs">シーンシーケンス管理引数の型</typeparam>
    /// <typeparam name="TModel">シーケンスModelの型</typeparam>
    /// <typeparam name="TUiPresenter">シーケンスUIPresenterの型</typeparam>
    public abstract class ProjectSceneSequenceManagerBase<TArgs, TModel, TUiPresenter> : SceneSequenceManagerBase<TArgs, TModel, TUiPresenter>
        where TArgs : ProjectSceneSequenceManagerArgsBase
        where TModel : HecSceneSequenceBase
        where TUiPresenter : ISceneSequenceUiPresenter
    {
    }
}
