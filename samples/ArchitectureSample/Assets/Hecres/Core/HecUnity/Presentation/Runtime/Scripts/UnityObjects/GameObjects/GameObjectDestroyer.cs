using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects
{
    /// <summary>
    /// GameObject破棄のヘルパークラス
    /// </summary>
    public static class GameObjectDestroyer
    {
        /// <summary>
        /// 指定のComponentがnullでない場合に破棄します。
        /// </summary>
        /// <param name="component">対象のComponent</param>
        public static void DestroyIfNotNull(Component component)
        {
            if (component != null)
            {
                DestroyIfNotNull(component.gameObject);
            }
        }

        /// <summary>
        /// 指定のObjectがnullでない場合に破棄します。
        /// </summary>
        /// <param name="obj">対象のObject</param>
        public static void DestroyIfNotNull(Object obj)
        {
            if (obj != null)
            {
                Object.Destroy(obj);
            }
        }
    }
}
