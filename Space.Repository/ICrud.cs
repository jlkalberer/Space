using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.Repository
{
    public interface ICrud<in TKey, TValue>
    {
        /// <summary>
        /// Creates an item in the datastore
        /// </summary>
        /// <returns></returns>
        TValue Create();

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TValue Add(TValue entity);

        /// <summary>
        /// Gets an item from the datastore using the supplied key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue Get(TKey key);

        /// <summary>
        /// Update a value in the datastore based on the item's key
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Update(TValue value);

        /// <summary>
        /// Deletes an item from the datastore
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Delete(TKey key);

        /// <summary>
        /// Get a queryable collection of entities
        /// </summary>
        IQueryable<TValue> All { get; }

        /// <summary>
        /// Save the changes to the data store
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
