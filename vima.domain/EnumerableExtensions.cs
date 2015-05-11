using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vima.domain
{
    /// <summary>
    ///     Contains a collection of extension methods on <seealso cref="IEnumerable{T}" /> objects.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region : Extension Methods :

        /// <summary>
        ///     Determines whether the specified collection has elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <returns>
        ///     <c>true</c> if the specified collection has elements; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasElements<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        /// <summary>
        /// Determines whether the specified collection has elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        ///   <c>true</c> if the specified collection has elements; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasElements<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return predicate == null
                ? collection.HasElements()
                : collection != null && collection.Any(predicate);
        }

        /// <summary>
        /// Typesafe way which etermines if the collection has no records or is not instantiated.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection</typeparam>
        /// <param name="collection">A collection of objects of the specified type.</param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.HasElements();
        }

        #endregion
    }
}