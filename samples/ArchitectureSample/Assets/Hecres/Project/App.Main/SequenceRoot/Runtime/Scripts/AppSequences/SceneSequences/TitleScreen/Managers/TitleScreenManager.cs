using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.TitleScreen.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Home.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.TitleScreen;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.TitleScreen.Managers
{
    /// <summary>
    /// タイトルシーケンスの管理クラス
    /// </summary>
    public class TitleScreenManager : ProjectSceneSequenceManagerBase<TitleScreenManagerArgs, TitleScreenSequence, TitleScreenUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<TitleScreenSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var args = new TitleScreenSequence.ManualArgs();
            return UniTask.FromResult(new TitleScreenSequence(args));
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, TitleScreenUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            presenter.LoginRequested.SubscribeAwait(async (_, subscribeToken) => await LoadHomeSequenceAsync(subscribeToken), AwaitOperation.Drop).AddTo(this);

            return new Tuple<GameObject, TitleScreenUiPresenter>(presenter.gameObject, presenter);
        }

        /// <summary>
        /// ホームシーケンスへの遷移を行ないます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>遷移処理の非同期タスク</returns>
        private async UniTask LoadHomeSequenceAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await SceneSequenceLoader.LoadSceneSequenceAsync(new HomeManagerArgs());
        }
    }
}
