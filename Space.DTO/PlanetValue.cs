// -----------------------------------------------------------------------
// <copyright file="PlanetValue.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Space.DTO
{
    /// <summary>
    /// The Gross values from a planet.
    /// </summary>
    public class PlanetValue : NetValue
    {
        /// <summary>
        /// The amount of cash generated from a population.
        /// </summary>
        public double PopulationCash { get; set; }

        /// <summary>
        /// The amount of cash generated from Tax Offices.
        /// </summary>
        public double TaxOfficeCash { get; set; }

        /// <summary>
        /// The amount of cash generated from Cash Factories.
        /// </summary>
        public double CashFactoryCash { get; set; }
    }
}
