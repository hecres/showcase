using Hecres.Frameworks.HecMasterData.Domain.Repositories.DataTables.Interfaces;
using Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.MasterData.Domain.Repositories.Managers.Interfaces
{
    /// <summary>
    /// クエストマスターデータの取得インターフェース
    /// </summary>
    public interface IQuestMasterDataGetter
    {
        /// <summary>
        /// クエストのデータテーブル
        /// </summary>
        IMasterDataRowGetter<QuestDataId, QuestData> QuestMasterDataTable { get; }
    }
}
