namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers
{
    /// <summary>
    /// シーンシーケンスのアクティベートタイプの列挙体
    /// </summary>
    public enum SceneSequenceActivateType
    {
        /// <summary>
        /// 新規読み込み
        /// </summary>
        LoadActivate,

        /// <summary>
        /// 再アクティベート
        /// </summary>
        /// <remarks>
        /// 例: 「戻る」操作などによるアクティベートです。
        /// </remarks>
        ReActivate,
    }
}
