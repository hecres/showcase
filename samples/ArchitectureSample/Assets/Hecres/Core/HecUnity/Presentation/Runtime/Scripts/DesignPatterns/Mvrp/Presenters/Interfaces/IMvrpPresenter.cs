using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces
{
    /// <summary>
    /// MVRPパターン対応Presenterの初期化インターフェース
    /// </summary>
    public interface IMvrpPresenter
    {
        /// <summary>
        /// MVRPのRX紐づけを行います。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask LinkMvrpRxAsync(CancellationToken token);

        /// <summary>
        /// MVRPのRX購読を解除します。
        /// </summary>
        void UnlinkMvrpRx();
    }

    /// <summary>
    /// MVRPパターン対応Presenterの初期化インターフェース
    /// </summary>
    public interface IMvrpPresenter<in TModel> : IMvrpPresenter
    {
        /// <summary>
        /// Modelとの紐づけを初期化します。
        /// </summary>
        /// <param name="model">新たに紐づけるModel</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask InitializeModelLinkAsync(TModel model, CancellationToken token);
    }
}
