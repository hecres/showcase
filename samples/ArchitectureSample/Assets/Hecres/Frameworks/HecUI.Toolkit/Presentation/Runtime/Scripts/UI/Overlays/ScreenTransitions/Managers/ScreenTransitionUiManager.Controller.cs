using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers
{
    public partial class ScreenTransitionUiManager
    {
        /// <summary>
        /// メインの画面遷移演出を非同期で表示します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask ShowMainAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await mainTransitionUi.ShowAsync(token);
        }

        /// <summary>
        /// メインの画面遷移演出を即時に表示します。
        /// </summary>
        public void ShowMainSoon()
        {
            mainTransitionUi.ShowSoon();
        }

        /// <summary>
        /// メインの画面遷移演出を非同期で非表示にします。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask HideMainAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await mainTransitionUi.HideAsync(token);
        }

        /// <summary>
        /// メインの画面遷移演出を即時に非表示にします。
        /// </summary>
        public void HideMainSoon()
        {
            mainTransitionUi.HideSoon();
        }

        /// <summary>
        /// サブの画面遷移演出を非同期で表示します。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask ShowSubAsync(int index, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await subTransitionUiList[index].ShowAsync(token);
        }

        /// <summary>
        /// サブの画面遷移演出を即時に表示します。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        public void ShowSubSoon(int index)
        {
            subTransitionUiList[index].ShowSoon();
        }

        /// <summary>
        /// サブの画面遷移演出を非同期で非表示にします。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask HideSubAsync(int index, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await subTransitionUiList[index].HideAsync(token);
        }

        /// <summary>
        /// サブの画面遷移演出を即時に非表示にします。
        /// </summary>
        /// <param name="index">対象演出のインデックス</param>
        public void HideSubSoon(int index)
        {
            subTransitionUiList[index].HideSoon();
        }
    }
}
