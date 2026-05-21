using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.Animators.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners
{
    /// <summary>
    /// AnimatorによるUI遷移クラス
    /// </summary>
    public class UiTransitionerByAnimator : UiTransitionerByCanvasGroupBase
    {
        [SerializeField] private Animator animator;

        [SerializeField] private string hideSoonTrigger = "HideSoon";
        [SerializeField] private string hideTrigger = "Hide";
        [SerializeField] private string showSoonTrigger = "ShowSoon";
        [SerializeField] private string showTrigger = "Show";

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        private void Awake()
        {
            animator.keepAnimatorStateOnDisable = true;
        }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask ShowAsyncInherent(CancellationToken token)
        {
            base.ShowSoonInherent();

            await PlayByTriggerAsync(showTrigger, token);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        protected override void ShowSoonInherent()
        {
            base.ShowSoonInherent();

            PlayByTriggerAsync(showSoonTrigger, destroyCancellationToken).Forget();
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask HideAsyncInherent(CancellationToken token)
        {
            base.HideSoonInherent();

            await PlayByTriggerAsync(hideTrigger, token);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        protected override void HideSoonInherent()
        {
            base.HideSoonInherent();

            PlayByTriggerAsync(hideSoonTrigger, destroyCancellationToken).Forget();
        }

        /// <summary>
        /// トリガーを用いてアニメーションを再生します。
        /// </summary>
        /// <param name="triggerName">再生するトリガー名</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        private async UniTask PlayByTriggerAsync(string triggerName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            // Transitionに関係するトリガーのみリセットする
            animator.ResetTrigger(showTrigger);
            animator.ResetTrigger(hideTrigger);
            animator.ResetTrigger(showSoonTrigger);
            animator.ResetTrigger(hideSoonTrigger);

            await animator.PlayByTriggerAsync(triggerName, token);
        }
    }
}
