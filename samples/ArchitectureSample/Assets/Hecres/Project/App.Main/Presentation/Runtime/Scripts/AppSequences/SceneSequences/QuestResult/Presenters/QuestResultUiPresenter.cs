using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestResult;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestResult.Presenters
{
    /// <summary>
    /// クエスト結果シーケンスのUIPresenterクラス
    /// </summary>
    /// <remarks>
    /// クエストの勝敗結果を表示し、ホームへ戻る要求を通知します。
    /// </remarks>
    public class QuestResultUiPresenter : SceneSequenceUiPresenterBase<QuestResultSequence>
    {
        /// <summary>
        /// ホーム復帰要求時に通知
        /// </summary>
        public Observable<Unit> ReturnHomeRequested => returnHomeButton.OnClickAsObservable().Share();

        [SerializeField] private Text resultLabel;
        [SerializeField] private Button returnHomeButton;

        /// <summary>
        /// MVRPのRX紐づけ時に呼び出されます。
        /// </summary>
        /// <remarks>
        /// クエストの勝敗結果を表示します。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected override UniTask OnLinkMvrpRxAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            resultLabel.text = Model.IsWin ? "勝利" : "敗北";

            return UniTask.CompletedTask;
        }
    }
}
