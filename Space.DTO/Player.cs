using System;
using System.Collections.Generic;
using Space.DTO.Spatial;
using Space.Repository.Entities;

namespace Space.DTO
{
    public class Player : IDataObject
    {
        public const float Decay = 0.995f;

        public Galaxy Galaxy { get; set; }

        /// <summary>
        /// The planets the player owns
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// The current fleets of the player
        /// </summary>
        public ICollection<Fleet> Fleets { get; set; }

        public ResearchPoints ResearchPoints { get; set; }

        private PlayerBonuses _bonuses;

        /// <summary>
        /// The calculated player bonuses this tick.
        /// </summary>
        public PlayerBonuses Bonuses
        {
            get
            {
                return _bonuses ?? (_bonuses = new PlayerBonuses
                                                   {
                                                       ConstructionBonus =
                                                           CalculateResearchValue(ResearchPoints.ConstructionPoints),
                                                       EconomyBonus =
                                                           CalculateResearchValue(ResearchPoints.EconomyPoints)*
                                                           Race.IncomeBonus,
                                                       MilitaryBonus =
                                                           CalculateResearchValue(ResearchPoints.MilitaryPoints)*
                                                           Race.AttackBonus,
                                                       ResearchBonus = Race.ScienceBonus,
                                                       ResourceBonus =
                                                           CalculateResearchValue(ResearchPoints.ResourcePoints),
                                                       WelfareBonus =
                                                           CalculateResearchValue(ResearchPoints.WelfarePoints)*
                                                           Race.PopulationBonus
                                                   });
            }
        }

        public double NetWorth { get; set; }

        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion

        public Race Race { get; set; }

        public NetValue TotalNetValue { get; set; }

        /// <summary>
        /// Normalized value for number of units
        /// </summary>
        public int UnitCount { get; set; }

        /// <summary>
        /// Updates the player's resources each tick - This uses the calculations generated from all planets.
        /// </summary>
        /// <param name="netValue"></param>
        public virtual void Update(NetValue netValue)
        {
            //TODO: these stats need to be shown to the player after each tick.  Store them in a table.

            // Decay existing values from last tick
            TotalNetValue.Cash *= Decay;
            TotalNetValue.Energy *= Decay;
            TotalNetValue.Food *= Decay;
            TotalNetValue.Iron *= Decay;

            // Add new values
            TotalNetValue.Add(netValue);

            TotalNetValue.BuildingCount = netValue.BuildingCount;
            TotalNetValue.Population = netValue.Population;

            // let them eat cake! -- but not so much cake that it goes below zero...
            TotalNetValue.Food -= TotalNetValue.Population/10.0 + UnitCount;
            var populationStarving = false;

            if(TotalNetValue.Food > 0)
            {
                TotalNetValue.Food = 0;
                TotalNetValue.Cash -= netValue.Cash/2;
            }

            // building maintainance & unit upkeep
            TotalNetValue.Cash = Math.Max(0, TotalNetValue.Cash - TotalNetValue.BuildingCount + UnitCount);
        }

        private double CalculateResearchValue(double researchPoints)
        {
            return 1+(1 - Math.Exp(-researchPoints/100*NetWorth));
        }
    }
}