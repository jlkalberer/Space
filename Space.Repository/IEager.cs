// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEager.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   An interface used by repositories for eager loading of complex objects.  This allows a single call to
//   build an object graph.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using System.Linq;

    /// <summary>
    /// An interface used by repositories for eager loading of complex objects.  This allows a single call to 
    /// build an object graph.
    /// </summary>
    /// <typeparam name="TKey">
    /// The primary key of the TValue.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// An object type to request from the datastore.
    /// </typeparam>
    public interface IEager<in TKey, out TValue>
    {
        /// <summary>
        /// Gets a queryable collection of items where the returned objects are eagerly loaded.
        /// </summary>
        IQueryable<TValue> EagerAll { get; }

        /// <summary>
        /// Gets an eagerly loaded item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        TValue EagerGet(TKey key);
    }
}
