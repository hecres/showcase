using System;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems
{
    /// <summary>
    /// UI選択状態の切り替えクラス
    /// </summary>
    public class UiSelectionSwitcher
    {
        private readonly IHecEventSystem eventSystem;
        private readonly GameObject gameObject;
        private readonly Action focusFirstAction;
        private readonly IFocusableUi focusableUi;

        private UiSelectionHistory uiSelectionHistory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UiSelectionSwitcher(IHecEventSystem eventSystem, GameObject gameObject, Action focusFirstAction)
        {
            this.eventSystem = eventSystem ?? throw new ArgumentNullException(nameof(eventSystem));
            this.gameObject = gameObject != null ? gameObject : throw new ArgumentNullException(nameof(gameObject));
            this.focusFirstAction = focusFirstAction ?? throw new ArgumentNullException(nameof(focusFirstAction));

            focusableUi = gameObject.GetComponentSafely<IFocusableUi>();
        }

        /// <summary>
        /// 選択状態にします。
        /// </summary>
        public void ActivateSelection()
        {
            if (uiSelectionHistory?.SelectedGameObject == null)
            {
                uiSelectionHistory?.RevertNavigations();
                focusFirstAction();
                return;
            }

            uiSelectionHistory?.RevertNavigations();
        }

        /// <summary>
        /// 選択状態を解除します。
        /// </summary>
        public void DeactivateSelection()
        {
            uiSelectionHistory = new UiSelectionHistory(eventSystem, gameObject);

            var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
            foreach (var item in selectables)
            {
                var parentFocusableUi = item.GetComponentInParent<IFocusableUi>();
                if (parentFocusableUi == focusableUi)
                {
                    item.navigation = new Navigation { mode = Navigation.Mode.None };
                }
            }

            var focusableUis = gameObject.GetComponentsInChildren<IFocusableUi>();
            foreach (var item in focusableUis)
            {
                if (item != focusableUi)
                {
                    item.DeFocusUi();
                }
            }
        }
    }
}
