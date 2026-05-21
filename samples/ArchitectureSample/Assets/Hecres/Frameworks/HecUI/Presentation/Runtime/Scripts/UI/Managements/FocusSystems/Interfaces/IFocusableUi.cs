using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces
{
    /// <summary>
    /// フォーカス可能UIインターフェース
    /// </summary>
    public interface IFocusableUi
    {
        /// <summary>
        /// オブジェクトが破棄されているかどうか
        /// </summary>
        bool IsDestroyed { get; }

        /// <summary>
        /// GameObjectの参照
        /// </summary>
        /// <remarks>
        /// Unity側の定義利用のため命名規則逸脱を許容しています。
        /// </remarks>
        // ReSharper disable once InconsistentNaming
        GameObject gameObject { get; }

        /// <summary>
        /// RectTransformの参照
        /// </summary>
        /// <remarks>
        /// Unity側の定義利用のため命名規則逸脱を許容しています。
        /// </remarks>
        // ReSharper disable once InconsistentNaming
        RectTransform rectTransform { get; }

        /// <summary>
        /// 本UIをフォーカスします。
        /// </summary>
        void FocusUi();

        /// <summary>
        /// 本UIからフォーカスを外します。
        /// </summary>
        void DeFocusUi();
    }
}
