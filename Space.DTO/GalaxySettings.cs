// -----------------------------------------------------------------------
// <copyright file="GalaxySettings.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.DTO
{
    /// <summary>
    /// Game setup used to create a galaxy
    /// </summary>
    public class GalaxySettings
    {
        /// <summary>
        /// The ID of the Galaxy the settings belong to.
        /// </summary>
        public int GalaxyID { get; set; }

        /// <summary>
        /// The width of the solar system
        /// </summary>
        public int Width = 10;
        
        /// <summary>
        /// The height of the solar system.
        /// </summary>
        public int Height = 10;
        
        /// <summary>
        /// The probability of how many systems will be generated.
        /// </summary>
        public double SystemGenerationProbability = 0.94f;
        
        /// <summary>
        /// The maximum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMaximum = 100000.0f;
        
        /// <summary>
        /// The minimum speed a planet can move while orbiting a mass.
        /// </summary>
        public double OrbitSpeedMinimum = 30000.0f;

        /// <summary>
        /// This is used to scale how far apart the planets are relative to the solar system.
        /// </summary>
        public double SolarSystemScalar = 1;

        /// <summary>
        /// Used multiple times in calculations to get the difference between the orbit speed maximum and minimum.
        /// </summary>
        public double OrbitSpeedDifference { get { return OrbitSpeedMaximum - OrbitSpeedMinimum; } }
    }
}
