using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecError.Handlers.Interfaces;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Interfaces;
using Hecres.Frameworks.HecInput.Presentation.EventSystems.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems.Interfaces;
using R3;
using VContainer;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Bases
{
    /// <summary>
    /// メインシーケンスの管理を行うクラスの基底
    /// </summary>
    public abstract partial class MainSequenceManagerBase : HecUnityMonoBehaviourBase
    {
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
        /// シーンシーケンスUIの管理インターフェース
        /// </summary>
        [field: Inject]
        protected ISceneSequenceUiManager SceneSequenceUiManager { get; }

        private readonly Stack<ISceneSequenceManager> sceneSequenceManagerStack = new();

        private bool isInitialized;

        // ReSharper disable once UnusedMember.Local
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <remarks>
        /// VContainerのInjectによって呼び出されます。<br/>
        /// 子LifetimeScope構築の都度SetContainerが伝播してInjectが複数回走るのを防ぐため、初回のみ実処理を行ないます。
        /// </remarks>
        [Inject]
        private void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;

            InitializeAsync(destroyCancellationToken).Forget();
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        private async UniTask InitializeAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            HandleUncaughtException();
            InitializeInputActions();

            await InitializeUiManagersAsync(token);
            await InitializeInternalAsync(token);

            await LoadFirstSceneSequenceAsync(token);
        }

        /// <summary>
        /// 未キャッチ例外のハンドリングを設定します。
        /// </summary>
        private void HandleUncaughtException()
        {
            var uncaughtExceptionHandler = Resolver.Resolve<IUncaughtExceptionHandler>();
            uncaughtExceptionHandler.UncaughtExceptionThrown.Subscribe(value =>
                                        {
                                            // TODO: 未キャッチ例外発生時のエラーハンドリングを行います（基本はダイアログを出し、タイトルに戻します）。
                                        }
                                    )
                                    .AddTo(this);
        }

        /// <summary>
        /// 各UIの管理クラスを初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        protected abstract UniTask InitializeUiManagersAsync(CancellationToken token);

        /// <summary>
        /// 継承先処理を初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        protected virtual UniTask InitializeInternalAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// コンポーネントが有効になった時に呼び出されます。
        /// </summary>
        private void OnEnable()
        {
            EnableInputActions();
        }

        /// <summary>
        /// コンポーネントが無効になった時に呼び出されます。
        /// </summary>
        private void OnDisable()
        {
            DisableInputActions();
        }

        /// <summary>
        /// コンポーネントが破棄された時に呼び出されます。
        /// </summary>
        private void OnDestroy()
        {
            DisposeInputActions();
        }
    }
}
