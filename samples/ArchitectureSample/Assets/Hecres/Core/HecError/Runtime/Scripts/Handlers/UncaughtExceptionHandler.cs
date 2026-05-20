using System;
using Hecres.Core.HecError.Handlers.Interfaces;
using R3;
using UnityEngine;

namespace Hecres.Core.HecError.Handlers
{
    /// <summary>
    /// 未捕捉例外を監視するクラス
    /// </summary>
    public class UncaughtExceptionHandler : IUncaughtExceptionHandler, IDisposable
    {
        /// <summary>
        /// 未捕捉例外が発生した時の通知
        /// </summary>
        public Observable<HecError> UncaughtExceptionThrown => uncaughtExceptionStream.Share();

        private readonly Subject<HecError> uncaughtExceptionStream = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UncaughtExceptionHandler()
        {
            Application.logMessageReceivedThreaded += HandleLog;
        }

        /// <summary>
        /// ログを処理します。
        /// </summary>
        /// <param name="condition">条件文字列</param>
        /// <param name="stackTrace">スタックトレース</param>
        /// <param name="type">ログタイプ</param>
        private void HandleLog(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Exception) return;

            // エディタ系の例外はアプリ上でのハンドリングを行わない
            if (stackTrace.StartsWith("UnityEditor.")) return;

            uncaughtExceptionStream.OnNext(new HecError(condition, stackTrace));
        }

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            Application.logMessageReceivedThreaded -= HandleLog;
        }
    }
}
