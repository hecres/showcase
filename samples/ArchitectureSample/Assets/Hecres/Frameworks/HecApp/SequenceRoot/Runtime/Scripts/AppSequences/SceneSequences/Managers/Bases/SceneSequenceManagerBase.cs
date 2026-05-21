using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects;
using Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Interfaces;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers.Interfaces;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases
{
    /// <summary>
    /// シーンシーケンスの管理クラスの基底
    /// </summary>
    public abstract class SceneSequenceManagerBase<TArgs, TModel, TUiPresenter> : HecUnityMonoBehaviourBase, ISceneSequenceManager<TArgs>
        where TArgs : SceneSequenceManagerArgsBase
        where TModel : HecSceneSequenceBase
        where TUiPresenter : ISceneSequenceUiPresenter
    {
        /// <summary>
        /// シーケンスがアクティブかどうか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> IsActive => isActive;

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// 依存性注入リゾルバ
        /// </summary>
        [field: Inject]
        protected IObjectResolver Resolver { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// イベントシステムインターフェース
        /// </summary>
        [field: Inject]
        protected IHecEventSystem HecEventSystem { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// UIフォーカスの管理インターフェース
        /// </summary>
        [field: Inject]
        protected IUiFocusSystem UiFocusSystem { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// シーンシーケンスの読み込みインターフェース
        /// </summary>
        [field: Inject]
        protected ISceneSequenceLoader SceneSequenceLoader { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// 画面遷移演出UIの管理インターフェース
        /// </summary>
        [field: Inject]
        protected IScreenTransitionUiManager ScreenTransitionUiManager { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        /// <summary>
        /// シーンシーケンスUIの生成インターフェース
        /// </summary>
        [field: Inject]
        protected ISceneSequenceUiCreator SceneSequenceUiCreator { get; }

        [SerializeField] protected TUiPresenter sequenceUiPresenterPrefab;

        /// <summary>
        /// シーケンス管理の引数
        /// </summary>
        protected TArgs SequenceManagerArgs { get; private set; }

        /// <summary>
        /// シーケンスのModel
        /// </summary>
        protected TModel SequenceModel { get; private set; }

        /// <summary>
        /// シーケンスのUIPresenter
        /// </summary>
        protected TUiPresenter SequenceUiPresenter { get; private set; }

        private readonly ReactiveProperty<bool> isActive = new();

        /// <summary>
        /// シーンシーケンスを初期化します。
        /// </summary>
        /// <remarks>
        /// シーン読み込み時にのみ呼び出されます。<br/>
        /// 戻る操作などによる再アクティブ化時には呼び出されません。
        /// </remarks>
        /// <param name="args">シーンシーケンス管理の引数</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        public async UniTask InitializeSequenceAsync(TArgs args, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            SequenceManagerArgs = args ?? throw new ArgumentNullException(nameof(args));

            SequenceModel = await CreateModelAsync(token);
            await SequenceModel.InitializeAsync(token);

            var result = await CreateSequenceUiPresenterAsync(token);
            SequenceUiPresenter = result.Item2;

            // this.OnDestroyAsObservable()でsequenceUiPresenterのDestroyとFocus更新を同一フレームで行なおうとすると、Destroyの遅延でFocus更新が正常に動作しません
            // DestroyImmediateで解決できますがエディタコード以外では非推奨となっているため分割しています
            this.OnDestroyAsObservable().Subscribe(_ => GameObjectDestroyer.DestroyIfNotNull(result.Item1));
            SequenceUiPresenter.gameObject.OnDestroyAsObservable().Subscribe(_ => UiFocusSystem.UpdateFocus());
            SequenceUiPresenter.BackSceneSequenceRequested.Subscribe(_ => BackSequenceAsync().Forget()).AddTo(this);
        }

        /// <summary>
        /// シーケンスModelを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成されたシーケンスModel</returns>
        protected abstract UniTask<TModel> CreateModelAsync(CancellationToken token);

        /// <summary>
        /// シーケンスUIPresenterを作成します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>
        /// T1: 生成されたルートのGameObject<br/>
        /// T2: 生成されたシーケンスUIPresenter
        /// </returns>
        protected abstract UniTask<Tuple<GameObject, TUiPresenter>> CreateSequenceUiPresenterAsync(CancellationToken token);

        /// <summary>
        /// シーンシーケンスをアクティベートします。
        /// </summary>
        /// <remarks>
        /// シーン読み込み時のほか、戻る操作などによる再アクティブ化時にも呼び出されます。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>アクティベート処理の非同期タスク</returns>
        public virtual UniTask ActivateSequenceAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            isActive.Value = true;
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// シーンシーケンスのアクティベート後に呼び出されます。
        /// </summary>
        /// <param name="activateType">アクティベートタイプ</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>アクティベート後処理の非同期タスク</returns>
        public virtual async UniTask OnPostActivateSequenceAsync(SceneSequenceActivateType activateType, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            UiFocusSystem.UpdateFocus();
            await ScreenTransitionUiManager.HideMainAsync(token);
        }

        /// <summary>
        /// 現シーンシーケンスを終了し、1つ前のシーンシーケンスに遷移します。
        /// </summary>
        /// <remarks>
        /// ホーム画面など「戻る」が行なえないシーンシーケンスでのリクエストは弾かれます。
        /// </remarks>
        private async UniTask BackSequenceAsync()
        {
            // シーケンス遷移のフェード演出完了前に処理されないよう弾きます
            if (!IsActive.CurrentValue) return;

            // スタックできないシーケンス（それ以上「戻る」が行なえないシーケンス）の終了リクエストは弾きます
            if (!SequenceManagerArgs.CanStack) return;

            // Deactivate（Sceneは残しGameObjectを非アクティブ化）でなくEnd（SceneのUnload）させる理由は以下の通りです
            // 理由1: 再Activateの考慮コストが重い。初回読み込み時と再Activate時の挙動確認が必要
            // 理由2: 再Activateのメリットはロード時間の短縮だが体感できるほどの差は発生しない
            SceneSequenceLoader.UnloadStackSceneSequenceAsync().Forget();
        }

        /// <summary>
        /// 所属するシーンを取得します。
        /// </summary>
        /// <returns>所属シーン</returns>
        public Scene GetScene() => gameObject.scene;

        /// <summary>
        /// コンポーネントが破棄された時に呼び出されます。
        /// </summary>
        protected virtual void OnDestroy()
        {
            isActive.Value = false;
            SequenceModel?.Dispose();
        }
    }
}
