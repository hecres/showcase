using System.Linq;
using System.Threading;

namespace Hecres.Core.HecCSharp.Utilities.Threading
{
    /// <summary>
    /// キャンセル処理クラス
    /// </summary>
    public class CancellationTokenSourceWrapper
    {
        /// <summary>
        /// キャンセル通知用のトークン
        /// </summary>
        public CancellationToken Token => tokenSource.Token;

        private CancellationTokenSource tokenSource = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CancellationTokenSourceWrapper()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CancellationTokenSourceWrapper(CancellationToken linkToken)
        {
            CreateTokenSource(linkToken);
        }

        /// <summary>
        /// 指定のTokenとリンクするtokenSourceを作成します。
        /// </summary>
        /// <param name="linkToken">リンクするToken</param>
        public void CreateTokenSource(CancellationToken linkToken)
        {
            Cancel();
            tokenSource = CancellationTokenSource.CreateLinkedTokenSource(linkToken);
        }

        /// <summary>
        /// 指定のTokenとリンクするtokenSourceを作成します。
        /// </summary>
        /// <param name="linkTokens">リンクするTokenの配列</param>
        public void CreateTokenSource(params CancellationToken[] linkTokens)
        {
            Cancel();
            tokenSource = linkTokens.Any() ? CancellationTokenSource.CreateLinkedTokenSource(linkTokens) : new CancellationTokenSource();
        }

        /// <summary>
        /// キャンセルします。
        /// </summary>
        public void Cancel()
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = null;
        }
    }
}
