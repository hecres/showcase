using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.PackageAdapters.UniTaskAdpts.Extensions
{
    /// <summary>
    /// UniTaskの拡張クラス
    /// </summary>
    public static class UniTaskExtensions
    {
        /// <summary>
        /// タスクを開放し、例外発生時にエラーログを出力します。
        /// </summary>
        /// <param name="self">対象のUniTask</param>
        /// <param name="context">エラーログに付加するコンテキスト情報</param>
        [HideInCallstack]
        public static void ForgetSilent(this UniTask self, string context = null)
        {
            self.Forget(ex =>
            {
                if (string.IsNullOrEmpty(context))
                {
                    Debug.LogError(ex);
                }
                else
                {
                    Debug.LogError($"[{context}] {ex}");
                }
            });
        }

        /// <summary>
        /// タスクを開放し、例外発生時にエラーログを出力します。
        /// </summary>
        /// <param name="self">対象のUniTask</param>
        /// <param name="context">エラーログに付加するコンテキスト情報</param>
        [HideInCallstack]
        public static void ForgetSilent<T>(this UniTask<T> self, string context = null)
        {
            self.Forget(ex =>
            {
                if (string.IsNullOrEmpty(context))
                {
                    Debug.LogError(ex);
                }
                else
                {
                    Debug.LogError($"[{context}] {ex}");
                }
            });
        }
    }
}
