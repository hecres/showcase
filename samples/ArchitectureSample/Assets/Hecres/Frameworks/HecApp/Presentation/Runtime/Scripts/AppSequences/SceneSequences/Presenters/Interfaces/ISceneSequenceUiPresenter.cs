using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.HierarchySystems.Interfaces;
using R3;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Interfaces
{
    /// <summary>
    /// シーンシーケンスのUIPresenterインターフェース
    /// </summary>
    /// <remarks>
    /// 型指定なしにシーンシーケンスUIPresenterをFindするため用いる想定です。
    /// </remarks>
    public interface ISceneSequenceUiPresenter : IMvrpPresenter, IFocusableUi, IHierarchicalUi
    {
        /// <summary>
        /// シーンシーケンスの終了リクエスト時に通知
        /// </summary>
        Observable<Unit> BackSceneSequenceRequested { get; }
    }
}
