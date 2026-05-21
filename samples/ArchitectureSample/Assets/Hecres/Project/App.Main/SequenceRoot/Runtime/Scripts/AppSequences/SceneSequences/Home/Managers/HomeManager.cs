using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.Home.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Home;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Home.Managers
{
    /// <summary>
    /// ホームシーケンスの管理クラス
    /// </summary>
    public class HomeManager : ProjectSceneSequenceManagerBase<HomeManagerArgs, HomeSequence, HomeUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<HomeSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var args = new HomeSequence.ManualArgs();
            return UniTask.FromResult(new HomeSequence(args));
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, HomeUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            return new Tuple<GameObject, HomeUiPresenter>(presenter.gameObject, presenter);
        }
    }
}
