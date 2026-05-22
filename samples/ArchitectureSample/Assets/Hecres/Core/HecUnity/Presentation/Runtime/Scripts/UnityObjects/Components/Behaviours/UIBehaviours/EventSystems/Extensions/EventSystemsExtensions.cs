using System;
using System.Linq;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.UIBehaviours.EventSystems.Extensions
{
    /// <summary>
    /// EventSystemの拡張クラス
    /// </summary>
    public static class EventSystemsExtensions
    {
        /// <summary>
        /// 同一選択を無視して選択中のGameObjectを設定します。
        /// </summary>
        /// <param name="self">対象のEventSystem</param>
        /// <param name="selected">選択するGameObject</param>
        public static void SetSelectedGameObjectIgnoreSame(this EventSystem self, GameObject selected)
        {
            if (selected == null) throw new ArgumentNullException(nameof(selected));

            if (selected == self.currentSelectedGameObject) return;

            self.SetSelectedGameObject(selected.gameObject);
        }

        /// <summary>
        /// 現在選択中のGameObjectが指定Transformの子孫かどうかを判定します。
        /// </summary>
        /// <param name="self">対象のEventSystem</param>
        /// <param name="parent">親となるTransform</param>
        /// <returns>true: 子孫である / false: 子孫ではない</returns>
        public static bool IsSelectedChildOf(this EventSystem self, Transform parent)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            return self.currentSelectedGameObject != null && self.currentSelectedGameObject.transform.IsChildOf(parent);
        }

        /// <summary>
        /// 指定したGameObject配下のSelectableを選択します。
        /// </summary>
        /// <param name="self">対象のEventSystem</param>
        /// <param name="selected">選択対象のGameObject</param>
        public static void SetSelectedGameObjectInChildren(this EventSystem self, GameObject selected)
        {
            if (selected == null) throw new ArgumentNullException(nameof(selected));

            if (selected.HasComponent<Selectable>())
            {
                self.SetSelectedGameObjectIgnoreSame(selected);
                return;
            }

            var selectables = selected.GetComponentsInChildren<Selectable>();
            if (selectables.Length > 1) throw new InvalidOperationException();

            self.SetSelectedGameObjectIgnoreSame(selectables.First().gameObject);
        }

        /// <summary>
        /// 指定したComponent配下のSelectableを選択します。
        /// </summary>
        /// <param name="self">対象のEventSystem</param>
        /// <param name="selected">選択対象のComponent</param>
        public static void SetSelectedGameObjectInChildren(this EventSystem self, Component selected)
        {
            if (selected == null) throw new ArgumentNullException(nameof(selected));

            self.SetSelectedGameObjectInChildren(selected.gameObject);
        }
    }
}
