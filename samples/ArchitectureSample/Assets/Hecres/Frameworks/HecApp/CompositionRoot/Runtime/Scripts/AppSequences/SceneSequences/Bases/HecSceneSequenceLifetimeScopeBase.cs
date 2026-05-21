using System;
using VContainer;
using VContainer.Unity;

namespace Hecres.Frameworks.HecApp.CompositionRoot.AppSequences.SceneSequences.Bases
{
    /// <summary>
    /// シーンシーケンス内におけるオブジェクトの生成/保持/破棄サイクルを管理するDIスコープクラスの基底
    /// </summary>
    public abstract class HecSceneSequenceLifetimeScopeBase : LifetimeScope
    {
        /// <summary>
        /// 依存関係をバインドします。
        /// </summary>
        protected override void Configure(IContainerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
        }
    }
}
