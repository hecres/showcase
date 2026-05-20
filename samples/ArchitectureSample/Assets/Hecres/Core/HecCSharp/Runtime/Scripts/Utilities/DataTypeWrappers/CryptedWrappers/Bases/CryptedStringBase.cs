using System;
using System.Globalization;
using System.Linq;
using Hecres.Core.HecCSharp.Utilities.Cryptography;
using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Interfaces;

namespace Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases
{
    /// <summary>
    /// 暗号化対応stringの型クラスの基底
    /// </summary>
    public abstract class CryptedStringBase<T> : ICryptedStringValue, IComparable<T>, IEquatable<T>
        where T : CryptedStringBase<T>
    {
        /// <summary>
        /// 復号後の値
        /// </summary>
        public string Value => IsEncrypted() ? HecEncryptor.DecryptString(encryptedValue, seed) : plainValue;

        private readonly string plainValue;
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
        protected CryptedStringBase(string plainValue, bool useEncryption, Func<string, bool> isValidFunc, Func<string, string> processPlainValueFunc)
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
        public static bool operator ==(CryptedStringBase<T> a, CryptedStringBase<T> b) => Equals(a, b);

        /// <summary>
        /// 非等価演算子
        /// </summary>
        public static bool operator !=(CryptedStringBase<T> a, CryptedStringBase<T> b) => !Equals(a, b);

        #endregion
    }
}
