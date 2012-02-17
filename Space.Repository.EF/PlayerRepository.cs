// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerRepository.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The EntityFramework implementation for accessing Players from the datastore.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF
{
    using System;
    using System.Linq;

    using Space.DTO.Players;

    /// <summary>
    /// The EntityFramework implementation for accessing Players from the datastore.
    /// </summary>
    public class PlayerRepository : IPlayerRepository
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly EntityFrameworkDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public PlayerRepository(EntityFrameworkDbContext context)
        {
            this.context = context;
        }

        #region Implementation of ICrud<in int,Player>

        /// <summary>
        /// Gets a queryable collection of entities.
        /// </summary>
        public IQueryable<Player> All
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates an item in the datastore.
        /// </summary>
        /// <returns>Item created in the datastore.</returns>
        public Player Create()
        {
            return this.context.Players.Create();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity">The item to store in the datastore.</param>
        /// <returns>The item stored in the datastore.</returns>
        public Player Add(Player entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key.
        /// </summary>
        /// <param name="key">The primary key of the item.</param>
        /// <returns>The item from the datastore.</returns>
        public Player Get(int key)
        {
            return this.context.Players.SingleOrDefault(p => p.ID == key);
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key.
        /// </summary>
        /// <param name="value">The value to update in the datastore.</param>
        /// <returns>The success status of the update.</returns>
        public bool Update(Player value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an item from the datastore.
        /// </summary>
        /// <param name="key">The primary key of the item to delete.</param>
        /// <returns>The success status of the deletion.</returns>
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save the changes to the data store.
        /// </summary>
        /// <returns>The success status of the save.</returns>
        public bool SaveChanges()
        {
            this.context.SaveChanges();
            return true;
        }

        #endregion
    }
}
