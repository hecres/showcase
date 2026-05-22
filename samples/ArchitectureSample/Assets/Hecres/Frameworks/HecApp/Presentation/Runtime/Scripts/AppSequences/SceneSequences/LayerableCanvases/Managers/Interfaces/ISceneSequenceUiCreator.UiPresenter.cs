using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Interfaces;
using UnityEngine;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces
{
    public partial interface ISceneSequenceUiCreator
    {
        /// <summary>
        /// デフォルトのUIレイヤーにUIPresenterを生成します。
        /// </summary>
        /// <typeparam name="TModel">Modelのタイプ</typeparam>
        /// <typeparam name="TPresenter">Presenterのタイプ</typeparam>
        /// <param name="model">UIにバインドするModel</param>
        /// <param name="prefab">生成に使用するPresenterのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したPresenter</returns>
        UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>;

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
        UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, bool isScreenUi, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>;

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
        UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, SceneSequenceUiLayerType layerType, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>;

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
        UniTask<TPresenter> CreateUiPresenterAsync<TModel, TPresenter>(TModel model, TPresenter prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token) where TPresenter : MonoBehaviour, IMvrpPresenter<TModel>;
    }
}
