using Hecres.Frameworks.HecApp.CompositionRoot.AppSequences.MainSequences.Bases;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.MainSequences.Managers;
using VContainer;

namespace Hecres.Project.App.Main.CompositionRoot.AppSequences.MainSequences
{
    /// <summary>
    /// メインシーケンス内におけるオブジェクトの生成/保持/破棄サイクルを管理するDIスコープクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルではメインシーケンス管理のバインドのみを担い、マスターデータ・ユーザーデータ・API
    /// などプロジェクト固有の依存は導入しません。
    /// </remarks>
    public class ProjectMainSequenceLifetimeScope : HecMainSequenceLifetimeScopeBase
    {
        /// <summary>
        /// メインシーケンス管理の依存関係をバインドします。
        /// </summary>
        /// <param name="builder">依存関係を解決するコンテナビルダー</param>
        protected override void FindAndBindMainSequenceManager(IContainerBuilder builder)
        {
            ProjectMainSequenceManager.FindAndBind(builder);
        }
    }
}
