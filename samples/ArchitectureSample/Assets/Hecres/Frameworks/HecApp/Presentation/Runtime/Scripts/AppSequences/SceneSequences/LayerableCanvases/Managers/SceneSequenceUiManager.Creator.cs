using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp;
using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers
{
    public partial class SceneSequenceUiManager
    {
        private const SceneSequenceUiLayerType DefaultUiLayerType = SceneSequenceUiLayerType.Default;
        private const bool DefaultIsScreenUi = false;

        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        public async UniTask<GameObject> CreateObjectAsync(GameObject prefab, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateObjectAsync(prefab, DefaultUiLayerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        public async UniTask<GameObject> CreateObjectAsync(GameObject prefab, bool isScreenUi, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateObjectAsync(prefab, DefaultUiLayerType, isScreenUi, token);
        }

        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        public async UniTask<GameObject> CreateObjectAsync(GameObject prefab, SceneSequenceUiLayerType layerType, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateObjectAsync(prefab, layerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        public async UniTask<GameObject> CreateObjectAsync(GameObject prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            var instance = resolver.Instantiate(prefab, uiRegistrar.TempPlacing);
            gameObjectCollection.Add(instance);

            var canvasConfig = canvasConfigTable[layerType];
            uiRegistrar.AddUi(canvasConfig, instance, isScreenUi);

            return instance;
        }

        /// <summary>
        /// デフォルトのUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        public async UniTask<TView> CreateViewAsync<TView>(TView prefab, CancellationToken token) where TView : MonoBehaviour
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateViewAsync(prefab, DefaultUiLayerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// デフォルトのUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        public async UniTask<TView> CreateViewAsync<TView>(TView prefab, bool isScreenUi, CancellationToken token) where TView : MonoBehaviour
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateViewAsync(prefab, DefaultUiLayerType, isScreenUi, token);
        }

        /// <summary>
        /// 指定のUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        public async UniTask<TView> CreateViewAsync<TView>(TView prefab, SceneSequenceUiLayerType layerType, CancellationToken token) where TView : MonoBehaviour
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateViewAsync(prefab, layerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// 指定のUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        public async UniTask<TView> CreateViewAsync<TView>(TView prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token) where TView : MonoBehaviour
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            var instance = resolver.Instantiate(prefab, uiRegistrar.TempPlacing);
            gameObjectCollection.Add(instance);

            var canvasConfig = canvasConfigTable[layerType];
            uiRegistrar.AddUi(canvasConfig, instance.gameObject, isScreenUi);

            return instance;
        }

        /// <summary>
        /// デフォルトのUIレイヤーにUIPresenterを生成します。
        /// </summary>
        /// <typeparam name="TModel">Modelのタイプ</typeparam>
        /// <typeparam name="TPresenter">Presenterのタイプ</typeparam>
        /// <param name="model">UIにバインドするModel</param>
        /// <param name="prefab">生成に使用するPresenterのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したPresenter</returns>
        public async UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateUiPresenterAsync(model, prefab, DefaultUiLayerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// デフォルトのUIレイヤーにUIPresenterを生成します。
        /// </summary>
        /// <typeparam name="TModel">Modelのタイプ</typeparam>
        /// <typeparam name="TPresenter">Presenterのタイプ</typeparam>
        /// <param name="model">UIにバインドするModel</param>
        /// <param name="prefab">生成に使用するPresenterのPrefab</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したPresenter</returns>
        public async UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, bool isScreenUi, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateUiPresenterAsync(model, prefab, DefaultUiLayerType, isScreenUi, token);
        }

        /// <summary>
        /// 指定のUIレイヤーにUIPresenterを生成します。
        /// </summary>
        /// <typeparam name="TModel">Modelのタイプ</typeparam>
        /// <typeparam name="TPresenter">Presenterのタイプ</typeparam>
        /// <param name="model">UIにバインドするModel</param>
        /// <param name="prefab">生成に使用するPresenterのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したPresenter</returns>
        public async UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, SceneSequenceUiLayerType layerType, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            return await CreateUiPresenterAsync(model, prefab, layerType, DefaultIsScreenUi, token);
        }

        /// <summary>
        /// 指定のUIレイヤーにUIPresenterを生成します。
        /// </summary>
        /// <typeparam name="TModel">Modelのタイプ</typeparam>
        /// <typeparam name="TPresenter">Presenterのタイプ</typeparam>
        /// <param name="model">UIにバインドするModel</param>
        /// <param name="prefab">生成に使用するPresenterのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したPresenter</returns>
        public async UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>
        {
            token.ThrowIfCancellationRequested();

            if (model == null) throw new ArgumentNullException(nameof(model));
            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            var instance = await CreateViewAsync(prefab, layerType, isScreenUi, token);
            await MvrpLinker.LinkAsync(model, instance, token);
            return instance;
        }
    }
}
