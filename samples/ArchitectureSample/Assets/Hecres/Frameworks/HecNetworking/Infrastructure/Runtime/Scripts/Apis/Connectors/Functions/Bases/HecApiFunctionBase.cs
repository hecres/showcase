using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Interfaces;
using R3;

namespace Hecres.Frameworks.HecNetworking.Infrastructure.Apis.Connectors.Functions.Bases
{
    /// <summary>
    /// API疎通処理を行なうクラスの基底
    /// </summary>
    /// <remarks>
    /// サーバーと疎通するアプリの場合、疎通処理をここに実装します。
    /// サーバーレスでローカルのセーブデータを直接編集するアプリの場合、その処理をここに実装します。
    /// </remarks>
    /// <typeparam name="TRequest">APIリクエストの型</typeparam>
    /// <typeparam name="TResult">APIリクエスト結果の型</typeparam>
    public abstract class HecApiFunctionBase<TRequest, TResult>
        where TRequest : IHecApiRequest
    {
        /// <summary>
        /// 最後に処理されたAPIリクエストの結果
        /// </summary>
        public ReadOnlyReactiveProperty<TResult> LatestResult => latestResult;

        private readonly ReactiveProperty<TResult> latestResult = new();

        /// <summary>
        /// APIリクエストを処理します。
        /// </summary>
        /// <param name="request">APIリクエスト</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>APIリクエスト結果</returns>
        public async UniTask<TResult> ProcessAsync(TRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var result = await ProcessInternalAsync(request, token);
            latestResult.Value = result;
            return result;
        }

        /// <summary>
        /// APIリクエストを処理します。
        /// </summary>
        /// <param name="request">APIリクエスト</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>APIリクエスト結果</returns>
        protected abstract UniTask<TResult> ProcessInternalAsync(TRequest request, CancellationToken token);
    }
}
