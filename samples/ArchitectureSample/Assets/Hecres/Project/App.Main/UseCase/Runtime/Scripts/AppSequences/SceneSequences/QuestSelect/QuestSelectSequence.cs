using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases;
using Hecres.Project.Foundation.MasterData.Domain.Entities.DataRows.Quests;
using Hecres.Project.Foundation.MasterData.Domain.Repositories.Managers.Interfaces;
using Hecres.Project.Foundation.Networking.Domain.Repositories.Apis.Managers.Interfaces;
using Hecres.Project.Foundation.Networking.Domain.ValueObjects.Apis.Contents.Quests;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestSelect
{
    /// <summary>
    /// クエスト選択シーケンスのModelクラス
    /// </summary>
    public class QuestSelectSequence : ProjectSceneSequenceBase
    {
        /// <summary>
        /// 受注可能なクエスト一覧
        /// </summary>
        /// <remarks>
        /// <see cref="InitializeAsync"/> の完了後に値が確定します。
        /// </remarks>
        public IReadOnlyList<QuestData> AvailableQuests => availableQuests;

        private readonly IProjectApiRequester apiRequester;
        private readonly IProjectMasterDataGetter masterDataGetter;
        private IReadOnlyList<QuestData> availableQuests = new List<QuestData>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="apiRequester">APIのリクエストインターフェース</param>
        /// <param name="masterDataGetter">マスターデータの取得インターフェース</param>
        public QuestSelectSequence(IProjectApiRequester apiRequester, IProjectMasterDataGetter masterDataGetter)
        {
            this.apiRequester = apiRequester ?? throw new ArgumentNullException(nameof(apiRequester));
            this.masterDataGetter = masterDataGetter ?? throw new ArgumentNullException(nameof(masterDataGetter));
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <remarks>
        /// API経由で受注可能なクエストID一覧を取得し、マスターデータと結合してクエストデータ一覧を構築します。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        public override async UniTask InitializeAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await base.InitializeAsync(token);

            var result = await apiRequester.GetQuestListAsync(new GetQuestListRequest(), token);
            availableQuests = result.QuestIds
                .Select(questId => masterDataGetter.QuestMasterDataTable.GetValue(questId))
                .ToList();
        }
    }
}
