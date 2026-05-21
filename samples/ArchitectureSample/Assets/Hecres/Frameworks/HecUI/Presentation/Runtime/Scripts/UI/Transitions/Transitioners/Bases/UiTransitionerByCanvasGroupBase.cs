using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases
{
    /// <summary>
    /// キャンバスグループによるUI遷移クラスの基底
    /// </summary>
    public abstract class UiTransitionerByCanvasGroupBase : UiTransitionerBase
    {
        [SerializeField] protected CanvasGroup canvasGroup;

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        protected override void ShowSoonInherent()
        {
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        protected override void HideSoonInherent()
        {
            canvasGroup.blocksRaycasts = false;
        }
    }
}
