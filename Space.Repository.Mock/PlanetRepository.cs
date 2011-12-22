using System;
using System.Collections.Generic;
using System.Linq;
using Space.DTO;

namespace Space.Repository.Mock
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly List<Planet> _planets;

        public PlanetRepository()
        {
            _planets = new List<Planet>();
        }
        #region Implementation of ICrud<in int,Planet>

        public bool Create(Planet value)
        {
            _planets.Add(value);
            return true;
        }

        public Planet Get(int key)
        {
            return _planets.FirstOrDefault(p => p.ID == key);
        }

        public bool Delete(int key)
        {
            _planets.RemoveAll(p => p.ID == key);
            return true;
        }

        public IQueryable<Planet> All
        {
            get { return _planets.AsQueryable(); }
        }

        public bool Update(Planet value)
        {
            var tplanet = Get(value.ID);
            tplanet = value;
            return true;
        }

        #endregion
    }
}
