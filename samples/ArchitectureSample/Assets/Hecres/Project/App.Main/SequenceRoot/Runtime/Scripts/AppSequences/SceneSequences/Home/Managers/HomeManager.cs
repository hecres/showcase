using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.Home.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestSelect.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Home;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Home.Managers
{
    /// <summary>
    /// ホームシーケンスの管理クラス
    /// </summary>
    public class HomeManager : ProjectSceneSequenceManagerBase<HomeManagerArgs, HomeSequence, HomeUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<HomeSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.FromResult(new HomeSequence());
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, HomeUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            presenter.QuestSelectRequested
                .SubscribeAwait(async (_, subscribeToken) => await LoadQuestSelectSequenceAsync(subscribeToken), AwaitOperation.Drop)
                .AddTo(this);

            return new Tuple<GameObject, HomeUiPresenter>(presenter.gameObject, presenter);
        }

        /// <summary>
        /// クエスト選択シーケンスへの遷移を行ないます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>遷移処理の非同期タスク</returns>
        private async UniTask LoadQuestSelectSequenceAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await SceneSequenceLoader.LoadSceneSequenceAsync(new QuestSelectManagerArgs());
        }
    }
}
