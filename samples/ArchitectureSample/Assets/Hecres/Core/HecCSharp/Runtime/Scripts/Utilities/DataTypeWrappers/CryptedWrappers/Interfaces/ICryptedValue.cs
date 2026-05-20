using System;

namespace Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Interfaces
{
    /// <summary>
    /// 暗号化対応の値型インターフェース
    /// </summary>
    public interface ICryptedValue : IComparable
    {
        /// <summary>
        /// 暗号化されているかどうかを返します。
        /// </summary>
        /// <returns>true: 暗号化されている / false: 暗号化されていない</returns>
        bool IsEncrypted();

        /// <summary>
        /// データが改竄されていないかを確認します。
        /// </summary>
        /// <returns>true: 改竄形跡がなく、整合性が保たれている / false: 改竄形跡があり、整合性が保たれていない</returns>
        bool IsSecure();
    }

    /// <summary>
    /// 暗号化対応の値型インターフェース
    /// </summary>
    public interface ICryptedValue<out T> : ICryptedValue
    {
        /// <summary>
        /// 復号後の値
        /// </summary>
        T Value { get; }
    }
}
