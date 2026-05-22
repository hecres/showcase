using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases
{
    public partial class SceneSequenceUiPresenterBase<T>
    {
        /// <summary>
        /// UI選択状態の切り替え器
        /// </summary>
        private UiSelectionSwitcher UiSelectionSwitcher => uiSelectionSwitcherCache ??= new UiSelectionSwitcher(HecEventSystem, gameObject, FocusUiFirst);

        private UiSelectionSwitcher uiSelectionSwitcherCache;

        /// <summary>
        /// 初回にフォーカスすべきUIを設定します。
        /// </summary>
        protected virtual void FocusUiFirst()
        {
        }

        /// <summary>
        /// 本UIをフォーカスします。
        /// </summary>
        public void FocusUi() => UiSelectionSwitcher.ActivateSelection();

        /// <summary>
        /// 本UIからフォーカスを外します。
        /// </summary>
        public void DeFocusUi() => UiSelectionSwitcher.DeactivateSelection();
    }
}
