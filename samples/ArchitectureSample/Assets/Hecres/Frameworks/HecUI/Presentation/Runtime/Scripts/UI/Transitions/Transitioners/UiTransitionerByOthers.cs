using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners.Bases;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Transitions.Transitioners
{
    /// <summary>
    /// 他のUI遷移を委譲して実行するクラス
    /// </summary>
    public class UiTransitionerByOthers : UiTransitionerBase
    {
        [SerializeField] private List<UiTransitionerBase> otherTransitioners;

        /// <summary>
        /// UIを表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask ShowAsyncInherent(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var tasks = new List<UniTask>(otherTransitioners.Count);
            tasks.AddRange(Enumerable.Select(otherTransitioners, item => item.ShowAsync(token)));

            await UniTask.WhenAll(tasks);
        }

        /// <summary>
        /// UIを即時表示します。
        /// </summary>
        protected override void ShowSoonInherent()
        {
            foreach (var item in otherTransitioners)
            {
                item.ShowSoon();
            }
        }

        /// <summary>
        /// UIを非表示化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override async UniTask HideAsyncInherent(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var tasks = new List<UniTask>(otherTransitioners.Count);
            tasks.AddRange(Enumerable.Select(otherTransitioners, item => item.HideAsync(token)));

            await UniTask.WhenAll(tasks);
        }

        /// <summary>
        /// UIを即時非表示化します。
        /// </summary>
        protected override void HideSoonInherent()
        {
            foreach (var item in otherTransitioners)
            {
                item.HideSoon();
            }
        }
    }
}
