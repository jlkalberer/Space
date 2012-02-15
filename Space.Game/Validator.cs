using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.Game
{
    using Space.DTO.Buildings;
    using Space.DTO.Players;
    using Space.Repository;

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
