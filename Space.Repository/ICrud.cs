// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICrud.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   An interface used by all repositories.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository
{
    using System.Linq;

    /// <summary>
    /// An interface used by all repositories.
    /// </summary>
    /// <typeparam name="TKey">
    /// The primary key of the TValue.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// An object type to request from the datastore.
    /// </typeparam>
    public interface ICrud<in TKey, TValue>
    {
        /// <summary>
        /// Gets a queryable collection of items.
        /// </summary>
        IQueryable<TValue> All { get; }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        TValue Create();

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        TValue Add(TValue entity);

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        TValue Get(TKey key);

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        bool Update(TValue value);

        /// <summary>
        /// Deletes an item from the datastore.
        /// </summary>
        /// <param name="key">The primary key of the item to delete.</param>
        /// <returns>The success status of the deletion.</returns>
        bool Delete(TKey key);

        /// <summary>
        /// Save the changes to the data store.
        /// </summary>
        /// <returns>The success status of the save.</returns>
        bool SaveChanges();
    }
}
