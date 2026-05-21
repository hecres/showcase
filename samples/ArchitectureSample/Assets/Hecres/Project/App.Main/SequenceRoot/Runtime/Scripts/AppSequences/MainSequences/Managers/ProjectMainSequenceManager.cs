using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Bases;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Interfaces;
using Hecres.Project.App.Main.SequenceRoot.AppSequences.SceneSequences.TitleScreen.Managers;
using VContainer;
using VContainer.Unity;

namespace Hecres.Project.App.Main.SequenceRoot.AppSequences.MainSequences.Managers
{
    /// <summary>
    /// メインシーケンスの管理を行うクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルでは UI 管理クラスの初期化は最小限とし、Title→Home 遷移のための
    /// 最初のシーンシーケンス読み込みのみを担います。
    /// </remarks>
    public class ProjectMainSequenceManager : MainSequenceManagerBase
    {
        /// <summary>
        /// 各UIの管理クラスを初期化します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        protected override UniTask InitializeUiManagersAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            return UniTask.CompletedTask;
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
