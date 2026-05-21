using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers.Interfaces
{
    /// <summary>
    /// 画面遷移演出UIの操作インターフェース
    /// </summary>
    public interface IScreenTransitionUiOperator
    {
        /// <summary>
        /// いずれかの画面遷移演出が実行中かどうか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> AnyVisibleTransition { get; }

        /// <summary>
        /// メインの画面遷移演出を非同期で表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask ShowMainAsync(CancellationToken token);

        /// <summary>
        /// メインの画面遷移演出を即時に表示します。
        /// </summary>
        void ShowMainSoon();

        /// <summary>
        /// メインの画面遷移演出を非同期で非表示にします。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask HideMainAsync(CancellationToken token);

        /// <summary>
        /// メインの画面遷移演出を即時に非表示にします。
        /// </summary>
        void HideMainSoon();

        /// <summary>
        /// サブの画面遷移演出を非同期で表示します。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask ShowSubAsync(int index, CancellationToken token);

        /// <summary>
        /// サブの画面遷移演出を即時に表示します。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        void ShowSubSoon(int index);

        /// <summary>
        /// サブの画面遷移演出を非同期で非表示にします。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask HideSubAsync(int index, CancellationToken token);

        /// <summary>
        /// サブの画面遷移演出を即時に非表示にします。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        void HideSubSoon(int index);
    }
}
