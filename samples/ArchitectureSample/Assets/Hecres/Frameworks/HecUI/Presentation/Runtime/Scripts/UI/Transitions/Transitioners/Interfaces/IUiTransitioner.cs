using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Interfaces
{
    /// <summary>
    /// UIの表示・非表示における具体的な演出処理を定義するインターフェース
    /// </summary>
    /// <remarks>
    /// このインターフェースを実装することで、UIのトランジション方法（例：アクティブ切り替え、Animatorによるアニメーション）を差し替え可能にします。
    /// IUiTransitionableと連携し、具体的なロジックをカプセル化します。
    /// </remarks>
    public interface IUiTransitioner
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
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask ShowAsync(bool isForce, CancellationToken token);

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask ShowAsync(CancellationToken token);

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        void ShowSoon(bool isForce = false);

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask HideAsync(bool isForce, CancellationToken token);

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask HideAsync(CancellationToken token);

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        void HideSoon(bool isForce = false);

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask SwitchAsync(bool wantShow, bool isForce, CancellationToken token);

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
        /// <param name="isForce">強制的に反映するかどうか</param>
        void SwitchSoon(bool wantShow, bool isForce = false);

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask FlipAsync(bool isForce, CancellationToken token);

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask FlipAsync(CancellationToken token);

        /// <summary>
        /// UIの表示/非表示状態を即時反転させます。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        void FlipSoon(bool isForce = false);
    }
}
