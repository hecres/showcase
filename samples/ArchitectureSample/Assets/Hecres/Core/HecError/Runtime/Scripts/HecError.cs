namespace Hecres.Core.HecError
{
    /// <summary>
    /// エラー情報の不変データクラス
    /// </summary>
    public class HecError
    {
        // TODO: エラータイプ・エラー番号の管理を整理する
        private const string UncaughtErrorType = "HECUNK";
        private const string UncaughtErrorNumber = "000000";

        /// <summary>
        /// エラータイプ
        /// </summary>
        /// <remarks>
        /// エラー発生個所をおおまかに絞り込むための値です。より詳細なエラー発生個所や原因を特定するためにはエラー番号を参照してください。
        /// </remarks>
        public string ErrorType { get; }

        /// <summary>
        /// エラー番号
        /// </summary>
        /// <remarks>
        /// エラータイプ内で個別のエラーを識別するための番号です。エラータイプと組み合わせてエラーコードを構成します。
        /// </remarks>
        public string ErrorNumber { get; }

        /// <summary>
        /// 開発者向けメッセージ
        /// </summary>
        public string MessageToDeveloper { get; }

        /// <summary>
        /// ユーザー向けメッセージ
        /// </summary>
        public string MessageToUser { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="errorType">エラータイプ</param>
        /// <param name="errorNumber">エラー番号</param>
        /// <param name="messageToUser">ユーザー向けメッセージ</param>
        /// <param name="messageToDeveloper">開発者向けメッセージ</param>
        public HecError(string errorType, string errorNumber, string messageToUser, string messageToDeveloper)
        {
            ErrorType = errorType;
            ErrorNumber = errorNumber;
            MessageToUser = messageToUser;
            MessageToDeveloper = messageToDeveloper;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="condition">例外の条件文字列</param>
        /// <param name="stackTrace">スタックトレース</param>
        public HecError(string condition, string stackTrace)
        {
            ErrorType = UncaughtErrorType;
            ErrorNumber = UncaughtErrorNumber;
            MessageToUser = string.Empty;
            MessageToDeveloper = $"{condition}\n{stackTrace}";
        }
    }
}
