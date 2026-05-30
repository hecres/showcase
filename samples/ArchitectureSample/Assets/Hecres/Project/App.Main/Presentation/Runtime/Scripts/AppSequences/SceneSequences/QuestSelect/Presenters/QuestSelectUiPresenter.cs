using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestSelect.Views;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestSelect;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;
using R3;
using UnityEngine;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestSelect.Presenters
{
    /// <summary>
    /// クエスト選択シーケンスのUIPresenterクラス
    /// </summary>
    /// <remarks>
    /// 受注可能なクエストを一覧表示し、選択されたクエストのIDを通知します。
    /// </remarks>
    public class QuestSelectUiPresenter : SceneSequenceUiPresenterBase<QuestSelectSequence>
    {
        /// <summary>
        /// クエスト開始要求時に通知
        /// </summary>
        /// <remarks>
        /// 通知値は選択されたクエストのIDです。
        /// </remarks>
        public Observable<QuestDataId> QuestStartRequested => questStartRequested;

        [SerializeField] private Transform questItemParent;
        [SerializeField] private QuestItemUi questItemUiPrefab;

        private readonly Subject<QuestDataId> questStartRequested = new();

        /// <summary>
        /// MVRPのRX紐づけ時に呼び出されます。
        /// </summary>
        /// <remarks>
        /// 受注可能なクエストごとにクエスト項目UIを生成し、選択操作を購読します。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override UniTask OnLinkMvrpRxAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            foreach (var questData in Model.AvailableQuests)
            {
                var questItemUi = Instantiate(questItemUiPrefab, questItemParent);
                questItemUi.Setup(questData);
                questItemUi.SelectRequested
                           .Subscribe(selectedQuestData => questStartRequested.OnNext(selectedQuestData.DataId))
                           .AddTo(MvrpRxToken);
            }

            return UniTask.CompletedTask;
        }
    }
}
