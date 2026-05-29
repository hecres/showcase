using System;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;

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
        public string QuestId { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">実行対象のクエストID</param>
        public QuestExecutionManagerArgs(string questId)
        {
            if (string.IsNullOrEmpty(questId)) throw new ArgumentException("値が空です", nameof(questId));

            QuestId = questId;
        }
    }
}
