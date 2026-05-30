using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestExecution;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestExecution.Presenters
{
    /// <summary>
    /// クエスト実行シーケンスのUIPresenterクラス
    /// </summary>
    /// <remarks>
    /// 実行中のクエストIDを表示し、勝敗の選択結果を通知します。
    /// </remarks>
    public class QuestExecutionUiPresenter : SceneSequenceUiPresenterBase<QuestExecutionSequence>
    {
        /// <summary>
        /// クエスト終了要求時に通知
        /// </summary>
        /// <remarks>
        /// 通知値はクエストに勝利したかどうかです。
        /// </remarks>
        public Observable<bool> QuestEndRequested => Observable.Merge(winButton.OnClickAsObservable().Select(_ => true), loseButton.OnClickAsObservable().Select(_ => false)).Share();

        [SerializeField] private Text questIdLabel;
        [SerializeField] private Button winButton;
        [SerializeField] private Button loseButton;

        /// <summary>
        /// MVRPのRX紐づけ時に呼び出されます。
        /// </summary>
        /// <remarks>
        /// 実行対象のクエストIDを表示します。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override UniTask OnLinkMvrpRxAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            questIdLabel.text = Model.QuestId.Value;

            return UniTask.CompletedTask;
        }
    }
}
