// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlanetValue.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   The Gross values from a planet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.DTO
{
    /// <summary>
    /// The Gross values from a planet.
    /// </summary>
    public class PlanetValue : NetValue
    {
        /// <summary>
        /// Gets or sets the amount of cash generated from a population.
        /// </summary>
        public double PopulationCash { get; set; }

        /// <summary>
        /// Gets or sets the amount of cash generated from Tax Offices.
        /// </summary>
        public double TaxOfficeCash { get; set; }

        /// <summary>
        /// Gets or sets the amount of cash generated from Cash Factories.
        /// </summary>
        public double CashFactoryCash { get; set; }
    }
}
