using System;
using System.Data;
using System.Linq;
using Space.DTO.Spatial;

namespace Space.Repository.EF
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly EFDBContext _context;

        public PlanetRepository(EFDBContext context)
        {
            _context = context;
        }

        #region Implementation of ICrud<in int,Planet>

        public Planet Create()
        {
            return _context.SpatialEntities.Create<Planet>();
        }

        public Planet Add(Planet entity)
        {
            return _context.SpatialEntities.Add(entity) as Planet;
        }

        public Planet Get(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(Planet value)
        {
            var entry = _context.Entry(value);
            entry.State = EntityState.Modified;
            if (value.Owner.ID != default(int))
            {
                entry.State = EntityState.Modified;
            }
            return true;
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Planet> All
        {
            get { return _context.SpatialEntities.OfType<Planet>(); }
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}
