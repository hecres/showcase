using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Bases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Interfaces;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using VContainer;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases
{
    /// <summary>
    /// シーンシーケンスのUIPresenterクラスの基底
    /// </summary>
    public abstract partial class SceneSequenceUiPresenterBase<T> : MvrpPresenterBase<T>, ISceneSequenceUiPresenter
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// イベントシステム
        /// </summary>
        [field: Inject]
        protected IHecEventSystem HecEventSystem { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// UIフォーカスシステム
        /// </summary>
        [field: Inject]
        protected IUiFocusSystem UiFocusSystem { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// シーンシーケンスUIの生成インターフェース
        /// </summary>
        [field: Inject]
        protected ISceneSequenceUiCreator SceneSequenceUiCreator { get; }
    }
}
