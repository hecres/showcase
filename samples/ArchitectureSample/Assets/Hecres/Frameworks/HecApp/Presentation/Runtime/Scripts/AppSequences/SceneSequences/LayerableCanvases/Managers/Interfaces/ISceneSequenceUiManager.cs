using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces
{
    /// <summary>
    /// シーンシーケンスUIの管理インターフェース
    /// </summary>
    public interface ISceneSequenceUiManager : ISceneSequenceUiCreator
    {
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="newCanvasConfigTable">レイヤー化対応キャンバスの設定テーブル</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask InitializeAsync(IReadOnlyDictionary<SceneSequenceUiLayerType, LayerableCanvasConfig> newCanvasConfigTable, CancellationToken token);

        /// <summary>
        /// 管理下のすべてのUIを破棄します。
        /// </summary>
        void CleanAllManagedUi();
    }
}
