using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects
{
    /// <summary>
    /// GameObjectのコレクションクラス
    /// </summary>
    public class GameObjectCollection : IReadOnlyCollection<GameObject>
    {
        /// <summary>
        /// 要素数
        /// </summary>
        public int Count => collection.Count;

        private readonly List<GameObject> collection = new();

        /// <summary>
        /// 指定のComponentをコレクションに追加します。
        /// </summary>
        /// <param name="component">追加するComponent</param>
        public void Add(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            Add(component.gameObject);
        }

        /// <summary>
        /// 指定のGameObjectをコレクションに追加します。
        /// </summary>
        /// <param name="gameObject">追加するGameObject</param>
        public void Add(GameObject gameObject)
        {
            if (gameObject == null) throw new ArgumentNullException(nameof(gameObject));

            collection.Add(gameObject);
            collection.RemoveAll(element => element == null);
        }

        /// <summary>
        /// 指定のComponent列をコレクションに追加します。
        /// </summary>
        /// <param name="components">追加するComponentの列</param>
        public void AddRange(IEnumerable<Component> components)
        {
            if (components == null) throw new ArgumentNullException(nameof(components));

            AddRange(components.Select(item => item == null ? null : item.gameObject));
        }

        /// <summary>
        /// 指定のGameObject列をコレクションに追加します。
        /// </summary>
        /// <param name="gameObjects">追加するGameObjectの列</param>
        public void AddRange(IEnumerable<GameObject> gameObjects)
        {
            if (gameObjects == null) throw new ArgumentNullException(nameof(gameObjects));

            collection.AddRange(gameObjects);
            collection.RemoveAll(element => element == null);
        }

        /// <summary>
        /// コレクション内の全てのGameObjectを破棄し、コレクションをクリアします。
        /// </summary>
        public void DisposeAll()
        {
            foreach (var gameObject in collection)
            {
                GameObjectDestroyer.DestroyIfNotNull(gameObject);
            }

            collection.Clear();
        }

        /// <summary>
        /// 指定のComponentに紐づくGameObjectを破棄し、コレクションから削除します。
        /// </summary>
        /// <param name="component">破棄する対象のComponent</param>
        public void Dispose(Component component)
        {
            if (component != null)
            {
                Dispose(component.gameObject);
            }
        }

        /// <summary>
        /// 指定のGameObjectを破棄し、コレクションから削除します。
        /// </summary>
        /// <param name="gameObject">破棄する対象のGameObject</param>
        public void Dispose(GameObject gameObject)
        {
            collection.Remove(gameObject);
            GameObjectDestroyer.DestroyIfNotNull(gameObject);
        }

        /// <summary>
        /// コレクションを列挙します。
        /// </summary>
        /// <returns>GameObjectの列挙子</returns>
        public IEnumerator<GameObject> GetEnumerator() => collection.GetEnumerator();

        /// <summary>
        /// コレクションを列挙します。
        /// </summary>
        /// <returns>列挙子</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
