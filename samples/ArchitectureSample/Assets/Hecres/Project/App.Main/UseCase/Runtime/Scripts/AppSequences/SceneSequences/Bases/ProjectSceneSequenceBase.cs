using Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases
{
    /// <summary>
    /// シーンシーケンスのModelクラスの基底
    /// </summary>
    /// <remarks>
    /// 本サンプルではプロジェクト共通の追加機能は持ちません。
    /// 製品コードではプロジェクト共通の依存（API取得・マスターデータ取得など）をここに集約する用途を想定しています。
    /// </remarks>
    public abstract class ProjectSceneSequenceBase : HecSceneSequenceBase
    {
    }
}
