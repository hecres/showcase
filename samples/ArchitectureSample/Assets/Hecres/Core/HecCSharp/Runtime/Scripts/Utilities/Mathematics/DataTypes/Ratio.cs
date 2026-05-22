using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases;

namespace Hecres.Core.HecCSharp.Utilities.Mathematics.DataTypes
{
    /// <summary>
    /// 割合の暗号化対応値クラス
    /// </summary>
    public class Ratio : CryptedFloatBase<Ratio>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public Ratio(float plainValue, bool useEncryption = false) : base(plainValue, useEncryption, IsValid, ProcessPlainValue)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public Ratio(double plainValue, bool useEncryption = false) : this((float)plainValue, useEncryption)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="goal">目標値</param>
        /// <param name="current">現在値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public Ratio(float goal, float current, bool useEncryption = false) : this(Create(goal, current), useEncryption)
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
        /// 目標値と現在値から割合を生成します。
        /// </summary>
        /// <param name="goal">目標値</param>
        /// <param name="current">現在値</param>
        /// <returns>割合</returns>
        private static float Create(float goal, float current)
        {
            if (goal.Equals(0) || current.Equals(0)) return 0;

            return current / goal;
        }

        /// <summary>
        /// %に変換します。
        /// </summary>
        /// <returns>変換後の値</returns>
        public float ToPercentage() => Value * 100;

        #region Operators

        /// <summary>
        /// 加算演算子
        /// </summary>
        public static Ratio operator +(Ratio a, Ratio b) => new(a.Value + b.Value);

        /// <summary>
        /// 減算演算子
        /// </summary>
        public static Ratio operator -(Ratio a, Ratio b) => new(a.Value - b.Value);

        /// <summary>
        /// 乗算演算子
        /// </summary>
        public static Ratio operator *(Ratio a, Ratio b) => new(a.Value * b.Value);

        /// <summary>
        /// 除算演算子
        /// </summary>
        public static Ratio operator /(Ratio a, Ratio b) => new(a.Value / b.Value);

        #endregion
    }
}
