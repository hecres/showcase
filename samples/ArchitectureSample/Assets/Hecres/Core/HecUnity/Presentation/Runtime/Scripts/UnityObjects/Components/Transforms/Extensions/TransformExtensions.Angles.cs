using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions
{
    /// <summary>
    /// Transformの拡張クラス
    /// </summary>
    public static partial class TransformExtensions
    {

        /// <summary>
        /// ワールド回転のX角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">設定する角度</param>
        public static void SetEulerAngleX(this Transform self, float x)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.x = x;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転のY角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">設定する角度</param>
        public static void SetEulerAngleY(this Transform self, float y)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.y = y;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転のZ角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">設定する角度</param>
        public static void SetEulerAngleZ(this Transform self, float z)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.z = z;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転に角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="addAngles">加算する角度</param>
        public static void AddEulerAngles(this Transform self, Vector3 addAngles)
        {
            var newEulerAngles = self.eulerAngles + addAngles;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転のX角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">加算する角度</param>
        public static void AddEulerAngleX(this Transform self, float x)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.x += x;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転のY角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">加算する角度</param>
        public static void AddEulerAngleY(this Transform self, float y)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.y += y;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ワールド回転のZ角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">加算する角度</param>
        public static void AddEulerAngleZ(this Transform self, float z)
        {
            var newEulerAngles = self.eulerAngles;
            newEulerAngles.z += z;
            self.eulerAngles = newEulerAngles;
        }

        /// <summary>
        /// ローカル回転のX角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">設定する角度</param>
        public static void SetLocalEulerAngleX(this Transform self, float x)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.x = x;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転のY角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">設定する角度</param>
        public static void SetLocalEulerAngleY(this Transform self, float y)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.y = y;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転のZ角度を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">設定する角度</param>
        public static void SetLocalEulerAngleZ(this Transform self, float z)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.z = z;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転に角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="addAngles">加算する角度</param>
        public static void AddLocalEulerAngles(this Transform self, Vector3 addAngles)
        {
            var newLocalEulerAngles = self.localEulerAngles + addAngles;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転のX角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">加算する角度</param>
        public static void AddLocalEulerAngleX(this Transform self, float x)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.x += x;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転のY角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">加算する角度</param>
        public static void AddLocalEulerAngleY(this Transform self, float y)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.y += y;
            self.localEulerAngles = newLocalEulerAngles;
        }

        /// <summary>
        /// ローカル回転のZ角度を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">加算する角度</param>
        public static void AddLocalEulerAngleZ(this Transform self, float z)
        {
            var newLocalEulerAngles = self.localEulerAngles;
            newLocalEulerAngles.z += z;
            self.localEulerAngles = newLocalEulerAngles;
        }

    }
}
