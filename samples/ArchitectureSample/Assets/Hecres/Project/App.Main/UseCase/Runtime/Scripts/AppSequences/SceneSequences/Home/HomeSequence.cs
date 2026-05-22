using Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Home
{
    /// <summary>
    /// ホームシーケンスのModelクラス
    /// </summary>
    public class HomeSequence : ProjectSceneSequenceBase<HomeSequence.ManualArgs>
    {
        /// <summary>
        /// シーケンス生成時に手渡しする手動引数クラス
        /// </summary>
        public class ManualArgs
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="args">シーケンス生成時に手渡しする手動引数</param>
        public HomeSequence(ManualArgs args) : base(args)
        {
        }
    }
}
