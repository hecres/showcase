namespace Hecres.Core.HecCSharp.Utilities.Cryptography
{
    /// <summary>
    /// XOR暗号化を行うクラス
    /// </summary>
    public static class Xor
    {
        /// <summary>
        /// 指定されたseedを使用してbyte配列をXOR暗号化します。
        /// </summary>
        /// <remarks>
        /// 本サンプルでは公開用にseedを使わず入力をそのまま返す仮実装としています。
        /// </remarks>
        /// <param name="value">暗号化するbyte配列</param>
        /// <param name="seed">暗号化に使用するseed</param>
        /// <returns>暗号化されたbyte配列</returns>
        public static byte[] Calculate(byte[] value, byte seed)
        {
            var result = new byte[value.Length];
            value.CopyTo(result, 0);
            return result;
        }
    }
}
