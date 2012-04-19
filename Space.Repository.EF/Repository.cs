// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   An abstract class for common query functionality.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// An abstract class for common query functionality.
    /// </summary>
    /// <typeparam name="TKey">
    /// The primary key of the TValue.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// An object type to request from the datastore.
    /// </typeparam>
    public abstract class Repository<TKey, TValue> : ICrud<TKey, TValue> where TValue : class
    {
        /// <summary>
        /// Member for the EntityFrameworDbContext
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// An expression used to select an object.
        /// </summary>
        private readonly Func<TKey, TValue, bool> selector;

        /// <summary>
        /// The DbSet we are currently working with.
        /// </summary>
        private readonly DbSet<TValue> dataBaseSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TKey,TValue}"/> class. 
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="selector">
        /// Used to select the object via the primary key.
        /// </param>
        protected Repository(EntityFrameworkDbContext context, Func<TKey, TValue, bool> selector)
        {
            this.context = context;
            this.selector = selector;
            this.dataBaseSet = this.context.Set<TValue>();
        }

        #region Implementation of ICrud<in TKey,TType>

        /// <summary>
        /// Gets a queryable collection of items.
        /// </summary>
        public virtual IQueryable<TValue> All
        {
            get
            {
                return this.dataBaseSet;
            }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public virtual TValue Create()
        {
            return this.dataBaseSet.Create<TValue>();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public virtual TValue Add(TValue entity)
        {
            return this.dataBaseSet.Add(entity);
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public virtual TValue Get(TKey key)
        {
            return this.dataBaseSet.FirstOrDefault(value => this.selector(key, value));
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public virtual bool Update(TValue value)
        {
            this.context.Entry(value);
            return true;
        }

        /// <summary>
        /// Deletes an item from the datastore.
        /// </summary>
        /// <param name="key">The primary key of the item to delete.</param>
        /// <returns>The success status of the deletion.</returns>
        public virtual bool Delete(TKey key)
        {
            var entity = this.Get(key);
            if (entity == null)
            {
                return false;
            }

            this.dataBaseSet.Remove(entity);

            return true;
        }

        /// <summary>
        /// Save the changes to the data store.
        /// </summary>
        /// <returns>The success status of the save.</returns>
        public virtual bool SaveChanges()
        {
            this.context.SaveChanges();
            return true;
        }

        #endregion
    }
}
