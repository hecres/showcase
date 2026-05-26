using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases;

namespace Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes
{
    /// <summary>
    /// クエストのマスターデータIDの型クラス
    /// </summary>
    public class QuestDataId : CryptedStringBase<QuestDataId>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public QuestDataId(string plainValue, bool useEncryption = false) : base(plainValue, useEncryption, IsValid, ProcessPlainValue)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="plainValue">平文の値</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(string plainValue)
        {
            plainValue = ProcessPlainValue(plainValue);
            return !string.IsNullOrEmpty(plainValue);
        }

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="plainValue">平文の値</param>
        /// <returns>前処理後の値</returns>
        private static string ProcessPlainValue(string plainValue) => plainValue;
    }
}
