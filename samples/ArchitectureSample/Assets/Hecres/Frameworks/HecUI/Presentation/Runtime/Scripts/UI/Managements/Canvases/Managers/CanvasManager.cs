using System;
using System.Collections.Generic;
using System.Linq;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Extensions;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Transforms.Extensions;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.DataTypes;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers
{
    /// <summary>
    /// キャンバスを管理するクラス
    /// </summary>
    public class CanvasManager : HecUnityMonoBehaviourBase, IUiRegistrar, IUiSearcher
    {
        /// <summary>
        /// レイヤー登録前UIの仮置き場
        /// </summary>
        public Transform TempPlacing => tempPlacingCache ? tempPlacingCache : tempPlacingCache = CreateTempPlacingCanvas().transform;

        [SerializeField] private LayerableCanvas layerableCanvasPrefab;
        [SerializeField] private Camera renderCamera;

        private readonly Dictionary<string, LayerableCanvas> canvasTable = new();

        private Transform tempPlacingCache;

        /// <summary>
        /// コンポーネントを初期化します。
        /// </summary>
        private void Awake()
        {
            tempPlacingCache = TempPlacing;
        }

        /// <summary>
        /// レイヤー用キャンバスを追加します。
        /// </summary>
        /// <param name="configs">追加するレイヤー設定の配列</param>
        public void AddLayers(params LayerableCanvasConfig[] configs)
        {
            if (configs == null) throw new ArgumentNullException(nameof(configs));

            foreach (var item in configs)
            {
                AddLayer(item);
            }
        }

        /// <summary>
        /// レイヤー用キャンバスを追加します。
        /// </summary>
        /// <param name="config">追加するレイヤーの設定</param>
        public void AddLayer(LayerableCanvasConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            var key = GenerateCanvasTableKey(config);
            if (canvasTable.ContainsKey(key))
            {
                Debug.LogWarning($"canvas layer \"{key}\" already exists");
                return;
            }

            canvasTable.Add(key, CreateCanvas(config));

            var canvases = canvasTable.Values.OrderBy(item => item.Config.LayerSortingOrder.Value);
            foreach (var item in canvases)
            {
                item.transform.SetAsLastSibling();
            }
        }

        /// <summary>
        /// 指定レイヤーに指定UIを登録します。
        /// </summary>
        /// <param name="config">登録先レイヤー</param>
        /// <param name="uiGameObject">登録対象UI</param>
        /// <param name="isScreenUi">セーフエリアを無視して画面全体に表示するかどうか</param>
        public void AddUi(LayerableCanvasConfig config, GameObject uiGameObject, bool isScreenUi = false)
        {
            var key = GenerateCanvasTableKey(config);
            if (!canvasTable.TryGetValue(key, out var value))
            {
                Debug.LogWarning($"canvas layer \"{key}\" not exist");
                return;
            }

            value.AddUi(uiGameObject, isScreenUi);
        }

        /// <summary>
        /// 管理上もっとも前面に配置されている指定の型のコンポーネントを取得します。
        /// </summary>
        /// <param name="includeInactive">
        /// true: 非アクティブなオブジェクトを検索対象に含める
        /// false: 非アクティブなオブジェクトを検索対象から除外する
        /// </param>
        /// <typeparam name="T">検索対象のコンポーネントの型</typeparam>
        /// <returns>管理上もっとも前面に配置されているコンポーネント</returns>
        public T GetFrontMostComponent<T>(bool includeInactive = true) where T : class
        {
            // 前面のレイヤーから順に調査します。
            var canvasList = canvasTable.Values.OrderByDescending(item => item.Config.LayerSortingOrder.Value).ToList();
            foreach (var canvas in canvasList)
            {
                var components = canvas.GetComponentsInChildren<T>(includeInactive).Where(component => component != null).Cast<Component>().ToArray();
                if (!components.Any()) continue;

                var frontMost = components.GroupBy(c => c.transform.GetHierarchyDepth(canvas.transform)) // ComponentをGameObjectのTransform深度でグループ化する
                                          .OrderBy(g => g.Key) // 深度が浅い（親に近い）ものを優先 (Key = Depth)
                                          .SelectMany(g => g.OrderByDescending(c => c.transform.GetSiblingIndex()).Select(c => c as T)) // 同じ深度のコンポーネントをSibling Indexの降順（大きいほど前面）でソートして、T型に再キャスト
                                          .First();

                return frontMost;
            }

            return null;
        }

        /// <summary>
        /// レイヤー登録前UIの仮置き用キャンバスを生成します。
        /// </summary>
        /// <returns>作成したキャンバス</returns>
        private LayerableCanvas CreateTempPlacingCanvas()
        {
            var temp = CreateCanvas(new LayerableCanvasConfig(new LayerName("Temp"), new LayerSortingOrder(0), renderCamera));
            temp.gameObject.layer = 0;
            temp.GetComponentSafely<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

            return temp;
        }

        /// <summary>
        /// 指定レイヤーに対応するキャンバスを生成します。
        /// </summary>
        /// <param name="config">レイヤー設定</param>
        /// <returns>作成したキャンバス</returns>
        private LayerableCanvas CreateCanvas(LayerableCanvasConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            var instance = Instantiate(layerableCanvasPrefab, transform);
            instance.Config = config;
            return instance;
        }

        /// <summary>
        /// 指定レイヤーの管理用テーブルキーを生成します。
        /// </summary>
        /// <param name="config">レイヤー設定</param>
        /// <returns>管理用テーブルキー</returns>
        private string GenerateCanvasTableKey(LayerableCanvasConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            return $"{config.LayerName.Value} ({config.LayerSortingOrder.Value})";
        }

        /// <summary>
        /// このクラスのインスタンスを検索し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>バインドされたインスタンス</returns>
        public static CanvasManager FindAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = FindAnyObjectByType<CanvasManager>();
            builder.RegisterComponent(instance).As<IUiRegistrar>().As<IUiSearcher>();

            return instance;
        }
    }
}
