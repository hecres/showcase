using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.CryptedWrappers.Bases;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.DataTypes
{
    /// <summary>
    /// レイヤーソート順の型クラス
    /// </summary>
    public class LayerSortingOrder : CryptedIntBase<LayerSortingOrder>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="plainValue">平文による値</param>
        /// <param name="useEncryption">暗号化を行うかどうか</param>
        public LayerSortingOrder(int plainValue, bool useEncryption = false) : base(plainValue, useEncryption, IsValid, ProcessPlainValue)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="plainValue">検証対象（平文）</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(int plainValue) => true;

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="plainValue">処理対象（平文）</param>
        /// <returns>前処理後の値</returns>
        private static int ProcessPlainValue(int plainValue) => plainValue;

        #region Operators

        /// <summary>
        /// 加算演算子
        /// </summary>
        public static LayerSortingOrder operator +(LayerSortingOrder a, LayerSortingOrder b) => new(a.Value + b.Value);

        /// <summary>
        /// 減算演算子
        /// </summary>
        public static LayerSortingOrder operator -(LayerSortingOrder a, LayerSortingOrder b) => new(a.Value - b.Value);

        /// <summary>
        /// 乗算演算子
        /// </summary>
        public static LayerSortingOrder operator *(LayerSortingOrder a, LayerSortingOrder b) => new(a.Value * b.Value);

        /// <summary>
        /// 除算演算子
        /// </summary>
        public static LayerSortingOrder operator /(LayerSortingOrder a, LayerSortingOrder b) => new(a.Value / b.Value);

        #endregion
    }
}
