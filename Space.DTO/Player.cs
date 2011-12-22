using System;
using System.Collections.Generic;
using Space.Repository.Entities;

namespace Space.DTO
{
    public class Player : IDataObject
    {
        public const float Decay = 0.995f;

        /// <summary>
        /// The planets the player owns
        /// </summary>
        public ICollection<Planet> Planets { get; set; }

        /// <summary>
        /// The current fleets of the player
        /// </summary>
        public ICollection<Fleet> Fleets { get; set; }

        public ResearchPoints ResearchPoints { get; set; }

        public PlayerBonuses Bonuses
        {
            get
            {
                return new PlayerBonuses
                {
                    ConstructionBonus = CalculateResearchValue(ResearchPoints.ConstructionPoints),
                    EconomyBonus = CalculateResearchValue(ResearchPoints.EconomyPoints) * Race.IncomeBonus,
                    MilitaryBonus = CalculateResearchValue(ResearchPoints.MilitaryPoints) * Race.AttackBonus,
                    ResearchBonus = Race.ScienceBonus,
                    ResourceBonus = CalculateResearchValue(ResearchPoints.ResourcePoints),
                    WelfareBonus =
                        CalculateResearchValue(ResearchPoints.WelfarePoints) * Race.PopulationBonus
                };
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

        public void Update(NetValue netValue)
        {
            // Decay existing values from last tick
            TotalNetValue.Cash *= Decay;
            TotalNetValue.Energy *= Decay;
            TotalNetValue.Food *= Decay;
            TotalNetValue.Iron *= Decay;

            // Add new values
            TotalNetValue.Add(netValue);

            // let them eat cake!
            TotalNetValue.Food -= TotalNetValue.Population/10 + UnitCount;

            // building maintainance & unit upkeep
            TotalNetValue.Cash -= TotalNetValue.BuildingCount + UnitCount;
        }

        private float CalculateResearchValue(int researchPoints)
        {
            return 100*(1 - (float)Math.Exp(-(float)researchPoints/100*NetWorth));
        }
    }
}
