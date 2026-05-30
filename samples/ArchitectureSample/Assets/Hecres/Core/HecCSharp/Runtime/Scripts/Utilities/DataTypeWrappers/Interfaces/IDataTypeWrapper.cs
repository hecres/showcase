using System;

namespace Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Interfaces
{
    /// <summary>
    /// 型ラップ値のインターフェース
    /// </summary>
    public interface IDataTypeWrapper : IComparable
    {
    }

    /// <summary>
    /// 型ラップ値のインターフェース
    /// </summary>
    /// <typeparam name="T">保持する値の型</typeparam>
    public interface IDataTypeWrapper<out T> : IDataTypeWrapper
    {
        /// <summary>
        /// 値
        /// </summary>
        T Value { get; }
    }
}
