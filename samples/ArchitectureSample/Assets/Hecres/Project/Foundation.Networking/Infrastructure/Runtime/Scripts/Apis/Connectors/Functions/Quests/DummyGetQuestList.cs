using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;
using Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Bases;

namespace Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Quests
{
    /// <summary>
    /// クエスト一覧取得APIの疎通クラス（ダミー実装）
    /// </summary>
    /// <remarks>
    /// 本サンプルではサーバー疎通の代替として、固定のクエストID一覧を返却します。
    /// </remarks>
    public class DummyGetQuestList : ProjectApiFunctionBase<GetQuestListRequest, GetQuestListResult>
    {
        /// <summary>
        /// APIリクエストを処理します。
        /// </summary>
        /// <param name="request">APIリクエスト</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>APIリクエスト結果</returns>
        protected override UniTask<GetQuestListResult> ProcessInternalAsync(GetQuestListRequest request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var questIds = new List<string>
            {
                "quest_001",
                "quest_002",
                "quest_003",
                "quest_004"
            };

            return UniTask.FromResult(new GetQuestListResult(questIds));
        }
    }
}
