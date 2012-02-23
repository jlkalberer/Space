// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpatialEntityProbabilities.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The probabilities for all spatial entities
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using Space.DTO.Entities;
    using Space.DTO.Spatial;

    /// <summary>
    /// The probabilities for all spatial entities
    /// </summary>
    public class SpatialEntityProbabilities : IDataObject<int>
    {
        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the foreign key to the SolarSystemConstants.
        /// </summary>
        public int SolarSystemConstantsID { get; set; }

        /// <summary>
        /// Gets or sets the type of spatial entity assosciated with the probabilities.
        /// </summary>
        public SpatialEntityType Type 
        {
            get
            {
                return (SpatialEntityType)this.TypeValue;
            }

            set
            {
                this.TypeValue = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the value stored in the datastore.  Used since EntityFramework doesn't support enums.
        /// </summary>
        public int TypeValue { get; set; }

        /// <summary>
        /// Gets or sets the Spawning Probability of this type.
        /// </summary>
        public double SpawningProbability { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Radius of this type.
        /// </summary>
        public double MaximumRadius { get; set; }

        /// <summary>
        /// Gets or sets the Minimum Radius of this type.
        /// </summary>
        public double MinimumRadius { get; set; }

        /// <summary>
        /// Gets or sets the Maximum Mass of this type.
        /// </summary>
        public double MaximumMass { get; set; }
        
        /// <summary>
        /// Gets or sets the Minimum Mass of this type.
        /// </summary>
        public double MinimumMass { get; set; }
    }
}
