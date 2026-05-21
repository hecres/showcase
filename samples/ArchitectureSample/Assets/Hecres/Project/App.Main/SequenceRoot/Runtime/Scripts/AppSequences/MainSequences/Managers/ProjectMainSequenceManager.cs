using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Bases;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.DataTypes;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.TitleScreen.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.MainSequences.Managers
{
    /// <summary>
    /// メインシーケンスの管理を行うクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルではシーンシーケンスUIの管理に必要な Default レイヤー 1 枚分の初期化と、
    /// 最初のシーンシーケンス読み込みのみを担います。
    /// </remarks>
    public class ProjectMainSequenceManager : MainSequenceManagerBase
    {
        [SerializeField] private Camera renderCamera;

        [Inject] private ISceneSequenceUiManager sceneSequenceUiManager;

        /// <summary>
        /// 各UIの管理クラスを初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        protected override async UniTask InitializeUiManagersAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            var defaultConfig = new LayerableCanvasConfig(new LayerName("Default"), new LayerSortingOrder(0), renderCamera);
            var table = new Dictionary<SceneSequenceUiLayerType, LayerableCanvasConfig>
            {
                { SceneSequenceUiLayerType.Default, defaultConfig },
            };
            await sceneSequenceUiManager.InitializeAsync(table, token);
        }

        /// <summary>
        /// 最初のシーンシーケンスを読み込みます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込み処理の非同期タスク</returns>
        protected override async UniTask LoadFirstSceneSequenceAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await LoadSceneSequenceAsync(new TitleScreenManagerArgs());
        }

        /// <summary>
        /// このクラスのインスタンスを検索し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>バインドされたインスタンス</returns>
        public static ProjectMainSequenceManager FindAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = FindAnyObjectByType<ProjectMainSequenceManager>();
            builder.RegisterComponent(instance).As<ISceneSequenceLoader>();

            return instance;
        }
    }
}
