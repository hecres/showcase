using System;
using Hecres.Core.HecError;
using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Interfaces;

namespace Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Bases
{
    /// <summary>
    /// APIリクエスト結果の不変データクラスの基底
    /// </summary>
    public abstract class HecApiResultBase : IHecApiResult
    {
        /// <summary>
        /// APIリクエストが成功したかどうか
        /// </summary>
        public bool IsSuccess => Error == null;

        /// <summary>
        /// APIリクエストエラー
        /// </summary>
        /// <remarks>エラーが発生していない場合はnullを返します。</remarks>
        public HecError Error { get; }

        /// <summary>
        /// 成功状態のコンストラクタ
        /// </summary>
        protected HecApiResultBase()
        {
        }

        /// <summary>
        /// エラー状態のコンストラクタ
        /// </summary>
        /// <param name="error">発生したエラー</param>
        protected HecApiResultBase(HecError error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
