using System;
using System.Threading.Tasks;

namespace Hecres.Core.HecCSharp.Utilities.Threading.Extensions
{
    /// <summary>
    /// Taskの拡張クラス
    /// </summary>
    public static class TaskTimeoutExtensions
    {
        /// <summary>
        /// タスクのタイムアウトを設定します。
        /// </summary>
        public static async Task Timeout(this Task self, TimeSpan timeout)
        {
            var delay = Task.Delay(timeout);
            if (await Task.WhenAny(self, delay) == delay)
            {
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// タスクのタイムアウトを設定します。
        /// </summary>
        public static async Task<T> Timeout<T>(this Task<T> self, TimeSpan timeout)
        {
            await ((Task)self).Timeout(timeout);
            return await self;
        }
    }
}
