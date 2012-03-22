// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpatialEntityType.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the SpatialEntityType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Spatial
{
    /// <summary>
    ///   Defines the SpatialEntityType type.
    /// </summary>
    public enum SpatialEntityType
    {
        /// <summary>
        /// The entity is a planet.
        /// </summary>
        Planet = 0,

        /// <summary>
        /// The entity is a gas giant.
        /// </summary>
        GasGiant,
        
        // star types

        /// <summary>
        /// The entity is a nebula.
        /// </summary>
        Nebula,

        /// <summary>
        /// The entity is a star.
        /// </summary>
        Star,

        /// <summary>
        /// The entity is a red giant.
        /// </summary>
        RedGiant,

        /// <summary>
        /// The entity is a planetary nebula.
        /// </summary>
        PlanetaryNebula,

        /// <summary>
        /// The entity is a white dwarf.
        /// </summary>
        WhiteDwarf,

        /// <summary>
        /// The entity is a black dwarf.
        /// </summary>
        BlackDwarf,
        
        // what can happen to a star

        /// <summary>
        /// The entity is a neutron star.
        /// </summary>
        NeutronStar,

        /// <summary>
        /// The entity is a black hole.
        /// </summary>
        BlackHole, // <-- eats things - nom nom nom

        /// <summary>
        /// The entity is a supernova.
        /// </summary>
        Supernova
    }
}
