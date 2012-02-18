// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataObject.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Used to give a DTO a common property for a primary key.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO.Entities
{
    /// <summary>
    /// Used to give a DTO a common property for a primary key.
    /// </summary>
    /// <typeparam name="TKey">
    /// The primary key type.
    /// </typeparam>
    public interface IDataObject<TKey>
    {
        /// <summary>
        /// Gets or sets the primary key for the entity
        /// </summary>
        TKey ID { get; set; }
    }
}
