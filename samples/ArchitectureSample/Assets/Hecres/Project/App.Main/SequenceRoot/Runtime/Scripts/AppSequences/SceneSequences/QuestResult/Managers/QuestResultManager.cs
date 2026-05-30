using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestResult.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Home.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestResult;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestResult.Managers
{
    /// <summary>
    /// クエスト結果シーケンスの管理クラス
    /// </summary>
    public class QuestResultManager : ProjectSceneSequenceManagerBase<QuestResultManagerArgs, QuestResultSequence, QuestResultUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<QuestResultSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.FromResult(new QuestResultSequence(SequenceManagerArgs.QuestId, SequenceManagerArgs.IsWin));
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, QuestResultUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            presenter.ReturnHomeRequested
                .SubscribeAwait(async (_, subscribeToken) => await LoadHomeSequenceAsync(subscribeToken), AwaitOperation.Drop)
                .AddTo(this);

            return new Tuple<GameObject, QuestResultUiPresenter>(presenter.gameObject, presenter);
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
