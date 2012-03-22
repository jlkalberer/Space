// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Constant.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Stores the constants to be used in the game.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Repository.EF.Entities
{
    /// <summary>
    /// Stores the constants to be used in the game.
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// Gets or sets the id for the entry
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the value stored.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the friendly name for easily distinguishing between entries.
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
