using Hecres.Core.HecCSharp.Utilities.DataTypeWrappers.Bases;

namespace Hecres.Frameworks.HecUI.Presentation.UI.Managements.Canvases.LayerableCanvases.DataTypes
{
    /// <summary>
    /// レイヤーソート順の型クラス
    /// </summary>
    public class LayerSortingOrder : IntDataTypeWrapperBase<LayerSortingOrder>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value">値</param>
        public LayerSortingOrder(int value) : base(value, IsValid, NormalizeValue)
        {
        }

        /// <summary>
        /// 値が有効かどうかを返します。
        /// </summary>
        /// <param name="value">検証対象</param>
        /// <returns>true: 有効 / false: 無効</returns>
        public static bool IsValid(int value) => true;

        /// <summary>
        /// 入力値を前処理します。
        /// </summary>
        /// <param name="value">処理対象</param>
        /// <returns>前処理後の値</returns>
        private static int NormalizeValue(int value) => value;

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
