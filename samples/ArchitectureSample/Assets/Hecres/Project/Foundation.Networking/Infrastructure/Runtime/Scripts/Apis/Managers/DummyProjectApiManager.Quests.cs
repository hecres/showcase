using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Quests;
using R3;

namespace Hecres.Project.Foundation.Networking.Infrastructure.Apis.Managers
{
    public partial class DummyProjectApiManager
    {
        /// <summary>
        /// クエスト一覧取得結果
        /// </summary>
        public ReadOnlyReactiveProperty<GetQuestListResult> GetQuestListResult => getQuestList.LatestResult;

        /// <summary>
        /// クエスト結果送信結果
        /// </summary>
        public ReadOnlyReactiveProperty<SendQuestReportResult> SendQuestReportResult => sendQuestReport.LatestResult;

        private readonly DummyGetQuestList getQuestList;
        private readonly DummySendQuestReport sendQuestReport;

        /// <summary>
        /// クエスト一覧を取得します。
        /// </summary>
        /// <param name="request">リクエストデータ</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>取得結果</returns>
        public UniTask<GetQuestListResult> GetQuestListAsync(GetQuestListRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return getQuestList.ProcessAsync(request, token);
        }

        /// <summary>
        /// クエストの結果を送信します。
        /// </summary>
        /// <param name="request">リクエストデータ</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>送信結果</returns>
        public UniTask<SendQuestReportResult> SendQuestReportAsync(SendQuestReportRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return sendQuestReport.ProcessAsync(request, token);
        }
    }
}
