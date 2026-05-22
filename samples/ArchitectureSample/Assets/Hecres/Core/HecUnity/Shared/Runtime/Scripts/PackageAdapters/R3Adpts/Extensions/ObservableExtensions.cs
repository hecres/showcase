using R3;

namespace Hecres.Core.HecUnity.Shared.PackageAdapters.R3Adpts.Extensions
{
    /// <summary>
    /// Observableの拡張クラス
    /// </summary>
    public static class ObservableExtensions
    {
        /// <summary>
        /// null値のみを通過させます。
        /// </summary>
        /// <param name="source">拡張対象のObservable</param>
        /// <returns>null値のみを通過させるObservable</returns>
        public static Observable<T> WhereNull<T>(this Observable<T> source) where T : class
        {
            return source.Where(value => value == null);
        }

        /// <summary>
        /// 非null値のみを通過させます。
        /// </summary>
        /// <param name="source">拡張対象のObservable</param>
        /// <returns>非null値のみを通過させるObservable</returns>
        public static Observable<T> NotNull<T>(this Observable<T> source) where T : class
        {
            return source.Where(value => value != null);
        }

        /// <summary>
        /// trueのみを通過させます。
        /// </summary>
        /// <param name="source">拡張対象のObservable</param>
        /// <returns>trueのみを通過させるObservable</returns>
        public static Observable<bool> WhereTrue(this Observable<bool> source)
        {
            return source.Where(value => value);
        }

        /// <summary>
        /// falseのみを通過させます。
        /// </summary>
        /// <param name="source">拡張対象のObservable</param>
        /// <returns>falseのみを通過させるObservable</returns>
        public static Observable<bool> WhereFalse(this Observable<bool> source)
        {
            return source.Where(value => !value);
        }
    }
}
