using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces
{
    /// <summary>
    /// UI登録機能を提供するインターフェース
    /// </summary>
    public interface IUiRegistrar
    {
        /// <summary>
        /// レイヤー登録前のUIの仮置き場
        /// </summary>
        Transform TempPlacing { get; }

        /// <summary>
        /// レイヤー用キャンバスを追加します。
        /// </summary>
        /// <param name="configs">追加するレイヤー設定の配列</param>
        void AddLayers(params LayerableCanvasConfig[] configs);

        /// <summary>
        /// レイヤー用キャンバスを追加します。
        /// </summary>
        /// <param name="config">追加するレイヤーの設定</param>
        void AddLayer(LayerableCanvasConfig config);

        /// <summary>
        /// 指定レイヤーに指定UIを登録します。
        /// </summary>
        /// <param name="config">登録先レイヤー</param>
        /// <param name="uiGameObject">登録対象UI</param>
        /// <param name="isScreenUi">true: セーフエリアを無視して画面全体に表示する</param>
        void AddUi(LayerableCanvasConfig config, GameObject uiGameObject, bool isScreenUi = false);
    }
}
