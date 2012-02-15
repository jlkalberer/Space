namespace Space.DTO.Entities
{
    public interface ISpatialEntity
    {
        /// <summary>
        /// The latitude of the entity in space
        /// </summary>
        double Latitude { get; set; }

        /// <summary>
        /// The longitude of the entity in space
        /// </summary>
        double Longitude { get; set; }
    }
}
