using System;
using System.Text;

namespace Hecres.Core.HecCSharp.Utilities.Cryptography
{
    /// <summary>
    /// 各種データ型の暗号化・復号化を行うクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルでは Xor.Calculate がダミー化されているため、本クラスも公開用の仮実装として動作します（暗号化はバイト配列の型変換のみ）。
    /// </remarks>
    public static class HecEncryptor
    {

        /// <summary>
        /// byte配列を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するbyte配列</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(byte[] value, byte seed) => Xor.Calculate(value, seed);

        /// <summary>
        /// bool値を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するbool値</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(bool value, byte seed) => Xor.Calculate(BitConverter.GetBytes(value), seed);

        /// <summary>
        /// float値を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するfloat値</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(float value, byte seed) => Xor.Calculate(BitConverter.GetBytes(value), seed);

        /// <summary>
        /// double値を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するdouble値</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(double value, byte seed) => Xor.Calculate(BitConverter.GetBytes(value), seed);

        /// <summary>
        /// int値を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するint値</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(int value, byte seed) => Xor.Calculate(BitConverter.GetBytes(value), seed);

        /// <summary>
        /// long値を暗号化します。
        /// </summary>
        /// <param name="value">暗号化するlong値</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(long value, byte seed) => Xor.Calculate(BitConverter.GetBytes(value), seed);

        /// <summary>
        /// 文字列を暗号化します。
        /// </summary>
        /// <param name="value">暗号化する文字列</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Encrypt(string value, byte seed) => Xor.Calculate(Encoding.UTF8.GetBytes(value), seed);

        /// <summary>
        /// 暗号化されたbyte配列をbool値に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化されたbool値</returns>
        public static bool DecryptBoolean(byte[] value, byte seed) => BitConverter.ToBoolean(Xor.Calculate(value, seed), 0);

        /// <summary>
        /// 暗号化されたbyte配列をfloat値に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化されたfloat値</returns>
        public static float DecryptFloat(byte[] value, byte seed) => BitConverter.ToSingle(Xor.Calculate(value, seed), 0);

        /// <summary>
        /// 暗号化されたbyte配列をdouble値に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化されたdouble値</returns>
        public static double DecryptDouble(byte[] value, byte seed) => BitConverter.ToDouble(Xor.Calculate(value, seed), 0);

        /// <summary>
        /// 暗号化されたbyte配列をint値に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化されたint値</returns>
        public static int DecryptInt(byte[] value, byte seed) => BitConverter.ToInt32(Xor.Calculate(value, seed), 0);

        /// <summary>
        /// 暗号化されたbyte配列をlong値に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化されたlong値</returns>
        public static long DecryptLong(byte[] value, byte seed) => BitConverter.ToInt64(Xor.Calculate(value, seed), 0);

        /// <summary>
        /// 暗号化されたbyte配列を文字列に復号化します。
        /// </summary>
        /// <param name="value">復号化するbyte配列</param>
        /// <param name="seed">復号化に使用するseed</param>
        /// <returns>復号化された文字列</returns>
        public static string DecryptString(byte[] value, byte seed) => Encoding.UTF8.GetString(Xor.Calculate(value, seed));

    }
}
