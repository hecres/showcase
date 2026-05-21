using System;
using System.Collections.Generic;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems
{
    /// <summary>
    /// UI選択状態の履歴クラス
    /// </summary>
    public class UiSelectionHistory
    {
        /// <summary>
        /// 選択中のGameObject
        /// </summary>
        public GameObject SelectedGameObject { get; }

        private readonly IHecEventSystem eventSystem;
        private readonly Dictionary<Selectable, Navigation> navigationTable = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UiSelectionHistory(IHecEventSystem eventSystem, GameObject gameObject)
        {
            this.eventSystem = eventSystem ?? throw new ArgumentNullException(nameof(eventSystem));

            if (gameObject == null) throw new ArgumentNullException(nameof(gameObject));

            if (!gameObject.HasComponent<IFocusableUi>()) throw new ArgumentException();

            var selectables = gameObject.GetComponentsInChildren<Selectable>();
            foreach (var item in selectables)
            {
                navigationTable.Add(item, item.navigation);
            }

            var currentSelectedGameObject = eventSystem.CurrentSelectedGameObject;
            if (currentSelectedGameObject != null)
            {
                var parentFocusableUi = currentSelectedGameObject.GetComponentInParent<IFocusableUi>(true);
                if (parentFocusableUi == gameObject.GetComponentSafely<IFocusableUi>())
                {
                    SelectedGameObject = currentSelectedGameObject;
                }
            }
        }

        /// <summary>
        /// Selectableのナビゲーション情報を復旧します。
        /// </summary>
        public void RevertNavigations()
        {
            foreach (var item in navigationTable)
            {
                item.Key.navigation = item.Value;
            }

            eventSystem.SetSelectedGameObject(SelectedGameObject);
        }
    }
}
