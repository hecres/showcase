using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace Hecres.Frameworks.HecApp.Presentation.AppSequences.SceneSequences.Presenters.Bases
{
    public partial class SceneSequenceUiPresenterBase<T>
    {
        /// <summary>
        /// 1つ前のシーンシーケンスへの遷移リクエスト時に通知
        /// </summary>
        public Observable<Unit> BackSceneSequenceRequested => backSceneSequenceStream.Share();

        private readonly Subject<Unit> backSceneSequenceStream = new();

        /// <summary>
        /// 1つ前の階層に戻ります。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        public virtual UniTask BackAsync(CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            // デフォルトではシーンシーケンス自体の戻り遷移リクエストを行います
            // メニューUI内で1つ前の階層に戻りたいなど、シーンシーケンス下のUI間でフォーカスを移動させたい場合は継承先でoverrideしてください
            RequestBackSceneSequence();

            return UniTask.CompletedTask;
        }

        /// <summary>
        /// 1つ前のシーンシーケンスへの遷移をリクエストします。
        /// </summary>
        private void RequestBackSceneSequence()
        {
            // ホーム画面などそれ以上戻り遷移できない場面でのリクエストは送信先で弾きます（ここでは判定を行ないません）。
            backSceneSequenceStream.OnNext(Unit.Default);
        }
    }
}
