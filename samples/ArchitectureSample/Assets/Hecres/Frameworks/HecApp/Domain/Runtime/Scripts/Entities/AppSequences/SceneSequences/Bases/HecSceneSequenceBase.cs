using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecCSharp.Utilities.Threading;

namespace Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases
{
    /// <summary>
    /// シーンシーケンスのModelクラスの基底
    /// </summary>
    public abstract class HecSceneSequenceBase : IDisposable
    {
        /// <summary>
        /// RX購読寿命設定用のトークン
        /// </summary>
        protected CancellationToken RxToken => rxCts.Token;

        private readonly CancellationTokenSourceWrapper rxCts = new();

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        public virtual UniTask InitializeAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        public virtual void Dispose()
        {
            rxCts.Cancel();
        }
    }
}
