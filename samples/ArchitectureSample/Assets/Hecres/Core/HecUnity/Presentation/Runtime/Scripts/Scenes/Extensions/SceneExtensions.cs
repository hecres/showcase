using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Hecres.Core.HecUnity.Presentation.Scenes.Extensions
{
    /// <summary>
    /// Sceneの拡張クラス
    /// </summary>
    public static class SceneExtensions
    {
        /// <summary>
        /// シーン内に指定型のコンポーネントが存在するか判定します。
        /// </summary>
        /// <param name="self">対象のScene</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>true: 存在する / false: 存在しない</returns>
        public static bool HasComponentInScene<T>(this Scene self, bool includeInactive = false) => self.GetComponentsInScene<T>(includeInactive).Any();

        /// <summary>
        /// シーン内から指定型のコンポーネントを安全に1件取得します。
        /// </summary>
        /// <param name="self">対象のScene</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentInSceneSafely<T>(this Scene self, bool includeInactive = false) => self.GetComponentsInSceneSafely<T>(includeInactive).First();

        /// <summary>
        /// シーン内から指定型のコンポーネントを安全に列挙します。
        /// </summary>
        /// <param name="self">対象のScene</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたコンポーネントの列</returns>
        public static IEnumerable<T> GetComponentsInSceneSafely<T>(this Scene self, bool includeInactive = false)
        {
            var components = self.GetComponentsInScene<T>(includeInactive).ToArray();
            if (components.Any()) return components;

            throw new InvalidOperationException($"{self.name} hasn't {typeof(T)}");
        }

        /// <summary>
        /// シーン内から指定型のコンポーネントを1件取得します。
        /// </summary>
        /// <param name="self">対象のScene</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentInScene<T>(this Scene self, bool includeInactive = false) => self.GetComponentsInScene<T>(includeInactive).FirstOrDefault();

        /// <summary>
        /// シーン内の全てのルートから指定型のコンポーネントを列挙します。
        /// </summary>
        /// <param name="self">対象のScene</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたコンポーネントの列</returns>
        public static IEnumerable<T> GetComponentsInScene<T>(this Scene self, bool includeInactive = false)
        {
            var rootGameObjects = self.GetRootGameObjects();
            return rootGameObjects.Select(item => item.GetComponentsInChildren<T>(includeInactive)).SelectMany(value => value);
        }
    }
}
