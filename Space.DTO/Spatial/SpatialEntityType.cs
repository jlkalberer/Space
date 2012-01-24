using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.DTO.Spatial
{
    public enum SpatialEntityType
    {
        Planet = 0,
        GasGiant,
        // star types
        Nebula,
        Star,
        RedGiant,
        PlanetaryNebula,
        WhiteDwarf,
        BlackDwarf,
        // what can happen to a star
        NeutronStar,
        BlackHole, // <-- eats things - nom nom nom
        Supernova
    }
}
