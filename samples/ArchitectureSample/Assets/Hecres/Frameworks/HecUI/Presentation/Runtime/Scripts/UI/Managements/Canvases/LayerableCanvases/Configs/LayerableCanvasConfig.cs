using System;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.DataTypes;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs
{
    /// <summary>
    /// レイヤー化対応キャンバスの設定クラス
    /// </summary>
    public class LayerableCanvasConfig
    {
        /// <summary>
        /// レイヤー名
        /// </summary>
        public LayerName LayerName { get; }

        /// <summary>
        /// レイヤー表示順
        /// </summary>
        public LayerSortingOrder LayerSortingOrder { get; }

        /// <summary>
        /// 描画用カメラ
        /// </summary>
        public Camera RenderCamera { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LayerableCanvasConfig(LayerName layerName, LayerSortingOrder layerSortingOrder, Camera renderCamera)
        {
            LayerName = layerName;
            LayerSortingOrder = layerSortingOrder;
            RenderCamera = renderCamera ? renderCamera : throw new ArgumentNullException(nameof(renderCamera));
        }
    }
}
