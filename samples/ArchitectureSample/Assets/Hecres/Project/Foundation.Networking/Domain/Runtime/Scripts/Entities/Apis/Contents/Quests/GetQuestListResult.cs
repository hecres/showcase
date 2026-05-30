using System;
using System.Collections.Generic;
using Hecres.Core.HecError;
using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Bases;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests
{
    /// <summary>
    /// クエスト一覧取得リクエスト結果の不変データクラス
    /// </summary>
    public class GetQuestListResult : HecApiResultBase
    {
        /// <summary>
        /// 取得したクエストID一覧
        /// </summary>
        public IReadOnlyList<QuestDataId> QuestIds { get; }

        /// <summary>
        /// 成功状態のコンストラクタ
        /// </summary>
        /// <param name="questIds">取得したクエストID一覧</param>
        public GetQuestListResult(IReadOnlyList<QuestDataId> questIds)
        {
            QuestIds = questIds ?? throw new ArgumentNullException(nameof(questIds));
        }

        /// <summary>
        /// エラー状態のコンストラクタ
        /// </summary>
        /// <param name="error">発生したエラー</param>
        public GetQuestListResult(HecError error) : base(error)
        {
            QuestIds = new List<QuestDataId>();
        }
    }
}
