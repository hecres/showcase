using System;
using Hecres.Frameworks.HecApp.Domain.Entities.AppSequences.SceneSequences.Bases;

namespace Hecres.Project.App.Main.UseCase.AppSequences.SceneSequences.Bases
{
    /// <summary>
    /// シーンシーケンスのModelクラスの基底
    /// </summary>
    /// <remarks>
    /// 本サンプルでは Title→Home 遷移成立のため、手動引数<see cref="Args"/>のみ受け取ります。<br/>
    /// 製品コードでは DI コンテナ管理外のシーケンス固有引数をここで受け取ります。
    /// </remarks>
    /// <typeparam name="TManualArgs">シーケンス生成時に手渡しする手動引数の型</typeparam>
    public abstract class ProjectSceneSequenceBase<TManualArgs> : HecSceneSequenceBase
    {
        /// <summary>
        /// シーケンス生成時に手渡しする手動引数
        /// </summary>
        protected TManualArgs Args { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="args">シーケンス生成時に手渡しする手動引数</param>
        protected ProjectSceneSequenceBase(TManualArgs args)
        {
            Args = args ?? throw new ArgumentNullException(nameof(args));
        }
    }
}
