using Hecres.Core.HecError;
using Hecres.Frameworks.HecNetworking.Domain.ValueObjects.Apis.Contents.Bases;

namespace Hecres.Project.Foundation.Networking.Domain.Entities.Apis.Contents.Quests
{
    /// <summary>
    /// クエスト結果送信リクエスト結果の不変データクラス
    /// </summary>
    /// <remarks>
    /// 本サンプルでは送信の成否のみを表し、固有の応答データは持ちません。
    /// </remarks>
    public class SendQuestReportResult : HecApiResultBase
    {
        /// <summary>
        /// 成功状態のコンストラクタ
        /// </summary>
        public SendQuestReportResult()
        {
        }

        /// <summary>
        /// エラー状態のコンストラクタ
        /// </summary>
        /// <param name="error">発生したエラー</param>
        public SendQuestReportResult(HecError error) : base(error)
        {
        }
    }
}
