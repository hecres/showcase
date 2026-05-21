namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions
{
    /// <summary>
    /// 遷移状態タイプの列挙体
    /// </summary>
    public enum UiTransitionStateType
    {
        /// <summary>
        /// 未初期化状態
        /// </summary>
        Uninitialized,

        /// <summary>
        /// 開く演出中
        /// </summary>
        Showing,

        /// <summary>
        /// 開く演出が完了済
        /// </summary>
        Showed,

        /// <summary>
        /// 閉じる演出中
        /// </summary>
        Hiding,

        /// <summary>
        /// 閉じる演出が完了済
        /// </summary>
        Hided
    }
}
