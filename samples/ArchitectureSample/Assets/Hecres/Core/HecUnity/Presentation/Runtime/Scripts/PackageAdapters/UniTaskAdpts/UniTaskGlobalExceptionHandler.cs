using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hecres.Core.HecUnity.Presentation.PackageAdapters.UniTaskAdpts
{
    /// <summary>
    /// UniTaskの未観測例外をグローバルに処理する静的クラス
    /// </summary>
    public static class UniTaskGlobalExceptionHandler
    {
        /// <summary>
        /// 静的フィールドを初期化します。
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeStaticFields()
        {
            UniTaskScheduler.UnobservedTaskException += HandleUnobservedTaskException;
        }

        /// <summary>
        /// 未観測のUniTask例外をログ出力します。
        /// </summary>
        /// <param name="exception">捕捉された例外</param>
        private static void HandleUnobservedTaskException(Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}
