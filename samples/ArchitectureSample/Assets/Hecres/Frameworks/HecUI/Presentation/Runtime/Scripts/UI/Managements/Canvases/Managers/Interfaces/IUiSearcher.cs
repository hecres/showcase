namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.Managers.Interfaces
{
    /// <summary>
    /// UI検索機能を提供するインターフェース
    /// </summary>
    public interface IUiSearcher
    {
        /// <summary>
        /// 管理上もっとも前面に配置されている指定の型のコンポーネントを取得します。
        /// </summary>
        /// <param name="includeInactive">
        /// true: 非アクティブなオブジェクトを検索対象に含める
        /// false: 非アクティブなオブジェクトを検索対象から除外する
        /// </param>
        /// <typeparam name="T">検索対象のコンポーネントの型</typeparam>
        /// <returns>管理上もっとも前面に配置されているコンポーネント</returns>
        T GetFrontMostComponent<T>(bool includeInactive = true) where T : class;
    }
}
