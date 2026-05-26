using System.Collections.Generic;
using Hecres.Frameworks.HecMasterData.Infrastructure.DataTables.Bases;
using Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.MasterData.Infrastructure.DataTables
{
    /// <summary>
    /// クエストのマスターデータテーブル（ダミー実装）
    /// </summary>
    /// <remarks>
    /// 本サンプルではコンストラクタで固定のダミーデータを保持します。
    /// </remarks>
    public class DummyQuestMasterDataTable : MasterDataTableBase<QuestDataId, QuestData>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DummyQuestMasterDataTable()
        {
            var dummy = new[]
            {
                new QuestData(new QuestDataId("quest_001"), "ダミークエスト 1"),
                new QuestData(new QuestDataId("quest_002"), "ダミークエスト 2"),
                new QuestData(new QuestDataId("quest_003"), "ダミークエスト 3"),
                new QuestData(new QuestDataId("quest_004"), "ダミークエスト 4")
            };

            var table = new Dictionary<QuestDataId, QuestData>();
            foreach (var row in dummy)
            {
                table.Add(row.DataId, row);
            }

            pairs = table;
        }
    }
}
