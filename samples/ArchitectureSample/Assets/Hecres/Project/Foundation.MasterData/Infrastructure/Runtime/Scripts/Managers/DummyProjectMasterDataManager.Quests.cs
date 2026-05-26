using Hecres.Frameworks.HecMasterData.Domain.Repositories.DataTables.Interfaces;
using Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;
using Hecres.Project.Foundation.MasterData.Infrastructure.DataTables;

namespace Hecres.Project.Foundation.MasterData.Infrastructure.Managers
{
    public partial class DummyProjectMasterDataManager
    {
        /// <summary>
        /// クエストのデータテーブル
        /// </summary>
        public IMasterDataRowGetter<QuestDataId, QuestData> QuestMasterDataTable => questMasterDataTable;

        private readonly DummyQuestMasterDataTable questMasterDataTable;
    }
}
