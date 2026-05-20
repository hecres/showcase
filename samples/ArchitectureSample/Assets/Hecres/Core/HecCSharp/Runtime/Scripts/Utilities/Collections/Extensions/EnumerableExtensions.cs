using System;
using System.Collections.Generic;

namespace Hecres.Core.HecCSharp.Utilities.Collections.Extensions
{
    /// <summary>
    /// IEnumerableの拡張クラス
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 各要素に対して指定のアクションを実行します。
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
        }

        /// <summary>
        /// 各要素に対して指定のアクションを実行します。
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
        {
            var num = 0;
            foreach (var item in self) action(item, num++);
        }

        /// <summary>
        /// インデックス付きのコレクションに変換します。
        /// </summary>
        public static IEnumerable<(T item, int index)> Indexed<T>(this IEnumerable<T> self)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));

            return Impl();

            IEnumerable<(T item, int index)> Impl()
            {
                var i = 0;
                foreach (var item in self)
                {
                    yield return (item, i);
                    ++i;
                }
            }
        }
    }
}
