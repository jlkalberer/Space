// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintExtensions.cs" company="COMPANY_PLACEHOLDER">
//   John Kalberer
// </copyright>
// <summary>
//   Console formatting for printing items
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Space.Console
{
    using System;
    using System.Linq;

    using Space.DTO;
    using Space.DTO.Players;
    using Space.DTO.Spatial;

    /// <summary>
    /// Console formatting for printing items
    /// </summary>
    public static class PrintExtensions
    {
        /// <summary>
        /// Print players stats.
        /// </summary>
        /// <param name="player">
        /// The player.
        /// </param>
        public static void Print(this Player player)
        {
            Console.WriteLine("\r\n*********");
            player.TotalNetValue.Print();
            Console.WriteLine("******");
            player.TickValue.Print();
            Console.WriteLine("*********\r\n");
        }

        /// <summary>
        /// Print a NetValue
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void Print(this NetValue value)
        {
            Console.WriteLine(
                string.Format(
                    "{0,-15}    {1,-15}    {2,-15}    {3,-15}    {4,-15}    {5,-15}    {6,-15}",
                    "Cash",
                    "Food",
                    "Iron",
                    "Octarine",
                    "Endurium",
                    "Networth",
                    "Attack"));
            Console.WriteLine(
                string.Format(
                    "{0,-15}    {1,-15}    {2,-15}    {3,-15}    {4,-15}    {5,-15}    {6,-15}",
                    value.Cash,
                    value.Food,
                    value.Iron,
                    0,
                    value.Energy,
                    value.Networth,
                    "100%"));
        }

        /// <summary>
        /// Print a TicketValue
        /// </summary>
        /// <param name="value">The value</param>
        public static void Print(this TickValue value)
        {
            Console.WriteLine(
                string.Format("{0,-25}    {1,-25}    {2,-30}    {3,-25}", "Cash", "Food", "Resources", "Science Points"));
            Console.WriteLine(
                string.Format(
                    "{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                    "Income",
                    value.ProducedCash,
                    "Food Produced",
                    value.ProducedFood,
                    "Iron Mined",
                    value.ProducedIron,
                    "Military",
                    0));
            Console.WriteLine(
                string.Format(
                    "{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                    "Units",
                    -value.Units,
                    "Food Consumed",
                    value.ProducedFood,
                    "Iron Decayed",
                    value.DecayedIron,
                    "Welfare",
                    0));
            Console.WriteLine(
                string.Format(
                    "{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                    "Buildings",
                    -value.Buildings,
                    "Food Decayed",
                    value.DecayedFood,
                    "Plutonium Mined",
                    value.ProducedEnergy,
                    "Economy",
                    0));
            Console.WriteLine(
                string.Format(
                    "{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                    "Bureaucracy",
                    -value.DecayedCash,
                    "Net Flow",
                    value.ProducedFood,
                    "Plutonium Decayed",
                    value.DecayedEnergy,
                    "Construction",
                    0));
            Console.WriteLine(
                string.Format(
                    "{0,-15}  {1,10}  {2,-15}  {3,10}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "Mana Mined",
                    0,
                    "Resources",
                    0));
        }

        /// <summary>
        /// Renders the galaxy to the console.
        /// </summary>
        /// <param name="galaxy">
        /// The galaxy.
        /// </param>
        public static void Print(this Galaxy galaxy)
        {
            for (var i = 0; i < galaxy.GalaxySettings.Width; i += 1)
            {
                for (var j = 0; j < galaxy.GalaxySettings.Height; j += 1)
                {
                    Console.Write(galaxy.SolarSystems.Any(s => (int)s.Latitude == i && (int)s.Longitude == j) ? "x " : "· ");
                }

                Console.WriteLine();
            }
        }
    }
}
