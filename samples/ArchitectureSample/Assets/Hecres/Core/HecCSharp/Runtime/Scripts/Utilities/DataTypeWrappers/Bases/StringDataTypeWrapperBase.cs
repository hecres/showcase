using System;
using System.Globalization;
using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Interfaces;

namespace Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Bases
{
    /// <summary>
    /// stringの型クラスの基底
    /// </summary>
    /// <typeparam name="T">継承先の型</typeparam>
    public abstract class StringDataTypeWrapperBase<T> : IDataTypeWrapper<string>, IComparable<T>, IEquatable<T>
        where T : StringDataTypeWrapperBase<T>
    {
        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="isValidFunc">値の妥当性検証関数</param>
        /// <param name="normalizeValueFunc">値の正規化関数</param>
        protected StringDataTypeWrapperBase(string value, Func<string, bool> isValidFunc, Func<string, string> normalizeValueFunc)
        {
            if (isValidFunc == null) throw new ArgumentNullException(nameof(isValidFunc));
            if (normalizeValueFunc == null) throw new ArgumentNullException(nameof(normalizeValueFunc));

            if (!isValidFunc.Invoke(value))
            {
                throw new FormatException($"\"{nameof(value)} ({normalizeValueFunc.Invoke(value)})\" format error");
            }

            Value = normalizeValueFunc.Invoke(value);
        }

        /// <summary>
        /// 指定されたオブジェクトと比較します。
        /// </summary>
        /// <param name="target">比較対象のオブジェクト</param>
        /// <returns>比較結果</returns>
        public int CompareTo(object target) => CompareTo((T)target);

        /// <summary>
        /// 指定されたオブジェクトと比較します。
        /// </summary>
        /// <param name="other">比較対象のオブジェクト</param>
        /// <returns>比較結果</returns>
        public int CompareTo(T other) => string.Compare(Value, other.Value, StringComparison.Ordinal);

        /// <summary>
        /// オブジェクトが等しいかどうかを判定します。
        /// </summary>
        /// <param name="target">比較対象のオブジェクト</param>
        /// <returns>true: 等しい / false: 等しくない</returns>
        public override bool Equals(object target) => target != null && EqualsType(target) && Equals((T)target);

        /// <summary>
        /// オブジェクトの型が等しいかどうかを判定します。
        /// </summary>
        /// <param name="target">比較対象のオブジェクト</param>
        /// <returns>true: 等しい / false: 等しくない</returns>
        private bool EqualsType(object target) => GetType() == target.GetType();

        /// <summary>
        /// オブジェクトが等しいかどうかを判定します。
        /// </summary>
        /// <param name="target">比較対象のオブジェクト</param>
        /// <returns>true: 等しい / false: 等しくない</returns>
        public bool Equals(T target) => target != null && Value == target.Value;

        /// <summary>
        /// ハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// 文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列</returns>
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// 指定されたフォーマットプロバイダーを使用して文字列に変換します。
        /// </summary>
        /// <param name="provider">フォーマットプロバイダー</param>
        /// <returns>変換された文字列</returns>
        public string ToString(IFormatProvider provider) => Value.ToString(provider);

        #region Operators

        /// <summary>
        /// 等価演算子
        /// </summary>
        public static bool operator ==(StringDataTypeWrapperBase<T> a, StringDataTypeWrapperBase<T> b) => Equals(a, b);

        /// <summary>
        /// 非等価演算子
        /// </summary>
        public static bool operator !=(StringDataTypeWrapperBase<T> a, StringDataTypeWrapperBase<T> b) => !Equals(a, b);

        #endregion
    }
}
