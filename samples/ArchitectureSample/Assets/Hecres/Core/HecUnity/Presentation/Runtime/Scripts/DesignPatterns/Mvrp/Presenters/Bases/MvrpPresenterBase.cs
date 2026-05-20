using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecCSharp.Utilities.Threading;
using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;

namespace Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Bases
{
    /// <summary>
    /// MVRPパターン対応MonoBehaviourのPresenterクラスの基底
    /// </summary>
    public abstract partial class MvrpPresenterBase<TModel> : HecUnityMonoBehaviourBase, IMvrpPresenter<TModel>
    {
        /// <summary>
        /// RX購読寿命設定用のトークン
        /// </summary>
        public CancellationToken MvrpRxToken => mvrpRxCts.Token;

        /// <summary>
        /// 現在紐づけられているModel
        /// </summary>
        protected TModel Model { get; private set; }

        private readonly CancellationTokenSourceWrapper mvrpRxCts = new();

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        protected virtual void Awake()
        {
            InitializeInputActions();
        }

        /// <summary>
        /// Modelとの紐づけを初期化します。
        /// </summary>
        /// <param name="model">新たに紐づけるModel</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask InitializeModelLinkAsync(TModel model, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));

            UnlinkMvrpRx();
            Model = model;

            await OnInitializeModelLinkAsync(token);
        }

        /// <summary>
        /// Modelとの紐づけ初期化時に呼び出されます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected virtual UniTask OnInitializeModelLinkAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// MVRPのRX紐づけを行います。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask LinkMvrpRxAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            mvrpRxCts.CreateTokenSource();
            await OnLinkMvrpRxAsync(token);
        }

        /// <summary>
        /// MVRPのRX紐づけ時に呼び出されます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        protected virtual UniTask OnLinkMvrpRxAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// MVRPのRX購読を解除します。
        /// </summary>
        public void UnlinkMvrpRx()
        {
            var unlinkedModel = Model;
            mvrpRxCts.Cancel();
            Model = default;

            if (unlinkedModel != null)
            {
                OnUnlinkMvrpRx(unlinkedModel);
            }
        }

        /// <summary>
        /// MVRPのRX紐づけ破棄時に呼び出されます。
        /// </summary>
        /// <param name="unlinkedModel">紐づけ破棄されたModel</param>
        protected virtual void OnUnlinkMvrpRx(TModel unlinkedModel)
        {
        }

        /// <summary>
        /// コンポーネントが有効になった時に呼び出されます。
        /// </summary>
        protected virtual void OnEnable()
        {
            EnableInputActions();
        }

        /// <summary>
        /// コンポーネントが無効になった時に呼び出されます。
        /// </summary>
        protected virtual void OnDisable()
        {
            DisableInputActions();
        }

        /// <summary>
        /// コンポーネントが破棄された時に呼び出されます。
        /// </summary>
        protected virtual void OnDestroy()
        {
            UnlinkMvrpRx();
            DisposeInputActions();
        }
    }
}
