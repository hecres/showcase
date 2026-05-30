using System;
using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Interfaces;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests
{
    /// <summary>
    /// クエスト結果送信リクエストの不変データクラス
    /// </summary>
    public class SendQuestReportRequest : IHecApiRequest
    {
        /// <summary>
        /// 結果報告対象のクエストID
        /// </summary>
        public QuestDataId QuestId { get; }

        /// <summary>
        /// クエストに勝利したかどうか
        /// </summary>
        public bool IsWin { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">結果報告対象のクエストID</param>
        /// <param name="isWin">クエストに勝利したかどうか</param>
        public SendQuestReportRequest(QuestDataId questId, bool isWin)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
            IsWin = isWin;
        }
    }
}
