using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;

namespace Hecres.Project.Foundation.Networking.Domain.Repositories.Apis.Managers.Interfaces
{
    public partial interface IProjectApiRequester
    {
        /// <summary>
        /// クエスト一覧を取得します。
        /// </summary>
        /// <param name="request">リクエストデータ</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>取得結果</returns>
        UniTask<GetQuestListResult> GetQuestListAsync(GetQuestListRequest request, CancellationToken token);
    }
}
