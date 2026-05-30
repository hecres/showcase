using Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.LayerableCanvases.Managers;
using Hecres.Frameworks.HecInput.Presentation.EventSystems;
using Hecres.Frameworks.HecResource.Domain.Repositories.ResourceLoaders.Interfaces;
using Hecres.Frameworks.HecResource.Infrastructure.ResourceLoaders;
using Hecres.Frameworks.HecResource.Infrastructure.ResourceLoaders.Interfaces;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers;
using Hecres.Frameworks.HecUI.Presentation.UI.Managements.FocusSystems;
using VContainer;
using VContainer.Unity;

namespace Hecres.Frameworks.HecApp.CompositionRoot.AppSequences.MainSequences.Bases
{
    /// <summary>
    /// メインシーケンス内におけるオブジェクトの生成/保持/破棄サイクルを管理するDIスコープクラスの基底
    /// </summary>
    public abstract class HecMainSequenceLifetimeScopeBase : LifetimeScope
    {
        /// <summary>
        /// 依存性注入に用いたイベントシステムのインスタンス
        /// </summary>
        protected HecEventSystem HecEventSystem { get; private set; }

        /// <summary>
        /// 依存性注入に用いたUIフォーカス管理のインスタンス
        /// </summary>
        protected UiFocusSystem UiFocusSystem { get; private set; }

        /// <summary>
        /// キャンバス管理のインスタンス
        /// </summary>
        protected CanvasManager CanvasManager { get; private set; }

        /// <summary>
        /// リソース読み込み機能のインスタンス
        /// </summary>
        protected IHecAddressablesResourceLoader ResourceLoader { get; private set; }

        /// <summary>
        /// 依存関係をバインドします。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            HecEventSystem = HecEventSystem.InstantiateAndBind(builder);
            CanvasManager = CanvasManager.FindAndBind(builder);
            UiFocusSystem = UiFocusSystem.InstantiateAndBind(builder, HecEventSystem, CanvasManager);

            ResourceLoader = new HecUnityAddressables();
            builder.RegisterInstance(ResourceLoader).As<IHecResourceLoader>();

            SceneSequenceUiManager.FindAndBind(builder);

            FindAndBindMainSequenceManager(builder);
        }

        /// <summary>
        /// メインシーケンス管理の依存関係をバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        protected abstract void FindAndBindMainSequenceManager(IContainerBuilder builder);
    }
}
