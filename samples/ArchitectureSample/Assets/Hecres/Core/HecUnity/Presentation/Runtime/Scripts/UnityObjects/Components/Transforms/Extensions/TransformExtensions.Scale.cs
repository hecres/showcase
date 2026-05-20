using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions
{
    /// <summary>
    /// Transformの拡張クラス
    /// </summary>
    public static partial class TransformExtensions
    {
        /// <summary>
        /// ローカルスケールのX値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">設定するX値</param>
        public static void SetLocalScaleX(this Transform self, float x)
        {
            var newLocalScale = self.localScale;
            newLocalScale.x = x;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ローカルスケールのY値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">設定するY値</param>
        public static void SetLocalScaleY(this Transform self, float y)
        {
            var newLocalScale = self.localScale;
            newLocalScale.y = y;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ローカルスケールのZ値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">設定するZ値</param>
        public static void SetLocalScaleZ(this Transform self, float z)
        {
            var newLocalScale = self.localScale;
            newLocalScale.z = z;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ローカルスケールのX値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">加算するX値</param>
        public static void AddLocalScaleX(this Transform self, float x)
        {
            var newLocalScale = self.localScale;
            newLocalScale.x += x;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ローカルスケールのY値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">加算するY値</param>
        public static void AddLocalScaleY(this Transform self, float y)
        {
            var newLocalScale = self.localScale;
            newLocalScale.y += y;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ローカルスケールのZ値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">加算するZ値</param>
        public static void AddLocalScaleZ(this Transform self, float z)
        {
            var newLocalScale = self.localScale;
            newLocalScale.z += z;
            self.localScale = newLocalScale;
        }

        /// <summary>
        /// ワールド空間での見かけのスケール（lossyScale）を指定値に調整します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="value">設定するワールドスケール</param>
        public static void SetLossyScale(this Transform self, Vector3 value)
        {
            var previousLocalScale = self.localScale;
            var previousLossyScale = self.lossyScale;
            self.localScale = new Vector3(
                previousLocalScale.x / previousLossyScale.x * value.x,
                previousLocalScale.y / previousLossyScale.y * value.y,
                previousLocalScale.z / previousLossyScale.z * value.z
            );
        }
    }
}
