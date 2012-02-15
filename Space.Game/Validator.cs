namespace Space.Game
{
    using DTO;
    using DTO.Buildings;
    using DTO.Players;

    /// <summary>
    /// Used to validate various game elements
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Constants for the solar system.
        /// </summary>
        private readonly SolarSystemConstants _solarSystemConstants;

        public Validator(SolarSystemConstants solarSystemConstants)
        {
            _solarSystemConstants = solarSystemConstants;
        }

        public bool CanPurchase(Player player, BuildingType buildingType, int count)
        {
            return false;
        }
    }
}
