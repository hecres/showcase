namespace Hecres.Core.HecUnity.Presentation.DesignPatterns.Mvrp.Presenters.Bases
{
    public abstract partial class MvrpPresenterBase<TModel>
    {
        /// <summary>
        /// 入力処理を初期化します。
        /// </summary>
        protected virtual void InitializeInputActions()
        {
        }

        /// <summary>
        /// 入力処理を有効化します。
        /// </summary>
        protected virtual void EnableInputActions()
        {
        }

        /// <summary>
        /// 入力処理を無効化します。
        /// </summary>
        protected virtual void DisableInputActions()
        {
        }

        /// <summary>
        /// 入力処理のリソースを破棄します。
        /// </summary>
        protected virtual void DisposeInputActions()
        {
        }
    }
}
