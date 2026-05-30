using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestSelect.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestExecution.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestSelect;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestSelect.Managers
{
    /// <summary>
    /// クエスト選択シーケンスの管理クラス
    /// </summary>
    public class QuestSelectManager : ProjectSceneSequenceManagerBase<QuestSelectManagerArgs, QuestSelectSequence, QuestSelectUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<QuestSelectSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.FromResult(new QuestSelectSequence(ApiRequester, MasterDataGetter));
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, QuestSelectUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            presenter.QuestStartRequested
                .SubscribeAwait(async (questId, subscribeToken) => await LoadQuestExecutionSequenceAsync(questId, subscribeToken), AwaitOperation.Drop)
                .AddTo(this);

            return new Tuple<GameObject, QuestSelectUiPresenter>(presenter.gameObject, presenter);
        }

        /// <summary>
        /// クエスト実行シーケンスへの遷移を行ないます。
        /// </summary>
        /// <param name="questId">実行対象のクエストID</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>遷移処理の非同期タスク</returns>
        private async UniTask LoadQuestExecutionSequenceAsync(QuestDataId questId, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await SceneSequenceLoader.LoadSceneSequenceAsync(new QuestExecutionManagerArgs(questId));
        }
    }
}
