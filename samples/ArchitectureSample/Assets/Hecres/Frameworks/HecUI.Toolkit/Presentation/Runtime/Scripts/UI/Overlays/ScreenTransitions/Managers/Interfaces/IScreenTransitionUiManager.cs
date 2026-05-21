using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;

namespace Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers.Interfaces
{
    /// <summary>
    /// 画面遷移演出UIの管理インターフェース
    /// </summary>
    public interface IScreenTransitionUiManager : IScreenTransitionUiOperator
    {
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="mainCanvasConfig">メインの画面遷移演出を置くレイヤー化対応キャンバスの設定</param>
        /// <param name="subCanvasConfig">サブの画面遷移演出を置くレイヤー化対応キャンバスの設定</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask InitializeAsync(LayerableCanvasConfig mainCanvasConfig, LayerableCanvasConfig subCanvasConfig, CancellationToken token);
    }
}
