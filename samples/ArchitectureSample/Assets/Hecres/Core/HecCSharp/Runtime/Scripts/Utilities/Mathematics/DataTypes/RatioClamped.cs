using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases;

namespace Hecres.Core.HecCSharp.Utilities.Mathematics.DataTypes
{
    /// <summary>
    /// 範囲制限付き割合の暗号化対応値クラス
    /// </summary>
    public class RatioClamped : CryptedFloatBase<RatioClamped>
    {
        private const float MaxPlainValue = 1;
        private const float MinPlainValue = 0;

        /// <summary>
        /// 最大値
        /// </summary>
        public static RatioClamped Max => new(MaxPlainValue);

        /// <summary>
        /// 最小値
        /// </summary>
        public static RatioClamped Min => new(MinPlainValue);

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public RatioClamped(float plainValue, bool useEncryption = false) : base(plainValue, useEncryption, IsValid, ProcessPlainValue)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public RatioClamped(double plainValue, bool useEncryption = false) : this((float)plainValue, useEncryption)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="goal">目標値</param>
        /// <param name="current">現在値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public RatioClamped(float goal, float current, bool useEncryption = false) : this(Create(goal, current), useEncryption)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="plainValue">検証対象（平文）</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(float plainValue)
        {
            var processed = ProcessPlainValue(plainValue);
            return processed is >= MinPlainValue and <= MaxPlainValue;
        }

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="plainValue">処理対象（平文）</param>
        /// <returns>前処理後の値</returns>
        private static float ProcessPlainValue(float plainValue) => Clamp(plainValue);

        /// <summary>
        /// 目標値と現在値から割合を生成します。
        /// </summary>
        /// <param name="goal">目標値</param>
        /// <param name="current">現在値</param>
        /// <returns>割合</returns>
        private static float Create(float goal, float current)
        {
            if (goal <= 0) return MinPlainValue;
            if (goal <= current) return MaxPlainValue;

            return current / goal;
        }

        /// <summary>
        /// 値を指定範囲に制限します。
        /// </summary>
        /// <param name="plainValue">処理対象（平文）</param>
        /// <returns>制限後の値</returns>
        private static float Clamp(float plainValue)
        {
            if (plainValue <= MinPlainValue) return MinPlainValue;
            if (plainValue >= MaxPlainValue) return MaxPlainValue;

            return plainValue;
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
        public static RatioClamped operator +(RatioClamped a, RatioClamped b) => new(a.Value + b.Value);

        /// <summary>
        /// 減算演算子
        /// </summary>
        public static RatioClamped operator -(RatioClamped a, RatioClamped b) => new(a.Value - b.Value);

        /// <summary>
        /// 乗算演算子
        /// </summary>
        public static RatioClamped operator *(RatioClamped a, RatioClamped b) => new(a.Value * b.Value);

        /// <summary>
        /// 除算演算子
        /// </summary>
        public static RatioClamped operator /(RatioClamped a, RatioClamped b) => new(a.Value / b.Value);

        #endregion
    }
}
