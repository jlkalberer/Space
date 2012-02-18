// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Race.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Defines the Race type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Players
{
    using Space.DTO.Entities;

    /// <summary>
    /// Used to create a race for players in the game.
    /// </summary>
    public class Race : IDataObject<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Race"/> class.
        /// </summary>
        public Race()
        {
            this.MagicBonus = 1;
            this.AttackBonus = 1;
            this.ScienceBonus = 1;
            this.IncomeBonus = 1;
            this.PopulationBonus = 1;
            this.SpeedBonus = 1;
        }

        #region Implementation of IDataObject

        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        public int ID { get; set; }

        #endregion
        
        /// <summary>
        /// Gets or sets the name of the race
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets MagicBonus.
        /// </summary>
        public float MagicBonus { get; set; }

        /// <summary>
        /// Gets or sets AttackBonus.
        /// </summary>
        public float AttackBonus { get; set; }

        /// <summary>
        /// Gets or sets ScienceBonus.
        /// </summary>
        public float ScienceBonus { get; set; }

        /// <summary>
        /// Gets or sets IncomeBonus.
        /// </summary>
        public float IncomeBonus { get; set; }

        /// <summary>
        /// Gets or sets PopulationBonus.
        /// </summary>
        public float PopulationBonus { get; set; }

        /// <summary>
        /// Gets or sets SpeedBonus.
        /// </summary>
        public float SpeedBonus { get; set; }
    }
}
