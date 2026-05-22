using System.Collections.Generic;
using System.Linq;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions
{
    /// <summary>
    /// Componentの拡張クラス
    /// </summary>
    public static partial class ComponentExtensions
    {
        /// <summary>
        /// 指定型のコンポーネントを保持しているか判定します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <returns>true: 指定型のコンポーネントを保持している / false: 指定型のコンポーネントを保持していない</returns>
        public static bool HasComponent<T>(this Component self) => self.gameObject.HasComponent<T>();

        /// <summary>
        /// 指定型のコンポーネントを安全に取得します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentSafely<T>(this Component self) => self.gameObject.GetComponentSafely<T>();

        /// <summary>
        /// 子階層から指定型のコンポーネントを安全に取得します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentInChildrenSafely<T>(this Component self, bool includeInactive = false) => self.gameObject.GetComponentInChildrenSafely<T>(includeInactive);

        /// <summary>
        /// 自身を除く子階層の指定型コンポーネントを列挙します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたコンポーネントの列</returns>
        public static IEnumerable<T> GetComponentsInChildrenWithoutSelf<T>(this Component self, bool includeInactive = false) => self.gameObject.GetComponentsInChildrenWithoutSelf<T>(includeInactive);

        /// <summary>
        /// 子階層のRendererから全てのMaterialを列挙します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたMaterialの列</returns>
        public static IEnumerable<Material> GetMaterialsInChildren(this Component self, bool includeInactive = false) => self.gameObject.GetMaterialsInChildren(includeInactive);

        /// <summary>
        /// ルートCanvasを取得します。
        /// </summary>
        /// <param name="self">対象のComponent</param>
        /// <returns>ルートCanvas</returns>
        public static Canvas GetRootCanvas(this Component self)
        {
            var parentCanvases = self.GetComponentsInParent<Canvas>();
            if (parentCanvases == null || parentCanvases.Length == 0) return null;

            return parentCanvases.FirstOrDefault(item => item.isRootCanvas);
        }
    }
}
