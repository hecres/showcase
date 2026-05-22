using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions
{
    /// <summary>
    /// RectTransformの拡張クラス
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// 親のRectTransform全体を満たすようにサイズと姿勢を設定します。
        /// </summary>
        /// <param name="self">対象のRectTransform</param>
        public static void FillParentFluid(this RectTransform self)
        {
            self.anchorMin = Vector2.zero;
            self.anchorMax = Vector2.one;
            self.offsetMin = Vector2.zero;
            self.offsetMax = Vector2.zero;
            self.pivot = new Vector2(0.5f, 0.5f);
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
            self.SetLocalPositionZ(0f);
        }

        /// <summary>
        /// 指定した親に設定した上で、親のRectTransform全体を満たすようにサイズと勢を設定します。
        /// </summary>
        /// <param name="self">対象のRectTransform</param>
        /// <param name="parent">設定する親Transform</param>
        /// <param name="worldPositionStays">ワールド座標を維持するかどうか</param>
        public static void FillParentFluid(this RectTransform self, Transform parent, bool worldPositionStays = true)
        {
            self.SetParent(parent, worldPositionStays);
            self.FillParentFluid();
        }
    }
}
