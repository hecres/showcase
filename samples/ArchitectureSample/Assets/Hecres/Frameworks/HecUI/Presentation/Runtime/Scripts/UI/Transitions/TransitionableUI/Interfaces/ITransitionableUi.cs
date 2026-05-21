using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.TransitionableUI.Interfaces
{
    /// <summary>
    /// UIの表示・非表示の操作インターフェース
    /// </summary>
    /// <remarks>
    /// このインターフェースを介して、UIのトランジションを非同期または即座に実行できます。<br/>
    /// 呼び出し元は、具体的な演出方法（アニメーション、アクティブ切り替えなど）を意識する必要がありません。
    /// </remarks>
    public interface ITransitionableUi
    {
        /// <summary>
        /// UIの非表示完了時に通知
        /// </summary>
        Observable<Unit> Hidden { get; }

        /// <summary>
        /// UIの表示完了時に通知
        /// </summary>
        Observable<Unit> Shown { get; }

        /// <summary>
        /// 現在の表示ステータス
        /// </summary>
        ReadOnlyReactiveProperty<UiTransitionStateType> State { get; }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask ShowAsync(CancellationToken token);

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        void ShowSoon();

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask HideAsync(CancellationToken token);

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        void HideSoon();

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask SwitchAsync(bool wantShow, CancellationToken token);

        /// <summary>
        /// UIを指定の表示状態に即時切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        void SwitchSoon(bool wantShow);

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask FlipAsync(CancellationToken token);

        /// <summary>
        /// UIの表示/非表示状態を即時反転させます。
        /// </summary>
        void FlipSoon();
    }
}
