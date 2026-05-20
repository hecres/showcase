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
        /// 指定したステートを再生し、完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="stateName">再生するステート名</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayStateAsync(this Animator self, string stateName, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await self.PlayStateAsync(stateName, 0, token);
        }

        /// <summary>
        /// 指定したレイヤーのステートを再生し、完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="stateName">再生するステート名</param>
        /// <param name="layerIndex">レイヤーインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayStateAsync(this Animator self, string stateName, int layerIndex, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await self.PlayStateAsync(stateName, layerIndex, 0, token);
        }

        /// <summary>
        /// 指定したレイヤーのステートを指定時間位置から再生し、完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="stateName">再生するステート名</param>
        /// <param name="layerIndex">レイヤーインデックス</param>
        /// <param name="normalizedTime">正規化時間（0〜1）</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayStateAsync(this Animator self, string stateName, int layerIndex, float normalizedTime, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await self.PlayStateAsync(stateName, layerIndex, normalizedTime, 1, token);
        }

        /// <summary>
        /// 指定したレイヤーのステートを指定時間位置から再生し、指定回数のループ完了まで待機します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <param name="stateName">再生するステート名</param>
        /// <param name="layerIndex">レイヤーインデックス</param>
        /// <param name="normalizedTime">正規化時間（0〜1）</param>
        /// <param name="loopCount">待機するループ回数</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>再生完了まで待機するタスク</returns>
        public static async UniTask PlayStateAsync(this Animator self, string stateName, int layerIndex, float normalizedTime, int loopCount, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            self.Play(stateName, layerIndex, normalizedTime);

            await UniTask.WaitUntil(
                () =>
                {
                    if (ShouldStop()) return true;

                    var targetStateInfo = self.GetCurrentAnimatorStateInfo(layerIndex);
                    return targetStateInfo.IsName(stateName) && IsPlayCountReached(targetStateInfo, loopCount);
                },
                cancellationToken: token
            );
            return;

            bool ShouldStop()
            {
                return self == null || self.GetCurrentAnimatorClipInfoCount(layerIndex) <= 0;
            }
        }
    }
}
