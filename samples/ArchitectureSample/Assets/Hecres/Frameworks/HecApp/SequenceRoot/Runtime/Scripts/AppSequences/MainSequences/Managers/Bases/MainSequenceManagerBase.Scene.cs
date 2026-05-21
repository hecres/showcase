using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Interfaces;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases;
using Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Interfaces;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.MainSequences.Managers.Bases
{
    public abstract partial class MainSequenceManagerBase : ISceneSequenceLoader
    {
        /// <summary>
        /// 最初のシーンシーケンスを読み込みます。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>読み込み処理の非同期タスク</returns>
        protected abstract UniTask LoadFirstSceneSequenceAsync(CancellationToken token);

        /// <summary>
        /// シーンシーケンスを読み込みます。
        /// </summary>
        /// <param name="args">シーンシーケンス引数</param>
        /// <typeparam name="TArgs">シーンシーケンス引数の型</typeparam>
        public async UniTask LoadSceneSequenceAsync<TArgs>(TArgs args) where TArgs : SceneSequenceManagerArgsBase
        {
            if (args == null) throw new ArgumentNullException(nameof(args));

            // 呼び出し元でキャンセル制御すべきものではないため、内部でトークンを設定しています
            var token = destroyCancellationToken;

            await ScreenTransitionUiManager.ShowMainAsync(token);

            // スタックできないシーケンスの場合、現在展開中のシーンシーケンスをすべて解放します
            if (!args.CanStack)
            {
                await UnloadAllSceneSequencesAsync(token);
            }

            // シーンシーケンスを読み込みます
            var address = args.SceneName;
            SceneInstance scene;

            // 現在のシーンからLifetimeScopeを取得して親として設定
            var currentScene = gameObject.scene;
            var parentLifetimeScope = currentScene.GetRootGameObjects().SelectMany(go => go.GetComponentsInChildren<LifetimeScope>(true)).FirstOrDefault();

            if (parentLifetimeScope != null)
            {
                using (LifetimeScope.EnqueueParent(parentLifetimeScope))
                {
                    scene = await Addressables.LoadSceneAsync(address, LoadSceneMode.Additive).ToUniTask(cancellationToken: token);
                }
            }
            else
            {
                scene = await Addressables.LoadSceneAsync(address, LoadSceneMode.Additive).ToUniTask(cancellationToken: token);
            }

            var sceneRootGameObjects = scene.Scene.GetRootGameObjects();
            ISceneSequenceManager<TArgs> nextSceneSequenceManager = null;
            foreach (var item in sceneRootGameObjects)
            {
                nextSceneSequenceManager = item.GetComponentInChildren<ISceneSequenceManager<TArgs>>();
                if (nextSceneSequenceManager != null)
                {
                    break;
                }
            }

            if (nextSceneSequenceManager == null)
            {
                throw new InvalidOperationException($"{scene.Scene.name}シーン内に{nameof(ISceneSequenceManager)}継承コンポーネントが存在しません。");
            }

            sceneSequenceManagerStack.Push(nextSceneSequenceManager);

            // 読み込んだシーンシーケンスの初期化を行ないます
            await nextSceneSequenceManager.InitializeSequenceAsync(args, token);
            await nextSceneSequenceManager.ActivateSequenceAsync(token);
            nextSceneSequenceManager.OnPostActivateSequenceAsync(SceneSequenceActivateType.LoadActivate, token).Forget();
        }

        /// <summary>
        /// 現在操作中のシーンシーケンスを解放します。
        /// </summary>
        public async UniTask UnloadStackSceneSequenceAsync()
        {
            if (!CanUnloadSceneSequence()) throw new InvalidOperationException();

            // 呼び出し元でキャンセル制御すべきものではないため、内部でトークンを設定しています
            var token = destroyCancellationToken;

            await ScreenTransitionUiManager.ShowMainAsync(token);

            // 展開中のシーンシーケンスを解放します
            if (sceneSequenceManagerStack.TryPop(out var targetSceneSequence))
            {
                var targetScene = targetSceneSequence.GetScene();
                if (targetScene.isLoaded)
                {
                    await SceneManager.UnloadSceneAsync(targetScene).ToUniTask(cancellationToken: token);
                }
            }

            // スタックしていたシーンシーケンスを復帰させます
            var nextSceneSequenceManager = sceneSequenceManagerStack.Peek();

            // 復帰させたシーンシーケンスの初期化を行ないます
            await nextSceneSequenceManager.ActivateSequenceAsync(token);
            nextSceneSequenceManager.OnPostActivateSequenceAsync(SceneSequenceActivateType.ReActivate, token).Forget();
        }

        /// <summary>
        /// シーンシーケンスの解放処理を行なえる状態かどうか判定します。
        /// </summary>
        /// <returns>true: 行なえる / false: 行なえない</returns>
        private bool CanUnloadSceneSequence()
        {
            return !InputBlockerUiManager.IsBlocking.CurrentValue && !ScreenTransitionUiManager.AnyVisibleTransition.CurrentValue;
        }

        /// <summary>
        /// 展開中のすべてのシーンシーケンスを解放します。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        private async UniTask UnloadAllSceneSequencesAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            foreach (var sceneSequenceManager in sceneSequenceManagerStack.Where(sceneSequenceManager => sceneSequenceManager.GetScene().isLoaded))
            {
                await SceneManager.UnloadSceneAsync(sceneSequenceManager.GetScene()).ToUniTask(cancellationToken: token);
            }

            sceneSequenceManagerStack.Clear();
        }
    }
}
