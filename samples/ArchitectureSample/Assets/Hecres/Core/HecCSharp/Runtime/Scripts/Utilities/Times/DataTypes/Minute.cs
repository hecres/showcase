using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases;

namespace Hecres.Core.HecCSharp.Utilities.Times.DataTypes
{
    /// <summary>
    /// 分の型クラス
    /// </summary>
    public class Minute : CryptedFloatBase<Minute>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public Minute(float plainValue, bool useEncryption = false) : base(plainValue, useEncryption, IsValid, ProcessPlainValue)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public Minute(double plainValue, bool useEncryption = false) : this((float)plainValue, useEncryption)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="plainValue">検証対象（平文）</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(float plainValue) => true;

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="plainValue">処理対象（平文）</param>
        /// <returns>前処理後の値</returns>
        private static float ProcessPlainValue(float plainValue) => plainValue;

        /// <summary>
        /// 秒に変換します。
        /// </summary>
        /// <returns>変換後のSeconds</returns>
        public Seconds ToSeconds() => new(Value * 60f);

        #region Operators

        /// <summary>
        /// 加算演算子
        /// </summary>
        public static Minute operator +(Minute a, Minute b) => new(a.Value + b.Value);

        /// <summary>
        /// 減算演算子
        /// </summary>
        public static Minute operator -(Minute a, Minute b) => new(a.Value - b.Value);

        /// <summary>
        /// 乗算演算子
        /// </summary>
        public static Minute operator *(Minute a, Minute b) => new(a.Value * b.Value);

        /// <summary>
        /// 除算演算子
        /// </summary>
        public static Minute operator /(Minute a, Minute b) => new(a.Value / b.Value);

        #endregion
    }
}
