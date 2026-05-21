using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces
{
    public partial interface ISceneSequenceUiCreator
    {
        /// <summary>
        /// デフォルトのUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        UniTask<TView> CreateViewAsync<TView>(TView prefab, CancellationToken token) where TView : MonoBehaviour;

        /// <summary>
        /// デフォルトのUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        UniTask<TView> CreateViewAsync<TView>(TView prefab, bool isScreenUi, CancellationToken token) where TView : MonoBehaviour;

        /// <summary>
        /// 指定のUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        UniTask<TView> CreateViewAsync<TView>(TView prefab, SceneSequenceUiLayerType layerType, CancellationToken token) where TView : MonoBehaviour;

        /// <summary>
        /// 指定のUIレイヤーにビューを生成します。
        /// </summary>
        /// <typeparam name="TView">ビューのタイプ</typeparam>
        /// <param name="prefab">生成に使用するビューのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したビュー</returns>
        UniTask<TView> CreateViewAsync<TView>(TView prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token) where TView : MonoBehaviour;
    }
}
