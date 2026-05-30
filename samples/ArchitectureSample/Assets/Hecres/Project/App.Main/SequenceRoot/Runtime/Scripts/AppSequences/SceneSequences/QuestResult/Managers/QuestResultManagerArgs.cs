using System;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestResult.Managers
{
    /// <summary>
    /// クエスト結果シーケンス管理の引数クラス
    /// </summary>
    public class QuestResultManagerArgs : ProjectSceneSequenceManagerArgsBase
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
        public QuestResultManagerArgs(QuestDataId questId, bool isWin)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
            IsWin = isWin;
        }
    }
}
