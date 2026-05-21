using System;
using System.Linq;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using UnityEngine;
using VContainer;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems
{
    /// <summary>
    /// UIフォーカスを管理するクラス
    /// </summary>
    public class UiFocusSystem : IUiFocusSystem
    {
        /// <summary>
        /// 現在のフォーカス対象UI
        /// </summary>
        public IFocusableUi CurrentTarget { get; private set; }

        private readonly IHecEventSystem eventSystem;
        private readonly IUiSearcher uiSearcher;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="eventSystem">イベントシステムインターフェース</param>
        /// <param name="uiSearcher">UI検索インターフェース</param>
        private UiFocusSystem(IHecEventSystem eventSystem, IUiSearcher uiSearcher)
        {
            this.eventSystem = eventSystem ?? throw new ArgumentNullException(nameof(eventSystem));
            this.uiSearcher = uiSearcher ?? throw new ArgumentNullException(nameof(uiSearcher));
        }

        /// <summary>
        /// 指定UIをフォーカス中かどうか判別します。
        /// </summary>
        /// <param name="target">調査対象UI</param>
        /// <returns>true: フォーカス中 / false: フォーカスしていない</returns>
        public bool IsFocus(IFocusableUi target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (target == CurrentTarget)
            {
                return true;
            }

            var children = CurrentTarget.gameObject.GetComponentsInChildren<IFocusableUi>(true);
            return children.Any(item => item == target);
        }

        /// <summary>
        /// 現在のフォーカス対象UIへのフォーカスを解除します。
        /// </summary>
        public void DeFocus()
        {
            string deFocusUiObjectName;
            if (CurrentTarget == null)
            {
                deFocusUiObjectName = "none";
            }
            else if (CurrentTarget.IsDestroyed)
            {
                deFocusUiObjectName = "destroyed";
            }
            else
            {
                deFocusUiObjectName = CurrentTarget.gameObject.name;
            }

            Debug.Log($"{GetType().Name} - DeFocusUi: {deFocusUiObjectName}");

            if (CurrentTarget != null && !CurrentTarget.IsDestroyed)
            {
                CurrentTarget.DeFocusUi();
            }

            CurrentTarget = null;
            eventSystem.SetSelectedGameObject(null);
        }

        /// <summary>
        /// 指定UIをフォーカスします。
        /// </summary>
        /// <param name="target">フォーカス対象UI</param>
        public void Focus(IFocusableUi target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            if (target == CurrentTarget) return;

            DeFocus();
            CurrentTarget = target;

            Debug.Log($"{GetType().Name} - FocusUi: {CurrentTarget.gameObject.name}");
            CurrentTarget.FocusUi();
        }

        /// <summary>
        /// 現在のUI配置状態に基づいてフォーカス状態を更新します。
        /// </summary>
        /// <remarks>
        /// LayerableCanvases管理上もっとも前面に配置されているアクティブなUIをフォーカス対象とします。
        /// </remarks>
        public void UpdateFocus()
        {
            var frontMostFocusableUi = uiSearcher.GetFrontMostComponent<IFocusableUi>(false);
            if (frontMostFocusableUi != null)
            {
                Focus(frontMostFocusableUi);
            }
        }

        /// <summary>
        /// このクラスのインスタンスを生成し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <param name="eventSystem">イベントシステムインターフェース</param>
        /// <param name="uiSearcher">UI検索インターフェース</param>
        /// <returns>生成されたインスタンス</returns>
        public static UiFocusSystem InstantiateAndBind(IContainerBuilder builder, IHecEventSystem eventSystem, IUiSearcher uiSearcher)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (eventSystem == null) throw new ArgumentNullException(nameof(eventSystem));
            if (uiSearcher == null) throw new ArgumentNullException(nameof(uiSearcher));

            var instance = new UiFocusSystem(eventSystem, uiSearcher);
            builder.RegisterInstance(instance).As<IUiFocusSystem>();

            return instance;
        }
    }
}
