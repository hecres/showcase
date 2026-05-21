using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Core.HecUnity.Presentation.UnityObjects.Components.Behaviours.MonoBehaviours.Bases;
using Hecres.Core.HecUnity.Presentation.UnityObjects.GameObjects;
using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.Configs;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces;
using VContainer;
using VContainer.Unity;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers
{
    /// <summary>
    /// シーンシーケンスUIの管理クラス
    /// </summary>
    public partial class SceneSequenceUiManager : HecUnityMonoBehaviourBase, ISceneSequenceUiManager
    {
        [Inject] private IObjectResolver resolver;
        [Inject] private IUiRegistrar uiRegistrar;

        private readonly GameObjectCollection gameObjectCollection = new();
        private IReadOnlyDictionary<SceneSequenceUiLayerType, LayerableCanvasConfig> canvasConfigTable;

        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="newCanvasConfigTable">レイヤー化対応キャンバスの設定テーブル</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public async UniTask InitializeAsync(IReadOnlyDictionary<SceneSequenceUiLayerType, LayerableCanvasConfig> newCanvasConfigTable, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            canvasConfigTable = newCanvasConfigTable ?? throw new ArgumentNullException(nameof(newCanvasConfigTable));

            foreach (var item in newCanvasConfigTable)
            {
                uiRegistrar.AddLayer(item.Value);
            }
        }

        /// <summary>
        /// 管理下のすべてのUIを破棄します。
        /// </summary>
        public void CleanAllManagedUi()
        {
            gameObjectCollection.DisposeAll();
        }

        /// <summary>
        /// このクラスのインスタンスを検索し、コンテナへバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        /// <returns>バインドされたインスタンス</returns>
        public static SceneSequenceUiManager FindAndBind(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var instance = FindAnyObjectByType<SceneSequenceUiManager>();
            builder.RegisterComponent(instance).As<ISceneSequenceUiManager>().As<ISceneSequenceUiCreator>();

            return instance;
        }
    }
}
