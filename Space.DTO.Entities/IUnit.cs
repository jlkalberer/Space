namespace Space.DTO.Entities
{
    public interface IUnit : IDataObject, ISpatialEntity
    {
        int FleetID { get; set; }
        void Update();
    }
}
