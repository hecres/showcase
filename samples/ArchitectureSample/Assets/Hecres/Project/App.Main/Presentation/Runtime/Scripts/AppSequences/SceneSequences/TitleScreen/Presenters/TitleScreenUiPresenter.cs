using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.TitleScreen;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.TitleScreen.Presenters
{
    /// <summary>
    /// タイトルシーケンスのUIPresenterクラス
    /// </summary>
    public class TitleScreenUiPresenter : SceneSequenceUiPresenterBase<TitleScreenSequence>
    {
        /// <summary>
        /// ログインリクエスト時に通知
        /// </summary>
        public Observable<Unit> LoginRequested => loginButton.OnClickAsObservable().Share();

        [SerializeField] private Button loginButton;
    }
}
