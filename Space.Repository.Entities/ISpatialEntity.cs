namespace Space.Repository.Entities
{
    public interface ISpatialEntity
    {
        /// <summary>
        /// The latitude of the entity in space
        /// </summary>
        float Latitude { get; set; }

        /// <summary>
        /// The longitude of the entity in space
        /// </summary>
        float Longitude { get; set; }
    }
}
