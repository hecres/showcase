using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.Animators.Extensions
{
    /// <summary>
    /// Animatorの拡張クラス
    /// </summary>
    public static partial class AnimatorExtensions
    {
        /// <summary>
        /// ベースレイヤーの現在のAnimatorStateInfoを取得します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <returns>ベースレイヤーの現在のAnimatorStateInfo</returns>
        public static AnimatorStateInfo GetBaseLayerCurrentAnimatorStateInfo(this Animator self) => self.GetCurrentAnimatorStateInfo(0);

        /// <summary>
        /// ベースレイヤーの現在のクリップ情報数を取得します。
        /// </summary>
        /// <param name="self">対象のAnimator</param>
        /// <returns>クリップ情報の数</returns>
        public static int GetBaseLayerCurrentAnimatorClipInfoCount(this Animator self) => self.GetCurrentAnimatorClipInfoCount(0);

        /// <summary>
        /// 再生回数が指定回数に達したかどうかを判定します。
        /// </summary>
        /// <param name="targetStateInfo">判定対象のステート情報</param>
        /// <param name="loopCount">判定するループ回数</param>
        /// <returns>true: 指定回数に達している / false: 指定回数に達していない</returns>
        private static bool IsPlayCountReached(AnimatorStateInfo targetStateInfo, int loopCount) => targetStateInfo.normalizedTime > loopCount;
    }
}
