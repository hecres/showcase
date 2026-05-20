using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Hecres.Core.HecCSharp.Utilities.Cryptography
{
    /// <summary>
    /// SHA256ハッシュ計算を行うクラス
    /// </summary>
    public static class Sha
    {
        /// <summary>
        /// ハッシュ計算で使用するエンコーディング
        /// </summary>
        public static Encoding Encoding => Encoding.Unicode;

        /// <summary>
        /// StreamのSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="inputStream">ハッシュ計算するStream</param>
        /// <param name="key">ハッシュ計算に使用するkey</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash256(Stream inputStream, int key)
        {
            var keyBytes = BitConverter.GetBytes(key);
            return new HMACSHA256(keyBytes).ComputeHash(inputStream);
        }

        /// <summary>
        /// byte配列のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算するbyte配列</param>
        /// <param name="key">ハッシュ計算に使用するkey</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash256(byte[] value, int key)
        {
            var keyBytes = BitConverter.GetBytes(key);
            return new HMACSHA256(keyBytes).ComputeHash(value);
        }

        /// <summary>
        /// float値のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算するfloat値</param>
        /// <param name="key">ハッシュ計算に使用するkey</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash256(float value, int key)
        {
            var valueBytes = BitConverter.GetBytes(value);
            return ToHash256(valueBytes, key);
        }

        /// <summary>
        /// int値のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算するint値</param>
        /// <param name="key">ハッシュ計算に使用するkey</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash256(int value, int key)
        {
            var valueBytes = BitConverter.GetBytes(value);
            return ToHash256(valueBytes, key);
        }

        /// <summary>
        /// 文字列のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算する文字列</param>
        /// <param name="key">ハッシュ計算に使用するkey</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash256(string value, int key)
        {
            var valueBytes = Encoding.GetBytes(value);
            return ToHash256(valueBytes, key);
        }
    }
}
