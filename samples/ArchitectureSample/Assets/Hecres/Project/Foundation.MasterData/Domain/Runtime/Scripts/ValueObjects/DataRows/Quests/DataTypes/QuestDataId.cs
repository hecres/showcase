using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Bases;

namespace Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes
{
    /// <summary>
    /// クエストのマスターデータIDの型クラス
    /// </summary>
    public class QuestDataId : StringDataTypeWrapperBase<QuestDataId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">値</param>
        public QuestDataId(string value) : base(value, IsValid, NormalizeValue)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(string value)
        {
            value = NormalizeValue(value);
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>前処理後の値</returns>
        private static string NormalizeValue(string value) => value;
    }
}
