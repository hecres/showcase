using System.Threading;
using Cysharp.Threading.Tasks;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.HierarchySystems.Interfaces
{
    /// <summary>
    /// 表示階層管理対応のViewインターフェース
    /// </summary>
    public interface IHierarchicalUi
    {
        /// <summary>
        /// 1つ前の階層に戻ります。
        /// </summary>
        /// <param name="token">キャンセル用のトークン</param>
        /// <returns>非同期処理のタスク</returns>
        UniTask BackAsync(CancellationToken token);
    }
}
