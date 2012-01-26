// -----------------------------------------------------------------------
// <copyright file="Constant.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.Repository.EF.Entities
{
    /// <summary>
    /// Stores the constants to be used in the game.
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// The id for the entry
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The value stored.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Friendly name for easily distinguishing between entries.
        /// </summary>
        public string FriendlyName { get; set; }
    }
}
