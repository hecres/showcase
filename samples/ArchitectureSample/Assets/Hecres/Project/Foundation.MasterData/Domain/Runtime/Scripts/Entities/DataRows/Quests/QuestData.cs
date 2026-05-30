using System;
using Hecres.Frameworks.HecMasterData.Domain.Entities.DataRows.Interfaces;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests
{
    /// <summary>
    /// クエストの不変データクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルではマスターデータの最小情報のみを保持します。
    /// </remarks>
    public class QuestData : IMasterDataRow<QuestDataId>
    {
        /// <summary>
        /// マスターデータID
        /// </summary>
        public QuestDataId DataId { get; }

        /// <summary>
        /// クエストの表示名
        /// </summary>
        /// <remarks>
        /// 本サンプルではローカライズ対応をオミットしているため、表示文字列をそのまま保持します。
        /// 実プロダクトでは多言語対応文字列IDから参照を引く形が望ましいです。
        /// </remarks>
        public string DisplayName { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataId">マスターデータID</param>
        /// <param name="displayName">クエストの表示名</param>
        public QuestData(QuestDataId dataId, string displayName)
        {
            if (string.IsNullOrEmpty(displayName)) throw new ArgumentException("値が空です", nameof(displayName));

            DataId = dataId ?? throw new ArgumentNullException(nameof(dataId));
            DisplayName = displayName;
        }
    }
}
