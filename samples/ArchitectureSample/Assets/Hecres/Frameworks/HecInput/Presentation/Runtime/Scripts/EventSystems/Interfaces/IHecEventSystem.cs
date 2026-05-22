using UnityEngine;

namespace Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces
{
    /// <summary>
    /// 独自にカスタマイズしたイベントシステムインターフェース
    /// </summary>
    public interface IHecEventSystem
    {
        /// <summary>
        /// 現在選択中のゲームオブジェクト
        /// </summary>
        GameObject CurrentSelectedGameObject { get; }

        /// <summary>
        /// 選択を解除します。
        /// </summary>
        void Deselect();

        /// <summary>
        /// 選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        void SetSelectedGameObject(GameObject selected);

        /// <summary>
        /// 選択対象を設定します。
        /// </summary>
        /// <remarks>
        /// 現在と同一オブジェクトが指定された場合は無視します。
        /// </remarks>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        void SetSelectedGameObjectIgnoreSame(GameObject selected);

        /// <summary>
        /// 子階層を考慮して選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のゲームオブジェクト</param>
        void SetSelectedGameObjectInChildren(GameObject selected);

        /// <summary>
        /// 子階層を考慮して選択対象を設定します。
        /// </summary>
        /// <param name="selected">選択対象のコンポーネント</param>
        void SetSelectedGameObjectInChildren(Component selected);

        /// <summary>
        /// 指定したTransformの子孫が選択されているか確認します。
        /// </summary>
        /// <param name="parent">親のTransform</param>
        /// <returns>true: 子孫に選択中のオブジェクトが含まれている / false: 含まれていない</returns>
        bool IsSelectedChildOf(Transform parent);

        /// <summary>
        /// UIナビゲーションにおけるリピートの開始判定時間を取得します。
        /// </summary>
        /// <remarks>
        /// リピートとは、十字キーなどを押し続けた時に自動でカーソル移動を行なう挙動のことを指します。
        /// </remarks>
        /// <returns>リピート開始判定時間（秒）</returns>
        float GetMoveRepeatDelaySeconds();

        /// <summary>
        /// UIナビゲーションにおけるリピートの間隔を取得します。
        /// </summary>
        /// <remarks>
        /// リピートとは、十字キーなどを押し続けた時に自動でカーソル移動を行なう挙動のことを指します。
        /// </remarks>
        /// <returns>リピート間隔（秒）</returns>
        float GetMoveRepeatRateSeconds();
    }
}
