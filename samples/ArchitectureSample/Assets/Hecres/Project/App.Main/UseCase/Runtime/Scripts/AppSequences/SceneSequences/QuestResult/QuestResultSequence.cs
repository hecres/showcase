using System;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestResult
{
    /// <summary>
    /// クエスト結果シーケンスのModelクラス
    /// </summary>
    public class QuestResultSequence : ProjectSceneSequenceBase
    {
        /// <summary>
        /// 結果表示対象のクエストID
        /// </summary>
        public QuestDataId QuestId { get; }

        /// <summary>
        /// クエストに勝利したかどうか
        /// </summary>
        public bool IsWin { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">結果表示対象のクエストID</param>
        /// <param name="isWin">クエストに勝利したかどうか</param>
        public QuestResultSequence(QuestDataId questId, bool isWin)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
            IsWin = isWin;
        }
    }
}
