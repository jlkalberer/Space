namespace Space.Repository.Entities
{
    public interface IUnit : IDataObject, ISpatialEntity
    {
        int FleetID { get; set; }
        void Update();
    }
}
