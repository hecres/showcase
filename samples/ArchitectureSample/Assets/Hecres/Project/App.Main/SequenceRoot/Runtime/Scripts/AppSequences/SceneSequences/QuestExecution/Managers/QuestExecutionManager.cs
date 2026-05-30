using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestExecution.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestResult.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestExecution;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestExecution.Managers
{
    /// <summary>
    /// クエスト実行シーケンスの管理クラス
    /// </summary>
    public class QuestExecutionManager : ProjectSceneSequenceManagerBase<QuestExecutionManagerArgs, QuestExecutionSequence, QuestExecutionUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<QuestExecutionSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.FromResult(new QuestExecutionSequence(SequenceManagerArgs.QuestId, ApiRequester));
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, QuestExecutionUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            presenter.QuestEndRequested
                .SubscribeAwait(async (isWin, subscribeToken) => await PerformQuestEndAsync(isWin, subscribeToken), AwaitOperation.Drop)
                .AddTo(this);

            return new Tuple<GameObject, QuestExecutionUiPresenter>(presenter.gameObject, presenter);
        }

        /// <summary>
        /// クエスト終了処理を行ないます。
        /// </summary>
        /// <remarks>
        /// 結果を送信した上で、クエスト結果シーケンスへ遷移します。
        /// </remarks>
        /// <param name="isWin">クエストに勝利したかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>クエスト終了処理の非同期タスク</returns>
        private async UniTask PerformQuestEndAsync(bool isWin, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await SequenceModel.SendResultAsync(isWin, token);
            await SceneSequenceLoader.LoadSceneSequenceAsync(new QuestResultManagerArgs(SequenceManagerArgs.QuestId, isWin));
        }
    }
}
