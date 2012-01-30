namespace Space.Repository.Entities
{
    public interface IDataObject
    {
        /// <summary>
        /// Used as the primary key for the entity
        /// </summary>
        int ID { get; set; }
    }
}
