using System.Text.RegularExpressions;

namespace Hecres.Frameworks.HecApp.SequenceRoot.AppSequences.SceneSequences.Managers.Bases
{
    /// <summary>
    /// シーンシーケンス管理の引数クラスの基底
    /// </summary>
    public abstract class SceneSequenceManagerArgsBase
    {
        /// <summary>
        /// 対応シーン名
        /// </summary>
        public virtual string SceneName
        {
            get
            {
                // 継承先のクラス名からシーン名を生成します
                var derivedType = GetType();
                var className = derivedType.Name;
                var sceneName = Regex.Replace(className, "ManagerArgs$", string.Empty);
                return sceneName;
            }
        }

        /// <summary>
        /// スタック読み込みが可能かどうか
        /// </summary>
        /// <remarks>
        /// ホーム画面など「戻る」操作を受け付けない画面ではfalseに設定してください。
        /// </remarks>
        public virtual bool CanStack => false;
    }
}
