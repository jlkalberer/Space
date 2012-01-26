using System;
using System.Data;
using System.Linq;
using Space.DTO.Spatial;

namespace Space.Repository.EF
{
    public class EntityRepository : IEntityRepository
    {
        private readonly EFDBContext _context;

        public EntityRepository(EFDBContext context)
        {
            _context = context;
        }

        #region Implementation of ICrud<in int,Planet>

        public SpatialEntity Create()
        {
            return _context.SpatialEntities.Create<SpatialEntity>();
        }

        public SpatialEntity Add(SpatialEntity entity)
        {
            return _context.SpatialEntities.Add(entity);
        }

        public SpatialEntity Get(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(SpatialEntity value)
        {
            var entry = _context.Entry(value);
            entry.State = EntityState.Modified;
            return true;
        }

        public bool Delete(int key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SpatialEntity> All
        {
            get { return _context.SpatialEntities.OfType<SpatialEntity>(); }
        }

        public bool SaveChanges()
        {
            _context.SaveChanges();
            return true;
        }

        #endregion
    }
}
