using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces
{
    /// <summary>
    /// シーンシーケンスUIの生成インターフェース
    /// </summary>
    public partial interface ISceneSequenceUiCreator
    {
        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        UniTask<GameObject> CreateObjectAsync(GameObject prefab, CancellationToken token);

        /// <summary>
        /// デフォルトのUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        UniTask<GameObject> CreateObjectAsync(GameObject prefab, bool isScreenUi, CancellationToken token);

        /// <summary>
        /// 指定のUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        UniTask<GameObject> CreateObjectAsync(GameObject prefab, SceneSequenceUiLayerType layerType, CancellationToken token);

        /// <summary>
        /// 指定のUIレイヤーにゲームオブジェクトを生成します。
        /// </summary>
        /// <param name="prefab">生成に使用するゲームオブジェクトのPrefab</param>
        /// <param name="layerType">生成先のUIレイヤー</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示させる, false: セーフエリア内に表示させる</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>生成したゲームオブジェクト</returns>
        UniTask<GameObject> CreateObjectAsync(GameObject prefab, SceneSequenceUiLayerType layerType, bool isScreenUi, CancellationToken token);
    }
}
