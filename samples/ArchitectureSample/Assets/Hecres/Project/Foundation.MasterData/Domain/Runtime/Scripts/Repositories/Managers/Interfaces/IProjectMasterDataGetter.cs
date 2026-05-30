using Hecres.Frameworks.HecMasterData.Domain.Repositories.Managers.Interfaces;

namespace Hecres.Project.Foundation.MasterData.Domain.Repositories.Managers.Interfaces
{
    /// <summary>
    /// プロジェクトマスターデータの取得インターフェース
    /// </summary>
    public interface IProjectMasterDataGetter : IMasterDataManager, IQuestMasterDataGetter
    {
    }
}
