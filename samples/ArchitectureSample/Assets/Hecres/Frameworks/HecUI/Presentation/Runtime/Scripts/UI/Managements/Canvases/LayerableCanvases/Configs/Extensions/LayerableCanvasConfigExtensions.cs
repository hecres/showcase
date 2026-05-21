using System;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs.Extensions
{
    /// <summary>
    /// Canvasの拡張クラス
    /// </summary>
    public static class LayerableCanvasConfigExtensions
    {
        /// <summary>
        /// 指定した設定をキャンバスに反映します。
        /// </summary>
        /// <param name="self">対象のキャンバス</param>
        /// <param name="config">反映する設定</param>
        public static void SetLayerableCanvasConfig(this Canvas self, LayerableCanvasConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            self.gameObject.name = $"{config.LayerName.Value} (order: {config.LayerSortingOrder.Value})";
            self.sortingOrder = config.LayerSortingOrder.Value;
            if (config.RenderCamera != null)
            {
                self.worldCamera = config.RenderCamera;
            }
        }
    }
}
