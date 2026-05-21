using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners
{
    /// <summary>
    /// キャンバスグループによる即時のUI遷移クラス
    /// </summary>
    public class UiTransitionerByCanvasGroupSoon : UiTransitionerByCanvasGroupBase
    {
        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask ShowAsyncInherent(CancellationToken token) => ShowSoonInherent();

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        protected override void ShowSoonInherent()
        {
            base.ShowSoonInherent();

            canvasGroup.alpha = 1;
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask HideAsyncInherent(CancellationToken token) => HideSoonInherent();

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        protected override void HideSoonInherent()
        {
            base.HideSoonInherent();

            canvasGroup.alpha = 0;
        }
    }
}
