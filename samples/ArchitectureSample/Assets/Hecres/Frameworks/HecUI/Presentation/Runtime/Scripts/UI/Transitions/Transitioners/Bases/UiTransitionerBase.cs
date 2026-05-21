using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Interfaces;
using R3;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases
{
    /// <summary>
    /// UI遷移クラスの基底
    /// </summary>
    public abstract class UiTransitionerBase : HecUnityMonoBehaviourBase, IUiTransitioner
    {
        /// <summary>
        /// UIの非表示完了時に通知
        /// </summary>
        public Observable<Unit> Hidden => hideStream.Share();

        /// <summary>
        /// UIの表示完了時に通知
        /// </summary>
        public Observable<Unit> Shown => showStream.Share();

        /// <summary>
        /// 現在の表示ステータス
        /// </summary>
        public ReadOnlyReactiveProperty<UiTransitionStateType> State => state;

        [SerializeField] private UiTransitionStateType defaultStateType = UiTransitionStateType.Uninitialized;

        private readonly Subject<Unit> hideStream = new();
        private readonly Subject<Unit> showStream = new();
        private readonly ReactiveProperty<UiTransitionStateType> state = new();

        private CancellationTokenSource transitionTaskCts;

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        protected virtual void Start()
        {
            // すでに外から表示/非表示指示が行なわれている場合はデフォルト状態の適用をスキップします
            if (State.CurrentValue == UiTransitionStateType.Uninitialized)
            {
                switch (defaultStateType)
                {
                    case UiTransitionStateType.Uninitialized: break;
                    case UiTransitionStateType.Showing:       ShowAsync(destroyCancellationToken).Forget(); break;
                    case UiTransitionStateType.Showed:        ShowSoon(); break;
                    case UiTransitionStateType.Hiding:        HideAsync(destroyCancellationToken).Forget(); break;
                    case UiTransitionStateType.Hided:         HideSoon(); break;
                    default:                                  throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        public async UniTask ShowAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (isForce)
            {
                await ShowAsyncInternal(token);
                return;
            }

            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Showing:
                    await Shown.FirstAsync(token);
                    return;
                case UiTransitionStateType.Showed: return;
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Hiding:
                case UiTransitionStateType.Hided:
                    await ShowAsyncInternal(token);
                    return;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        private async UniTask ShowAsyncInternal(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            CancelTransitionTask();
            transitionTaskCts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);

            state.Value = UiTransitionStateType.Showing;
            try
            {
                gameObject.SetActive(true);
                await ShowAsyncInherent(CancellationTokenSource.CreateLinkedTokenSource(token, transitionTaskCts.Token).Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            state.Value = UiTransitionStateType.Showed;
            showStream.OnNext(Unit.Default);
        }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        protected abstract UniTask ShowAsyncInherent(CancellationToken token);

        /// <summary>
        /// UIを表示します。
        /// </summary>
        public UniTask ShowAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return ShowAsync(false, token);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        public void ShowSoon(bool isForce = false)
        {
            if (isForce)
            {
                ShowSoonInternal();
                return;
            }

            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Showing:
                case UiTransitionStateType.Showed:
                    return;
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Hiding:
                case UiTransitionStateType.Hided:
                    ShowSoonInternal();
                    return;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        private void ShowSoonInternal()
        {
            CancelTransitionTask();
            state.Value = UiTransitionStateType.Showing;
            gameObject.SetActive(true);
            ShowSoonInherent();
            state.Value = UiTransitionStateType.Showed;
            showStream.OnNext(Unit.Default);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        protected abstract void ShowSoonInherent();

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        public async UniTask HideAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (isForce)
            {
                await HideAsyncInternal(token);
                return;
            }

            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Showing:
                case UiTransitionStateType.Showed:
                    await HideAsyncInternal(token);
                    return;
                case UiTransitionStateType.Hiding:
                    await Hidden.FirstAsync();
                    return;
                case UiTransitionStateType.Hided: return;
                default:                          throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        private async UniTask HideAsyncInternal(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            CancelTransitionTask();
            transitionTaskCts = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);

            state.Value = UiTransitionStateType.Hiding;
            try
            {
                await HideAsyncInherent(CancellationTokenSource.CreateLinkedTokenSource(token, transitionTaskCts.Token).Token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            state.Value = UiTransitionStateType.Hided;
            gameObject.SetActive(false);
            hideStream.OnNext(Unit.Default);
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        protected abstract UniTask HideAsyncInherent(CancellationToken token);

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        public UniTask HideAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return HideAsync(false, token);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        public void HideSoon(bool isForce = false)
        {
            if (isForce)
            {
                HideSoonInternal();
                return;
            }

            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Showing:
                case UiTransitionStateType.Showed:
                    HideSoonInternal();
                    return;
                case UiTransitionStateType.Hiding:
                case UiTransitionStateType.Hided:
                    return;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        private void HideSoonInternal()
        {
            CancelTransitionTask();
            state.Value = UiTransitionStateType.Hiding;
            HideSoonInherent();
            gameObject.SetActive(false);
            state.Value = UiTransitionStateType.Hided;
            hideStream.OnNext(Unit.Default);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        protected abstract void HideSoonInherent();

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        public UniTask SwitchAsync(bool wantShow, bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return wantShow ? ShowAsync(isForce, token) : HideAsync(isForce, token);
        }

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        public UniTask SwitchAsync(bool wantShow, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return SwitchAsync(wantShow, false, token);
        }

        /// <summary>
        /// UIを指定の表示状態に即時切り替えます。
        /// </summary>
        public void SwitchSoon(bool wantShow, bool isForce = false)
        {
            if (wantShow)
            {
                ShowSoon(isForce);
            }
            else
            {
                HideSoon(isForce);
            }
        }

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        public async UniTask FlipAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Showing:
                case UiTransitionStateType.Showed:
                    await HideAsync(isForce, token);
                    return;
                case UiTransitionStateType.Hiding:
                case UiTransitionStateType.Hided:
                    await ShowAsync(isForce, token);
                    return;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        public UniTask FlipAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return FlipAsync(false, token);
        }

        /// <summary>
        /// UIの表示/非表示状態を即時反転させます。
        /// </summary>
        public void FlipSoon(bool isForce = false)
        {
            switch (State.CurrentValue)
            {
                case UiTransitionStateType.Uninitialized:
                case UiTransitionStateType.Showing:
                case UiTransitionStateType.Showed:
                    HideSoon(isForce);
                    return;
                case UiTransitionStateType.Hiding:
                case UiTransitionStateType.Hided:
                    ShowSoon(isForce);
                    return;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 遷移処理をキャンセルします。
        /// </summary>
        private void CancelTransitionTask()
        {
            if (transitionTaskCts == null) return;

            transitionTaskCts.Cancel();
            transitionTaskCts.Dispose();
            transitionTaskCts = null;
        }
    }
}
