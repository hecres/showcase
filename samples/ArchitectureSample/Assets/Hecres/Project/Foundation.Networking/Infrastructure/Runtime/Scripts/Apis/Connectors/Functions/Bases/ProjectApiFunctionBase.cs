using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Interfaces;
using Hecres.Frameworks.HecNetworking.Infrastructure.Apis.Connectors.Functions.Bases;

namespace Hecres.Project.Foundation.Networking.Infrastructure.Apis.Connectors.Functions.Bases
{
    /// <summary>
    /// API疎通処理クラスの基底
    /// </summary>
    /// <remarks>
    /// 本サンプルではプロジェクト共通の依存（マスターデータ取得・ユーザー状態管理）を持たない薄い基底です。
    /// 実プロダクトではマスターデータ取得インターフェースやユーザー状態管理インターフェースをこの基底でDI注入し、
    /// 派生API疎通クラスから共通利用する形が想定されます。
    /// </remarks>
    /// <typeparam name="TRequest">APIリクエストの型</typeparam>
    /// <typeparam name="TResult">APIリクエスト結果の型</typeparam>
    public abstract class ProjectApiFunctionBase<TRequest, TResult> : HecApiFunctionBase<TRequest, TResult>
        where TRequest : IHecApiRequest
    {
    }
}
