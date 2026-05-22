using System;
using System.Collections.Generic;
using System.Linq;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions
{
    /// <summary>
    /// GameObjectの拡張クラス
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// シーン階層上のパスを取得します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <returns>シーン階層上のパス</returns>
        public static string GetScenePath(this GameObject self) => self.transform.GetScenePath();

        /// <summary>
        /// 指定型のコンポーネントを保持しているか判定します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <returns>true: 指定型のコンポーネントを保持している / false: 指定型のコンポーネントを保持していない</returns>
        public static bool HasComponent<T>(this GameObject self) => self.GetComponent<T>() != null;

        /// <summary>
        /// 指定型のコンポーネントを安全に取得します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentSafely<T>(this GameObject self)
        {
            var component = self.GetComponent<T>();
            if (component == null) throw new InvalidOperationException($"{self.name} hasn't {typeof(T)}");

            return component;
        }

        /// <summary>
        /// 子階層から指定型のコンポーネントを安全に取得します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>取得したコンポーネント</returns>
        public static T GetComponentInChildrenSafely<T>(this GameObject self, bool includeInactive = false)
        {
            var component = self.GetComponentInChildren<T>(includeInactive);
            if (component == null) throw new InvalidOperationException($"{self.name} children hasn't {typeof(T)}");

            return component;
        }

        /// <summary>
        /// 自身を除く子階層の指定型コンポーネントを列挙します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたコンポーネントの列</returns>
        public static IEnumerable<T> GetComponentsInChildrenWithoutSelf<T>(this GameObject self, bool includeInactive = false)
        {
            var selfComponent = self.GetComponent<T>();
            return self.GetComponentsInChildren<T>(includeInactive).Where(value => !value.Equals(selfComponent));
        }

        /// <summary>
        /// 子階層のRendererから全てのMaterialを列挙します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <param name="includeInactive">非アクティブのオブジェクトを含めるかどうか</param>
        /// <returns>列挙されたMaterialの列</returns>
        public static IEnumerable<Material> GetMaterialsInChildren(this GameObject self, bool includeInactive = false)
        {
            var temp = new List<Material>();
            var renderers = self.GetComponentsInChildren<Renderer>(includeInactive);
            foreach (var item in renderers)
            {
                temp.AddRange(item.materials);
            }

            return temp;
        }

        /// <summary>
        /// 指定型のコンポーネントが存在しない場合に追加します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <returns>取得または追加されたコンポーネント</returns>
        public static T AddComponentIfNotAttached<T>(this GameObject self) where T : Component
        {
            var component = self.GetComponent<T>();
            return component ? component : self.AddComponent<T>();
        }

        /// <summary>
        /// 指定レイヤーを子孫オブジェクトに再帰的に設定します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <param name="layer">設定するレイヤー</param>
        /// <param name="ignoreLayers">設定を無視するレイヤー</param>
        public static void SetLayerRecursively(this GameObject self, int layer, params int[] ignoreLayers)
        {
            if (ignoreLayers.All(item => item != self.layer))
            {
                self.layer = layer;
            }

            foreach (Transform child in self.transform) SetLayerRecursively(child.gameObject, layer, ignoreLayers);
        }

        /// <summary>
        /// 親オブジェクトのレイヤーに合わせて自身および子孫のレイヤーを設定します。
        /// </summary>
        /// <param name="self">対象のGameObject</param>
        /// <param name="ignoreLayers">設定を無視するレイヤー</param>
        public static void FitParentLayerRecursively(this GameObject self, params int[] ignoreLayers)
        {
            var selfParent = self.transform.parent;
            if (selfParent == null) return;

            var parentGameObject = selfParent.gameObject;
            self.SetLayerRecursively(parentGameObject.layer, ignoreLayers);
        }
    }
}
