using System;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestExecution
{
    /// <summary>
    /// クエスト実行シーケンスのModelクラス
    /// </summary>
    public class QuestExecutionSequence : ProjectSceneSequenceBase
    {
        /// <summary>
        /// 実行対象のクエストID
        /// </summary>
        /// <remarks>
        /// クエスト選択シーケンスから受け渡された、実行するクエストのIDです。
        /// </remarks>
        public QuestDataId QuestId { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="questId">実行対象のクエストID</param>
        public QuestExecutionSequence(QuestDataId questId)
        {
            QuestId = questId ?? throw new ArgumentNullException(nameof(questId));
        }
    }
}
