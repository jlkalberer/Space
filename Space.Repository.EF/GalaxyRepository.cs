// -----------------------------------------------------------------------
// <copyright file="GalaxyRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.DTO.Spatial;

namespace Space.Repository.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GalaxyRepository : IGalaxyRepository
    {
        private readonly EFDBContext _context;

        public GalaxyRepository(EFDBContext context)
        {
            _context = context;
        }

        #region Implementation of ICrud<in int,Galaxy>

        /// <summary>
        /// Creates an item in the datastore
        /// </summary>
        /// <returns></returns>
        public Galaxy Create()
        {
            return _context.Galaxies.Create();
        }

        /// <summary>
        /// Used to store a created item.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Galaxy Add(Galaxy entity)
        {
            return _context.Galaxies.Add(entity);
        }

        /// <summary>
        /// Gets an item from the datastore using the supplied key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Galaxy Get(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a value in the datastore based on the item's key
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Update(Galaxy value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an item from the datastore
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a queryable collection of entities
        /// </summary>
        public IQueryable<Galaxy> All
        {
            get { return _context.Galaxies; }
        }

        /// <summary>
        /// Save the changes to the data store
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}
