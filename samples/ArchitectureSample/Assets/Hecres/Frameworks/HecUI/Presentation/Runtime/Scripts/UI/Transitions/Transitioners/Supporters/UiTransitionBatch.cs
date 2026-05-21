using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Interfaces;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Supporters
{
    /// <summary>
    /// 複数のUI遷移を順次実行するサポートクラス
    /// </summary>
    public class UiTransitionBatch : HecUnityMonoBehaviourBase
    {
        [SerializeField] private float delayByIndex;

        private IEnumerable<IUiTransitioner> Transitioners => transitionersCache ??= this.GetComponentsInChildrenWithoutSelf<IUiTransitioner>();
        private IEnumerable<IUiTransitioner> transitionersCache;

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask ShowAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            foreach (var item in Transitioners)
            {
                await item.ShowAsync(isForce, token);
                await UniTask.Delay(TimeSpan.FromSeconds(delayByIndex), cancellationToken: token);
            }
        }

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask ShowAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return ShowAsync(false, token);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        public void ShowSoon(bool isForce = false)
        {
            foreach (var item in Transitioners)
            {
                item.ShowSoon(isForce);
            }
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask HideAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            foreach (var item in Transitioners)
            {
                await item.HideAsync(isForce, token);
                await UniTask.Delay(TimeSpan.FromSeconds(delayByIndex), cancellationToken: token);
            }
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask HideAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return HideAsync(false, token);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        public void HideSoon(bool isForce = false)
        {
            foreach (var item in Transitioners)
            {
                item.HideSoon(isForce);
            }
        }

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask SwitchAsync(bool wantShow, bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            foreach (var item in Transitioners)
            {
                await (wantShow ? item.ShowAsync(isForce, token) : item.HideAsync(isForce, token));
                await UniTask.Delay(TimeSpan.FromSeconds(delayByIndex), cancellationToken: token);
            }
        }

        /// <summary>
        /// UIを指定の表示状態に切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask SwitchAsync(bool wantShow, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return SwitchAsync(wantShow, false, token);
        }

        /// <summary>
        /// UIを指定の表示状態に即時切り替えます。
        /// </summary>
        /// <param name="wantShow">表示へ切り替えるかどうか</param>
        /// <param name="isForce">強制的に反映するかどうか</param>
        public void SwitchSoon(bool wantShow, bool isForce = false)
        {
            foreach (var item in Transitioners)
            {
                item.SwitchSoon(wantShow, isForce);
            }
        }

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask FlipAsync(bool isForce, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.WhenAll(Transitioners.Select(item => item.FlipAsync(isForce, token)));
        }

        /// <summary>
        /// UIの表示/非表示状態を反転させます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public UniTask FlipAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return FlipAsync(false, token);
        }

        /// <summary>
        /// UIの表示/非表示状態を即時反転させます。
        /// </summary>
        /// <param name="isForce">強制的に反映するかどうか</param>
        public void FlipSoon(bool isForce = false)
        {
            foreach (var item in Transitioners)
            {
                item.FlipSoon(isForce);
            }
        }
    }
}
