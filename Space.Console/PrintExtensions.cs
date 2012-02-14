// -----------------------------------------------------------------------
// <copyright file="PrintExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Space.DTO;

namespace Space.Console
{
    using System;

    /// <summary>
    /// Console formatting for printing items
    /// </summary>
    public static class PrintExtensions
    {
        public static void Print(this NetValue value)
        {
            Console.WriteLine(string.Format("{0,-15}    {1,-15}    {2,-15}    {3,-15}    {4,-15}    {5,-15}    {6,-15}",
                "Cash", "Food", "Iron", "Octarine", "Endurium", "Networth", "Attack"));
            Console.WriteLine(string.Format("{0,-15}    {1,-15}    {2,-15}    {3,-15}    {4,-15}    {5,-15}    {6,-15}",
                value.Cash, value.Food, value.Iron, 0, value.Energy, value.Networth, "100%"));
        }

        public static void Print(this TickValue value)
        {
            Console.WriteLine(string.Format("{0,-25}    {1,-25}    {2,-30}    {3,-25}", "Cash", "Food", "Resources",
                                            "Science Points"));
            Console.WriteLine(string.Format("{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                                            "Income", value.ProducedCash, "Food Produced", value.ProducedFood,
                                            "Iron Mined", value.ProducedIron, "Military", 0));
            Console.WriteLine(string.Format("{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                                            "Units", -value.Units, "Food Consumed", value.ProducedFood,
                                            "Iron Decayed", value.DecayedIron, "Welfare", 0));
            Console.WriteLine(string.Format("{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                                            "Buildings", -value.Buildings, "Food Decayed", value.DecayedFood,
                                            "Plutonium Mined", value.ProducedEnergy, "Economy", 0));
            Console.WriteLine(string.Format("{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                                            "Bureaucracy", -value.DecayedCash, "Net Flow", value.ProducedFood,
                                            "Plutonium Decayed", value.DecayedEnergy, "Construction", 0));
            Console.WriteLine(string.Format("{0,-15}  {1,10:G4}  {2,-15}  {3,10:G4}  {4,-20}  {5,10:G4}  {6,-15}  {7,10:G4}",
                                            "", "", "", "",
                                            "Octarine Mined", 0, "Resources", 0));
        }
    }
}
