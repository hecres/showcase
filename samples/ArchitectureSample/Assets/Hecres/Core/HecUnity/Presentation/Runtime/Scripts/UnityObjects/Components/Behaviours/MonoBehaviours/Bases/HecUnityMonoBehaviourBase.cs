using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases
{
    /// <summary>
    /// 独自にカスタマイズしたMonoBehaviourクラスの基底
    /// </summary>
    public abstract class HecUnityMonoBehaviourBase : MonoBehaviour
    {
        /// <summary>
        /// オブジェクトが破棄されているかどうか
        /// </summary>
        public bool IsDestroyed => this == null;

        // 2020.2以降のUnityではgameObjectやtransformのアクセスが高速化されているため、ユーザー側でキャッシュ処理を実装するメリットはない
        // 取得毎のasによるキャスト負荷も限りなく小さい

        /// <summary>
        /// RectTransformの参照
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public RectTransform rectTransform => transform as RectTransform;
    }
}
