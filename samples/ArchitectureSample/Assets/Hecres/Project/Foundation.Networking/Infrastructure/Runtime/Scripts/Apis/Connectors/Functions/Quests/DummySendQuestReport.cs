using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Bases;

namespace Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Quests
{
    /// <summary>
    /// クエスト結果送信APIの疎通クラス（ダミー実装）
    /// </summary>
    /// <remarks>
    /// 本サンプルではサーバー疎通の代替として、常に成功結果を返却します。
    /// </remarks>
    public class DummySendQuestReport : ProjectApiFunctionBase<SendQuestReportRequest, SendQuestReportResult>
    {
        /// <summary>
        /// APIリクエストを処理します。
        /// </summary>
        /// <param name="request">APIリクエスト</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>APIリクエスト結果</returns>
        protected override UniTask<SendQuestReportResult> ProcessInternalAsync(SendQuestReportRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (request == null) throw new ArgumentNullException(nameof(request));

            return UniTask.FromResult(new SendQuestReportResult());
        }
    }
}
