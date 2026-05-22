using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Transitions;
using Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers.Interfaces;
using Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Views;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Hecres.Frameworks.HecUI.Toolkit.Presentation.UI.Overlays.ScreenTransitions.Managers
{
    /// <summary>
    /// 画面遷移演出UIの管理クラス
    /// </summary>
    public partial class ScreenTransitionUiManager : HecUnityMonoBehaviourBase, IScreenTransitionUiManager
    {
        /// <summary>
        /// いずれかの画面遷移演出が実行中かどうか
        /// </summary>
        public ReadOnlyReactiveProperty<bool> AnyVisibleTransition => anyVisibleTransition;

        [SerializeField] private Camera renderCamera;
        [SerializeField] private ScreenTransitionUi mainTransitionUiPrefab;
        [SerializeField] private List<ScreenTransitionUi> subTransitionUiPrefabList;

        [Inject] private IUiRegistrar uiRegistrar;

        private ScreenTransitionUi mainTransitionUi;
        private readonly List<ScreenTransitionUi> subTransitionUiList = new();

        private readonly ReactiveProperty<bool> anyVisibleTransition = new(false);

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="mainCanvasConfig">メインの画面遷移演出を置くレイヤー化対応キャンバスの設定</param>
        /// <param name="subCanvasConfig">サブの画面遷移演出を置くレイヤー化対応キャンバスの設定</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask InitializeAsync(LayerableCanvasConfig mainCanvasConfig, LayerableCanvasConfig subCanvasConfig, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (mainCanvasConfig == null) throw new ArgumentNullException(nameof(mainCanvasConfig));
            if (subCanvasConfig == null) throw new ArgumentNullException(nameof(subCanvasConfig));

            uiRegistrar.AddLayer(mainCanvasConfig);
            uiRegistrar.AddLayer(subCanvasConfig);

            mainTransitionUi = await InstantiateUiAsync(mainTransitionUiPrefab, token);
            uiRegistrar.AddUi(mainCanvasConfig, mainTransitionUi.gameObject, true);

            foreach (var prefab in subTransitionUiPrefabList)
            {
                var instance = await InstantiateUiAsync(prefab, token);
                subTransitionUiList.Add(instance);
                uiRegistrar.AddUi(subCanvasConfig, instance.gameObject, true);
            }

            var uiStateStream = new Observable<UiTransitionStateType>[] { mainTransitionUi.State }.Concat(subTransitionUiList.Select(item => item.State));
            uiStateStream.Merge().Subscribe(_ =>
                             {
                                 var isVisibleMain = mainTransitionUi.State.CurrentValue is UiTransitionStateType.Showing or UiTransitionStateType.Showed;
                                 var anyVisibleSub = subTransitionUiList.Any(item => item.State.CurrentValue is UiTransitionStateType.Showing or UiTransitionStateType.Showed);
                                 anyVisibleTransition.Value = isVisibleMain || anyVisibleSub;
                             }
                         )
                         .AddTo(this);
        }

        /// <summary>
        /// UIをインスタンス化します。
        /// </summary>
        /// <param name="prefab">UIPrefab</param>
        /// <param name="token">キャンセル用のトークン</param>
        private async UniTask<ScreenTransitionUi> InstantiateUiAsync(ScreenTransitionUi prefab, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            if (prefab == null) throw new ArgumentNullException(nameof(prefab));

            var instances = await InstantiateAsync(prefab, uiRegistrar.TempPlacing);
            var instance = instances[0];
            instance.HideSoon();
            return instance;
        }

        /// <summary>
        /// このクラスのインスタンスを検索し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>バインドされたインスタンス</returns>
        public static ScreenTransitionUiManager FindAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = FindAnyObjectByType<ScreenTransitionUiManager>();
            builder.RegisterComponent(instance).As<IScreenTransitionUiManager>().As<IScreenTransitionUiOperator>();

            return instance;
        }
    }
}
