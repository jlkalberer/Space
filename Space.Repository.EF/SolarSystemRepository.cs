using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.DTO;
using Space.DTO.Spatial;

namespace Space.Repository.EF
{
    public class SolarSystemRepository : ISolarSystemRepository
    {
        private readonly EFDBContext _context;

        public SolarSystemRepository(EFDBContext context)
        {
            _context = context;
        }

        #region Implementation of ICrud<in int,ResearchPoints>

        public SolarSystem Create()
        {
            var solarSystem = _context.SolarSystems.Create();
            return _context.SolarSystems.Add(solarSystem);
        }

        public SolarSystem Get(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(SolarSystem value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SolarSystem> All
        {
            get { throw new NotImplementedException(); }
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}
