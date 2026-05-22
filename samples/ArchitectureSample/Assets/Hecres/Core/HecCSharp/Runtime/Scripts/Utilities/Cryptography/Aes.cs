using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hecres.Core.HecCSharp.Utilities.Cryptography
{
    /// <summary>
    /// AES暗号化・復号化を行うクラス
    /// </summary>
    public static class Aes
    {
        /// <summary>
        /// 暗号化・復号化で使用するエンコーディング
        /// </summary>
        public static Encoding Encoding => Encoding.UTF8;

        private const int BlockSizeBytes = 16;
        private const int SaltSizeBytes = 16;

        /// <summary>
        /// 指定されたキーのbyteサイズでRijndaelManagedインスタンスを生成します。
        /// </summary>
        /// <param name="keySizeBytes">キーのbyteサイズ</param>
        /// <returns>設定済みのRijndaelManagedインスタンス</returns>
        private static RijndaelManaged GenerateManaged(int keySizeBytes)
        {
            return new RijndaelManaged
            {
                BlockSize = SizeBytesToBits(BlockSizeBytes),
                KeySize = SizeBytesToBits(keySizeBytes),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
        }

        /// <summary>
        /// bitサイズをbyteサイズに変換します。
        /// </summary>
        /// <param name="bitsSize">bitサイズ</param>
        /// <returns>byteサイズ</returns>
        private static int SizeBitsToBytes(int bitsSize) => bitsSize / 8;

        /// <summary>
        /// byteサイズをbitサイズに変換します。
        /// </summary>
        /// <param name="bytesSize">byteサイズ</param>
        /// <returns>bitサイズ</returns>
        private static int SizeBytesToBits(int bytesSize) => bytesSize * 8;

        /// <summary>
        /// 256bitサイズのAES暗号化を使用して文字列を暗号化します。
        /// </summary>
        /// <param name="plainValue">暗号化する平文の文字列</param>
        /// <param name="key">暗号化に使用するキー</param>
        /// <returns>Base64エンコードされた暗号化文字列</returns>
        public static string Encrypt256(string plainValue, string key)
        {
            const int keySizeBits = 256;
            return EncryptInternal(plainValue, key, SizeBitsToBytes(keySizeBits));
        }

        /// <summary>
        /// 指定されたキーのbyteサイズで文字列を暗号化します。
        /// </summary>
        /// <param name="plainValue">暗号化する平文の文字列</param>
        /// <param name="key">暗号化に使用するキー</param>
        /// <param name="keySizeBytes">キーのbyteサイズ</param>
        /// <returns>Base64エンコードされた暗号化文字列</returns>
        private static string EncryptInternal(string plainValue, string key, int keySizeBytes)
        {
            using var managed = GenerateManaged(keySizeBytes);
            var deriveBytes = new Rfc2898DeriveBytes(key, SaltSizeBytes);
            var bufferKey = deriveBytes.GetBytes(keySizeBytes);
            managed.Key = bufferKey;
            managed.GenerateIV();

            var encryptor = managed.CreateEncryptor(managed.Key, managed.IV);
            byte[] encrypted;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream, Encoding))
                    {
                        streamWriter.Write(plainValue);
                    }

                    encrypted = memoryStream.ToArray();
                }
            }

            var salt = deriveBytes.Salt;
            var includedSaltAndIv = salt.Concat(managed.IV).Concat(encrypted).ToArray();
            return Convert.ToBase64String(includedSaltAndIv);
        }

        /// <summary>
        /// 256bitサイズのAES暗号化を使用して暗号化された文字列を復号化します。
        /// </summary>
        /// <param name="encryptedValue">復号化するBase64エンコードされた暗号化文字列</param>
        /// <param name="key">復号化に使用するキー</param>
        /// <returns>復号化された平文の文字列</returns>
        public static string Decrypt256ToString(string encryptedValue, string key)
        {
            const int keySizeBits = 256;
            return DecryptToStringInternal(encryptedValue, key, SizeBitsToBytes(keySizeBits));
        }

        /// <summary>
        /// 指定されたキーのbyteサイズで暗号化された文字列を復号化します。
        /// </summary>
        /// <param name="encryptedValue">復号化するBase64エンコードされた暗号化文字列</param>
        /// <param name="key">復号化に使用するキー</param>
        /// <param name="keySizeBytes">キーのbyteサイズ</param>
        /// <returns>復号化された平文の文字列</returns>
        private static string DecryptToStringInternal(string encryptedValue, string key, int keySizeBytes)
        {
            using var managed = GenerateManaged(keySizeBytes);
            var includedSaltAndIv = Convert.FromBase64String(encryptedValue);

            var salt = includedSaltAndIv.Take(SaltSizeBytes).ToArray();
            var withoutSalt = includedSaltAndIv.Skip(salt.Length).ToArray();

            var iv = withoutSalt.Take(BlockSizeBytes).ToArray();
            var withoutSaltAndIv = withoutSalt.Skip(iv.Length).ToArray();

            var deriveBytes = new Rfc2898DeriveBytes(key, salt);
            var bufferKey = deriveBytes.GetBytes(keySizeBytes);
            managed.Key = bufferKey;
            managed.IV = iv;

            using var memoryStream = new MemoryStream(withoutSaltAndIv);
            var decryptor = managed.CreateDecryptor(managed.Key, managed.IV);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            var plain = streamReader.ReadLine();

            return plain;
        }

    }
}
