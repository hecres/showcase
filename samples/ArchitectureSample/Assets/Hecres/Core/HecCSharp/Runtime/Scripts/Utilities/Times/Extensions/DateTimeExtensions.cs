using System;

namespace Hecres.Core.HecCSharp.Utilities.Times.Extensions
{
    /// <summary>
    /// DateTimeの拡張クラス
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// タイムスタンプに変換します。
        /// </summary>
        public static long ToTimestamp(this DateTime self)
        {
            var dateTimeOffset = new DateTimeOffset(self.Ticks, new TimeSpan(+09, 00, 00));
            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}
