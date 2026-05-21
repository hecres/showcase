using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases;
using UnityEngine.SceneManagement;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Interfaces
{
    /// <summary>
    /// シーンシーケンスの管理インターフェース
    /// </summary>
    public interface ISceneSequenceManager
    {
        /// <summary>
        /// シーンシーケンスをアクティベートします。
        /// </summary>
        /// <remarks>
        /// シーン読み込み時のほか、戻る操作などによる再アクティブ化時にも呼び出されます。
        /// </remarks>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        UniTask ActivateSequenceAsync(CancellationToken token);

        /// <summary>
        /// シーンシーケンスのアクティベート後に呼び出されます。
        /// </summary>
        /// <param name="activateType">アクティベートタイプ</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        UniTask OnPostActivateSequenceAsync(SceneSequenceActivateType activateType, CancellationToken token);

        /// <summary>
        /// 所属するシーンを取得します。
        /// </summary>
        /// <returns>所属シーン</returns>
        Scene GetScene();
    }

    /// <summary>
    /// シーンシーケンスの管理インターフェース
    /// </summary>
    public interface ISceneSequenceManager<in TArgs> : ISceneSequenceManager where TArgs : SceneSequenceManagerArgsBase
    {
        /// <summary>
        /// シーンシーケンスを初期化します。
        /// </summary>
        /// <remarks>
        /// シーン読み込み時にのみ呼び出されます。<br/>
        /// 戻る操作などによる再アクティブ化時には呼び出されません。
        /// </remarks>
        /// <param name="args">シーンシーケンス管理の引数</param>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>初期化処理の非同期タスク</returns>
        UniTask InitializeSequenceAsync(TArgs args, CancellationToken token);
    }
}
