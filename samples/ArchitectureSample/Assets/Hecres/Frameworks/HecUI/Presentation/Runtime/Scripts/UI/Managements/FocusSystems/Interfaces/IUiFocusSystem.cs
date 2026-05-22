namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces
{
    /// <summary>
    /// UIフォーカスの管理インターフェース
    /// </summary>
    public interface IUiFocusSystem
    {
        /// <summary>
        /// 現在のフォーカス対象UI
        /// </summary>
        IFocusableUi CurrentTarget { get; }

        /// <summary>
        /// 指定UIをフォーカス中かどうか判別します。
        /// </summary>
        /// <param name="target">調査対象UI</param>
        /// <returns>true: フォーカス中 / false: フォーカスしていない</returns>
        bool IsFocus(IFocusableUi target);

        /// <summary>
        /// 現在のフォーカス対象UIへのフォーカスを解除します。
        /// </summary>
        public void DeFocus();

        /// <summary>
        /// 指定UIをフォーカスします。
        /// </summary>
        /// <param name="target">フォーカス対象UI</param>
        public void Focus(IFocusableUi target);

        /// <summary>
        /// 現在のUI配置状態に基づいてフォーカス状態を更新します。
        /// </summary>
        /// <remarks>
        /// LayerableCanvases管理上もっとも前面に配置されているアクティブなUIをフォーカス対象とします。
        /// </remarks>
        void UpdateFocus();
    }
}
