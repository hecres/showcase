using System.Collections.Generic;

namespace Hecres.Core.HecCSharp.Utilities.Collections.Extensions
{
    /// <summary>
    /// ICollectionの拡張クラス
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// 指定コレクションの要素を追加します。
        /// </summary>
        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> items)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                self.Add(item);
            }
        }
    }
}
