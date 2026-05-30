using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;
using Hecres.Project.Foundation.Networking.Domain.Repositories.Apis.Managers.Interfaces;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestExecution
{
    /// <summary>
    /// クエスト実行シーケンスのModelクラス
    /// </summary>
    public class QuestExecutionSequence : ProjectSceneSequenceBase
    {
        /// <summary>
        /// 実行対象のクエストID
        /// </summary>
        /// <remarks>
        /// クエスト選択シーケンスから受け渡された、実行するクエストのIDです。
        /// </remarks>
        public QuestDataId QuestId { get; }

        private readonly IProjectApiRequester apiRequester;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">実行対象のクエストID</param>
        /// <param name="apiRequester">APIのリクエストインターフェース</param>
        public QuestExecutionSequence(QuestDataId questId, IProjectApiRequester apiRequester)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
            this.apiRequester = apiRequester ?? throw new ArgumentNullException(nameof(apiRequester));
        }

        /// <summary>
        /// クエストの結果を送信します。
        /// </summary>
        /// <param name="isWin">クエストに勝利したかどうか</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>送信処理の非同期タスク</returns>
        public async UniTask SendResultAsync(bool isWin, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await apiRequester.SendQuestReportAsync(new SendQuestReportRequest(QuestId, isWin), token);
        }
    }
}
