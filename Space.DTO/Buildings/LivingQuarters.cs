using Space.Repository.Entities;

namespace Space.DTO.Buildings
{
    public class LivingQuarters : IBuilding
    {
        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        #region Implementation of ISpatialEntity

        public double Latitude { get; set; }
        public double Longitude { get; set; }
       
        #endregion
    }
}
