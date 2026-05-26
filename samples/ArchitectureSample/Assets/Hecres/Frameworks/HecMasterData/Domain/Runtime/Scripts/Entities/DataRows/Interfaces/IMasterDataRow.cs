namespace Hecres.Frameworks.HecMasterData.Domain.Entities.DataRows.Interfaces
{
    /// <summary>
    /// マスターデータの行データインターフェース
    /// </summary>
    public interface IMasterDataRow<out TId>
    {
        /// <summary>
        /// マスターデータID
        /// </summary>
        public TId DataId { get; }
    }
}
