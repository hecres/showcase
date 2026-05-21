using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.TransitionableUI.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Interfaces;
using R3;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases
{
    public partial class SceneSequenceUiPresenterBase<T> : ITransitionableUi
    {
        /// <summary>
        /// UIの非表示完了時に通知
        /// </summary>
        public Observable<Unit> Hidden => Transitioner.Hidden;

        /// <summary>
        /// UIの表示完了時に通知
        /// </summary>
        public Observable<Unit> Shown => Transitioner.Shown;

        /// <summary>
        /// 現在の表示ステータス
        /// </summary>
        public ReadOnlyReactiveProperty<UiTransitionStateType> State => Transitioner.State;

        private IUiTransitioner Transitioner => transitionerCache ??= this.GetComponentSafely<IUiTransitioner>();

        private IUiTransitioner transitionerCache;

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask ShowAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Transitioner.ShowAsync(token);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        public void ShowSoon() => Transitioner.ShowSoon();

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask HideAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Transitioner.HideAsync(token);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        public void HideSoon() => Transitioner.HideSoon();

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask SwitchAsync(bool wantShow, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Transitioner.SwitchAsync(wantShow, token);
        }

        /// <summary>
        /// UIを指定の表示状態に即時切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        public void SwitchSoon(bool wantShow) => Transitioner.SwitchSoon(wantShow);

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask FlipAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return Transitioner.FlipAsync(token);
        }

        /// <summary>
        /// UIの表示/非表示状態を即時反転させます。
        /// </summary>
        public void FlipSoon() => Transitioner.FlipSoon();
    }
}
