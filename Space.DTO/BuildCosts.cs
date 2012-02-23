// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildCosts.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Keeps track of building costs for members.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    using Space.DTO.Entities;

    /// <summary>
    /// Keeps track of building costs for members.
    /// </summary>
    /// <typeparam name="TType">
    /// What type of object is this.
    /// </typeparam>
    public class BuildCosts<TType> : IDataObject<int>
    {
        #region Implementation of IDataObject<int>

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the type of building assosciated with the costs.
        /// </summary>
        public TType Type
        {
            get
            {
                return (TType)(object)this.BuildingTypeValue;
            }

            set
            {
                this.BuildingTypeValue = (int)(object)value;
            }
        }

        /// <summary>
        /// Gets or sets the BuildingType in the datastore.  This is here since EntityFramework does not support enums.
        /// </summary>
        public int BuildingTypeValue { get; set; }

        /// <summary>
        /// Gets or sets the building cost in Cash
        /// </summary>
        public double Cash { get; set; }

        /// <summary>
        /// Gets or sets the building cost in Food
        /// </summary>
        public double Food { get; set; }

        /// <summary>
        /// Gets or sets the building cost in Iron
        /// </summary>
        public double Iron { get; set; }

        /// <summary>
        /// Gets or sets the building cost in Energy
        /// </summary>
        public double Energy { get; set; }

        /// <summary>
        /// Gets or sets the building cost in Mana
        /// </summary>
        public double Mana { get; set; }

        /// <summary>
        /// Gets or sets the amount of time in ticks to build the item.
        /// </summary>
        public double Time { get; set; }
    }
}
