using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Interfaces
{
    /// <summary>
    /// シーンシーケンスの読み込みインターフェース
    /// </summary>
    public interface ISceneSequenceLoader
    {
        /// <summary>
        /// シーンシーケンスを読み込みます。
        /// </summary>
        /// <param name="args">シーンシーケンス引数</param>
        /// <typeparam name="TArgs">シーンシーケンス引数の型</typeparam>
        UniTask LoadSceneSequenceAsync<TArgs>(TArgs args) where TArgs : SceneSequenceManagerArgsBase;

        /// <summary>
        /// 現在操作中のシーンシーケンスを解放します。
        /// </summary>
        UniTask UnloadStackSceneSequenceAsync();
    }
}
