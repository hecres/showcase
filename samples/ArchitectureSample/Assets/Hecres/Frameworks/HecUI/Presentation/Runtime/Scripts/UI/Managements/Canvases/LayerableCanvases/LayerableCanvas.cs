using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs.Extensions;
using UnityEngine;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases
{
    /// <summary>
    /// レイヤー化対応キャンバスの制御クラス
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class LayerableCanvas : HecUnityMonoBehaviourBase
    {
        /// <summary>
        /// キャンバスのレイヤー関連設定
        /// </summary>
        public LayerableCanvasConfig Config { get => config; set => ApplyConfig(value); }

        [SerializeField] private RectTransform safeArea;
        [SerializeField] private RectTransform screenArea;

        private LayerableCanvasConfig config;

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        private void Awake()
        {
            InitializeAsync(destroyCancellationToken).Forget();
        }

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        private async UniTask InitializeAsync(CancellationToken token)
        {
            await UniTask.WaitUntil(IsSafeAreaInitialized, cancellationToken: token);
            ApplySafeAreaSize(GetSafeAreaSize());
        }

        /// <summary>
        /// セーフエリア情報が初期化済みか判定します。
        /// </summary>
        /// <returns>true: 画面サイズとセーフエリアサイズの初期化が十分に完了している場合 / false: 上記以外の場合</returns>
        private static bool IsSafeAreaInitialized()
        {
            var screenSize = new Vector2(Screen.width, Screen.height);
            var safeAreaSize = Screen.safeArea.size;

            const float errorSize = 10f;
            return screenSize.x + errorSize >= safeAreaSize.x && screenSize.y + errorSize >= safeAreaSize.y;
        }

        /// <summary>
        /// セーフエリアのサイズを適用します。
        /// </summary>
        /// <param name="safeAreaSize">セーフエリアのピクセル矩形</param>
        private void ApplySafeAreaSize(Rect safeAreaSize)
        {
            var screenSize = new Vector2Int(Screen.width, Screen.height);
            var anchorMin = safeAreaSize.position;
            var anchorMax = safeAreaSize.position + safeAreaSize.size;
            anchorMin.x /= screenSize.x;
            anchorMin.y /= screenSize.y;
            anchorMax.x /= screenSize.x;
            anchorMax.y /= screenSize.y;
            safeArea.anchorMin = anchorMin;
            safeArea.anchorMax = anchorMax;
        }

        /// <summary>
        /// セーフエリアのサイズを取得します。
        /// </summary>
        /// <returns>セーフエリアの矩形</returns>
        private static Rect GetSafeAreaSize() => Application.isEditor ? GetEditorSafeAreaSize() : GetActualSafeAreaSize();

        /// <summary>
        /// エディタ上のセーフエリアサイズを取得します。
        /// </summary>
        /// <remarks>
        /// iPhone X 相当の解像度の場合は、ノッチを考慮した値に補正します。
        /// </remarks>
        /// <returns>セーフエリアの矩形</returns>
        private static Rect GetEditorSafeAreaSize()
        {
            var isSizeIphoneX = Screen.width == 1125 && Screen.height == 2436;
            if (!isSizeIphoneX) return Screen.safeArea;

            var safeAreaSize = Screen.safeArea;
            safeAreaSize.y = 102;
            safeAreaSize.height = 2202;
            return safeAreaSize;
        }

        /// <summary>
        /// 実機のセーフエリアサイズを取得します。
        /// </summary>
        /// <returns>セーフエリアの矩形</returns>
        private static Rect GetActualSafeAreaSize() => Screen.safeArea;

        /// <summary>
        /// 設定を反映します。
        /// </summary>
        /// <param name="newConfig">レイヤーキャンバス設定</param>
        private void ApplyConfig(LayerableCanvasConfig newConfig)
        {
            config = newConfig ?? throw new ArgumentNullException(nameof(newConfig));

            gameObject.SetActive(true);

            var canvas = gameObject.GetComponentSafely<Canvas>();
            canvas.SetLayerableCanvasConfig(newConfig);
        }

        /// <summary>
        /// UIを追加します。
        /// </summary>
        /// <param name="uiGameObject">追加するUIオブジェクト</param>
        /// <param name="isScreenUi">セーフエリアを無視して画面全体に表示するかどうか</param>
        public void AddUi(GameObject uiGameObject, bool isScreenUi)
        {
            if (uiGameObject == null) throw new ArgumentNullException(nameof(uiGameObject));

            uiGameObject.transform.SetParent(isScreenUi ? screenArea : safeArea, false);
            uiGameObject.transform.SetLocalPositionZ(0);
        }
    }
}
