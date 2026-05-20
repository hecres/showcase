using System;
using System.Collections.Generic;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions
{
    /// <summary>
    /// Transformの拡張クラス
    /// </summary>
    public static partial class TransformExtensions
    {
        /// <summary>
        /// アンカーからの相対的な深さを取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="anchor">基準となるTransform</param>
        /// <returns>アンカーからの相対的な深さ</returns>
        public static int GetHierarchyDepth(this Transform self, Transform anchor)
        {
            var depth = 0;
            var current = self;

            // アンカーの直下の子なら深さ1、アンカー自身なら深さ0
            while (current != null && current != anchor.parent)
            {
                if (current == anchor)
                {
                    // 検索対象のルート自身にTがある場合
                    return 0;
                }

                current = current.parent;
                depth++;
            }

            // 最後にanchor自身の深さを引いて、anchorからの相対的な深さに調整
            return depth - 1;
        }

        /// <summary>
        /// シーン階層上の親一覧をルートから自身まで取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <returns>ルートから自身までのTransformの読み取り専用一覧</returns>
        public static IReadOnlyList<Transform> GetSceneHierarchy(this Transform self)
        {
            var list = new List<Transform> { self };
            var parent = self.parent;
            while (parent)
            {
                list.Add(parent);
                parent = parent.parent;
            }

            list.Reverse();
            return list;
        }

        /// <summary>
        /// シーン階層上のパスを取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <returns>シーン階層上のパス</returns>
        public static string GetScenePath(this Transform self)
        {
            var path = self.name;
            var parent = self.parent;
            while (parent)
            {
                path = $"{parent.name}/{path}";
                parent = parent.parent;
            }

            return path;
        }

        /// <summary>
        /// ローカル座標・回転・スケールを初期化します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        public static void ResetLocalValues(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
        }

        /// <summary>
        /// 直下の子Transform配列を取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <returns>子Transform配列</returns>
        public static Transform[] GetChildren(this Transform self)
        {
            var childCount = self.childCount;
            var children = new Transform[childCount];
            for (var i = 0; i < childCount; i++) children[i] = self.GetChild(i);

            return children;
        }

        /// <summary>
        /// 指定名の子Transformを安全に取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="childName">子の名前</param>
        /// <returns>取得したTransform</returns>
        public static Transform FindChildSafely(this Transform self, string childName)
        {
            var child = self.Find(childName);
            if (child == null) throw new ArgumentException($"{self.name} has'nt {childName}");

            return child;
        }

        /// <summary>
        /// 指定名の子から指定型のコンポーネントを安全に取得します。
        /// </summary>
        /// <param name="self">対象のTransform</param>
        /// <param name="childName">子の名前</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetChildComponentSafely<T>(this Transform self, string childName)
        {
            var child = self.FindChildSafely(childName);
            return child.GetComponentSafely<T>();
        }

    }
}
