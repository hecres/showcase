using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Home;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.Home.Presenters
{
    /// <summary>
    /// ホームシーケンスのUIPresenterクラス
    /// </summary>
    /// <remarks>
    /// クエスト選択シーケンスへ進むためのユーザー操作を通知します。
    /// </remarks>
    public class HomeUiPresenter : SceneSequenceUiPresenterBase<HomeSequence>
    {
        /// <summary>
        /// クエスト選択要求時に通知
        /// </summary>
        public Observable<Unit> QuestSelectRequested => questSelectButton.OnClickAsObservable().Share();

        [SerializeField] private Button questSelectButton;
    }
}
