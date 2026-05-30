using System;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.Foundation.MasterData.Domain.ValueObjects.DataRows.Quests.DataTypes;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestExecution.Managers
{
    /// <summary>
    /// クエスト実行シーケンス管理の引数クラス
    /// </summary>
    public class QuestExecutionManagerArgs : ProjectSceneSequenceManagerArgsBase
    {
        /// <summary>
        /// 実行対象のクエストID
        /// </summary>
        public QuestDataId QuestId { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">実行対象のクエストID</param>
        public QuestExecutionManagerArgs(QuestDataId questId)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
        }
    }
}
