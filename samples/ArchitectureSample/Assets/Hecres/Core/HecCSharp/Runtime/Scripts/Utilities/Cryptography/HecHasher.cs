namespace Hecres.Core.HecCSharp.Utilities.Cryptography
{
    /// <summary>
    /// ハッシュ計算を行うクラス
    /// </summary>
    public static class HecHasher
    {
        /// <summary>
        /// byte配列のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算するbyte配列</param>
        /// <param name="seed">ハッシュ計算に使用するseed</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash(byte[] value, int seed) => Sha.ToHash256(value, seed);

        /// <summary>
        /// int値のSHA256ハッシュを計算します。
        /// </summary>
        /// <param name="value">ハッシュ計算するint値</param>
        /// <param name="seed">ハッシュ計算に使用するseed</param>
        /// <returns>計算されたハッシュ値</returns>
        public static byte[] ToHash(int value, int seed) => Sha.ToHash256(value, seed);
    }
}
