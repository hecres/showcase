using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Bases;

namespace Hecres.Frameworks.HecMasterData.Domain.ValueObjects.DataTables.DataTypes
{
    /// <summary>
    /// マスターデータ名の型クラス
    /// </summary>
    public class MasterDataTableName : StringDataTypeWrapperBase<MasterDataTableName>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">値</param>
        public MasterDataTableName(string value) : base(value, IsValid, NormalizeValue)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="value">検証対象</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(string value)
        {
            value = NormalizeValue(value);
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="value">処理対象</param>
        /// <returns>前処理後の値</returns>
        private static string NormalizeValue(string value) => value;
    }
}
