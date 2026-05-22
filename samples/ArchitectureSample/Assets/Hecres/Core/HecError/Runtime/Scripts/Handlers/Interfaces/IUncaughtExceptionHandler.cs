using R3;

namespace Hecres.Core.HecError.Handlers.Interfaces
{
    /// <summary>
    /// 未捕捉例外の状態取得インターフェース
    /// </summary>
    public interface IUncaughtExceptionHandler
    {
        /// <summary>
        /// 未捕捉例外が発生した時の通知
        /// </summary>
        Observable<HecError> UncaughtExceptionThrown { get; }
    }
}
