using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions
{
    /// <summary>
    /// Transformの拡張クラス
    /// </summary>
    public static partial class TransformExtensions
    {

        /// <summary>
        /// ワールド座標のX値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">設定するX値</param>
        public static void SetPositionX(this Transform self, float x)
        {
            var newPosition = self.position;
            newPosition.x = x;
            self.position = newPosition;
        }

        /// <summary>
        /// ワールド座標のY値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">設定するY値</param>
        public static void SetPositionY(this Transform self, float y)
        {
            var newPosition = self.position;
            newPosition.y = y;
            self.position = newPosition;
        }

        /// <summary>
        /// ワールド座標のZ値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">設定するZ値</param>
        public static void SetPositionZ(this Transform self, float z)
        {
            var newPosition = self.position;
            newPosition.z = z;
            self.position = newPosition;
        }

        /// <summary>
        /// ワールド座標のX値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">加算するX値</param>
        public static void AddPositionX(this Transform self, float x)
        {
            var newPosition = self.position;
            newPosition.x += x;
            self.position = newPosition;
        }

        /// <summary>
        /// ワールド座標のY値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">加算するY値</param>
        public static void AddPositionY(this Transform self, float y)
        {
            var newPosition = self.position;
            newPosition.y += y;
            self.position = newPosition;
        }

        /// <summary>
        /// ワールド座標のZ値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">加算するZ値</param>
        public static void AddPositionZ(this Transform self, float z)
        {
            var newPosition = self.position;
            newPosition.z += z;
            self.position = newPosition;
        }

        /// <summary>
        /// ローカル座標のX値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">設定するX値</param>
        public static void SetLocalPositionX(this Transform self, float x)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.x = x;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標のY値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">設定するY値</param>
        public static void SetLocalPositionY(this Transform self, float y)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.y = y;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標のZ値を設定します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">設定するZ値</param>
        public static void SetLocalPositionZ(this Transform self, float z)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.z = z;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="motion">加算するローカル座標</param>
        public static void AddLocalPosition(this Transform self, Vector3 motion)
        {
            var newLocalPosition = self.localPosition + motion;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標のX値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="x">加算するX値</param>
        public static void AddLocalPositionX(this Transform self, float x)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.x += x;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標のY値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="y">加算するY値</param>
        public static void AddLocalPositionY(this Transform self, float y)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.y += y;
            self.localPosition = newLocalPosition;
        }

        /// <summary>
        /// ローカル座標のZ値を加算します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="z">加算するZ値</param>
        public static void AddLocalPositionZ(this Transform self, float z)
        {
            var newLocalPosition = self.localPosition;
            newLocalPosition.z += z;
            self.localPosition = newLocalPosition;
        }

    }
}
