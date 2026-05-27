using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Project.App.Main.Presentation.AppSequences.SceneSequences.QuestResult.Presenters;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.Bases.Managers;
using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.QuestResult;
using UnityEngine;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.QuestResult.Managers
{
    /// <summary>
    /// クエスト結果シーケンスの管理クラス
    /// </summary>
    public class QuestResultManager : ProjectSceneSequenceManagerBase<QuestResultManagerArgs, QuestResultSequence, QuestResultUiPresenter>
    {
        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected override UniTask<QuestResultSequence> CreateModelAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.FromResult(new QuestResultSequence());
        }

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected override async UniTask<Tuple<GameObject, QuestResultUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var presenter = await SceneSequenceUiCreator.CreateUiPresenterAsync(SequenceModel, sequenceUiPresenterPrefab, token);
            return new Tuple<GameObject, QuestResultUiPresenter>(presenter.gameObject, presenter);
        }
    }
}
