using System;
using System.Globalization;
using System.Linq;
using Hecres.Core.HecCSharp.Utilities.Cryptography;
using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Interfaces;

namespace Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases
{
    /// <summary>
    /// 暗号化対応intの型クラスの基底
    /// </summary>
    public abstract class CryptedIntBase<T> : ICryptedIntValue, IComparable<T>, IEquatable<T>, IFormattable
        where T : CryptedIntBase<T>
    {
        /// <summary>
        /// 復号後の値
        /// </summary>
        public int Value => IsEncrypted() ? HecEncryptor.DecryptInt(encryptedValue, seed) : plainValue;

        private readonly int plainValue;
        private readonly byte[] encryptedValue;
        private readonly byte[] hashedEncryptedValue;
        private readonly byte seed;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        /// <param name="isValidFunc">値の妥当性検証関数</param>
        /// <param name="processPlainValueFunc">平文値の前処理関数</param>
        protected CryptedIntBase(int plainValue, bool useEncryption, Func<int, bool> isValidFunc, Func<int, int> processPlainValueFunc)
        {
            if (isValidFunc == null) throw new ArgumentNullException(nameof(isValidFunc));
            if (processPlainValueFunc == null) throw new ArgumentNullException(nameof(processPlainValueFunc));

            if (!isValidFunc.Invoke(plainValue))
            {
                throw new FormatException($"\"{nameof(plainValue)} ({processPlainValueFunc.Invoke(plainValue)})\" format error");
            }

            var processPlainValue = processPlainValueFunc.Invoke(plainValue);
            if (!useEncryption)
            {
                this.plainValue = processPlainValue;
                return;
            }

            seed = Seed.RandomByte();
            encryptedValue = HecEncryptor.Encrypt(processPlainValue, seed);
            hashedEncryptedValue = HecHasher.ToHash(encryptedValue, seed);
        }

        /// <summary>
        /// 暗号化されているかどうかを返します。
        /// </summary>
        /// <returns>true: 暗号化されている / false: 暗号化されていない</returns>
        public bool IsEncrypted() => encryptedValue != null;

        /// <summary>
        /// データが改竄されていないかを確認します。
        /// </summary>
        /// <returns>true: 改竄形跡がなく、整合性が保たれている / false: 改竄形跡があり、整合性が保たれていない</returns>
        public bool IsSecure() => !IsEncrypted() || hashedEncryptedValue.SequenceEqual(HecHasher.ToHash(encryptedValue, seed));

        /// <summary>
        /// 偶数かどうか判定します。
        /// </summary>
        /// <returns>true: 偶数 / false: 奇数</returns>
        public bool IsEven() => Value % 2 == 0;

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
        public int CompareTo(T other) => Value.CompareTo(other.Value);

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

        /// <summary>
        /// 指定されたフォーマットを使用して文字列に変換します。
        /// </summary>
        /// <param name="format">フォーマット</param>
        /// <returns>変換された文字列</returns>
        public string ToString(string format) => Value.ToString(format);

        /// <summary>
        /// 指定されたフォーマットとフォーマットプロバイダーを使用して文字列に変換します。
        /// </summary>
        /// <param name="format">フォーマット</param>
        /// <param name="provider">フォーマットプロバイダー</param>
        /// <returns>変換された文字列</returns>
        public string ToString(string format, IFormatProvider provider) => Value.ToString(format, provider);

        #region Operators

        /// <summary>
        /// 等価演算子
        /// </summary>
        public static bool operator ==(CryptedIntBase<T> a, CryptedIntBase<T> b) => Equals(a, b);

        /// <summary>
        /// 非等価演算子
        /// </summary>
        public static bool operator !=(CryptedIntBase<T> a, CryptedIntBase<T> b) => !Equals(a, b);

        /// <summary>
        /// 小なり演算子
        /// </summary>
        public static bool operator <(CryptedIntBase<T> a, CryptedIntBase<T> b) => a.Value < b.Value;

        /// <summary>
        /// 大なり演算子
        /// </summary>
        public static bool operator >(CryptedIntBase<T> a, CryptedIntBase<T> b) => a.Value > b.Value;

        /// <summary>
        /// 小なり等価演算子
        /// </summary>
        public static bool operator <=(CryptedIntBase<T> a, CryptedIntBase<T> b) => a.Value <= b.Value;

        /// <summary>
        /// 大なり等価演算子
        /// </summary>
        public static bool operator >=(CryptedIntBase<T> a, CryptedIntBase<T> b) => a.Value >= b.Value;

        // newの必要があるため、四則演算のオペレーターは継承先で各自実装してください。

        #endregion
    }
}
