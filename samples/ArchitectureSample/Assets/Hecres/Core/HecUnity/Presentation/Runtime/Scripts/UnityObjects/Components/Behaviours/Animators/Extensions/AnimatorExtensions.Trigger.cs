using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.Animators.Extensions
{
    /// <summary>
    /// Animatorの拡張クラス
    /// </summary>
    public static partial class AnimatorExtensions
    {
        /// <summary>
        /// 指定トリガーを発火してステートを再生し、完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="triggerName">発火するトリガー名</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayByTriggerAsync(this Animator self, string triggerName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await self.PlayByTriggerAsync(triggerName, 0, token);
        }

        /// <summary>
        /// 指定トリガーを発火して指定レイヤーのステートを再生し、完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="triggerName">発火するトリガー名</param>
        /// <param name="layerIndex">レイヤーインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayByTriggerAsync(this Animator self, string triggerName, int layerIndex, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var startStateInfo = self.GetCurrentAnimatorStateInfo(layerIndex);
            self.SetTrigger(triggerName);

            // ステート遷移するまで待機
            await UniTask.WaitUntil(
                () =>
                {
                    if (ShouldStop()) return true;

                    var targetStateInfo = self.GetCurrentAnimatorStateInfo(layerIndex);
                    return startStateInfo.shortNameHash != targetStateInfo.shortNameHash;
                },
                cancellationToken: token
            );

            // 再生条件を満たせなくなっていた場合は途中切り上げ
            if (ShouldStop()) return;

            // 1ループ完了するまで待機
            var nextStateInfo = self.GetCurrentAnimatorStateInfo(layerIndex);
            await UniTask.WaitUntil(
                () =>
                {
                    if (ShouldStop()) return true;

                    // 遷移先からさらに遷移していた場合は中止、そうでなければ1ループ完了まで待つ
                    var targetStateInfo = self.GetCurrentAnimatorStateInfo(layerIndex);
                    return nextStateInfo.shortNameHash != targetStateInfo.shortNameHash || IsPlayCountReached(targetStateInfo, 1);
                },
                cancellationToken: token
            );
            return;

            bool ShouldStop()
            {
                return self == null || !self.gameObject.activeInHierarchy || !self.enabled || self.GetCurrentAnimatorClipInfoCount(layerIndex) <= 0;
            }
        }

        /// <summary>
        /// すべてのトリガーのフラグをリセットします。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        public static void ResetAllTriggers(this Animator self)
        {
            foreach (var item in self.parameters)
            {
                if (item.type == AnimatorControllerParameterType.Trigger)
                {
                    self.ResetTrigger(item.name);
                }
            }
        }
    }
}
