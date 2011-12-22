using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Space.DTO;

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
            var planet = _context.Planets.Create();
            return _context.Planets.Add(planet);
        }

        public Planet Get(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(Planet value)
        {
            var entry = _context.Entry(value);
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
            get { return _context.Planets; }
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}
