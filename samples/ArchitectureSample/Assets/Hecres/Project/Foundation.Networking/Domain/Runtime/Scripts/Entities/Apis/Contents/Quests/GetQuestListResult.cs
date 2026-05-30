using System;
using System.Collections.Generic;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests
{
    /// <summary>
    /// クエスト一覧取得リクエスト結果の不変データクラス
    /// </summary>
    public class GetQuestListResult
    {
        /// <summary>
        /// 取得したクエストID一覧
        /// </summary>
        public IReadOnlyList<QuestDataId> QuestIds { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questIds">取得したクエストID一覧</param>
        public GetQuestListResult(IReadOnlyList<QuestDataId> questIds)
        {
            QuestIds = questIds ?? throw new ArgumentNullException(nameof(questIds));
        }
    }
}
