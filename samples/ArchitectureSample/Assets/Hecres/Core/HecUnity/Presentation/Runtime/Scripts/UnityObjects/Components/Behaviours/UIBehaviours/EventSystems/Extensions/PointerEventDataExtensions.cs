using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.UIBehaviours.EventSystems.Extensions
{
    /// <summary>
    /// PointerEventDataの拡張クラス
    /// </summary>
    public static class PointerEventDataExtensions
    {
        /// <summary>
        /// UGUI上のワールド座標を取得します。
        /// </summary>
        /// <param name="self">対象のPointerEventData</param>
        /// <param name="rootCanvas">ルートCanvas</param>
        /// <param name="rootCanvasRectTransform">ルートCanvasのRectTransform</param>
        /// <returns>UGUI上のワールド座標</returns>
        public static Vector3 GetUguiWorldPosition(this PointerEventData self, Canvas rootCanvas, RectTransform rootCanvasRectTransform)
        {
            var screenPosition = self.position;
            return rootCanvas.renderMode switch
            {
                RenderMode.ScreenSpaceOverlay                        => GetUguiPositionCaseScreenSpace(rootCanvasRectTransform, screenPosition),
                RenderMode.ScreenSpaceCamera or RenderMode.WorldSpace => GetUguiPositionCaseCameraSpace(rootCanvas, rootCanvasRectTransform, screenPosition),
                _                                                     => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Screen Space Overlayモード時の座標を算出します。
        /// </summary>
        /// <param name="rootCanvasRectTransform">ルートCanvasのTransform</param>
        /// <param name="screenPosition">スクリーン座標</param>
        /// <returns>計算された座標</returns>
        private static Vector3 GetUguiPositionCaseScreenSpace(Transform rootCanvasRectTransform, Vector2 screenPosition)
        {
            if (rootCanvasRectTransform == null) throw new ArgumentNullException(nameof(rootCanvasRectTransform));

            return rootCanvasRectTransform.InverseTransformPoint(screenPosition);
        }

        /// <summary>
        /// Screen Space Camera/World Spaceモード時の座標を算出します。
        /// </summary>
        /// <param name="rootCanvas">ルートCanvas</param>
        /// <param name="rootCanvasRectTransform">ルートCanvasのRectTransform</param>
        /// <param name="screenPosition">スクリーン座標</param>
        /// <returns>計算された座標</returns>
        private static Vector3 GetUguiPositionCaseCameraSpace(Canvas rootCanvas, RectTransform rootCanvasRectTransform, Vector2 screenPosition)
        {
            if (rootCanvas == null) throw new ArgumentNullException(nameof(rootCanvas));
            if (rootCanvasRectTransform == null) throw new ArgumentNullException(nameof(rootCanvasRectTransform));

            var result = RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rootCanvasRectTransform,
                screenPosition,
                rootCanvas.worldCamera,
                out var position
            );

            var localPosition = rootCanvasRectTransform.localPosition;
            return result ? position : localPosition;
        }
    }
}
