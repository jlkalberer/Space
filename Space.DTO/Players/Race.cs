using Space.Repository.Entities;

namespace Space.DTO.Players
{
    public class Race : IDataObject
    {
        public Race()
        {
            MagicBonus = 1;
            AttackBonus = 1;
            ScienceBonus = 1;
            IncomeBonus = 1;
            PopulationBonus = 1;
            SpeedBonus = 1;
        }

        #region Implementation of IDataObject

        public int ID { get; set; }

        #endregion
        
        /// <summary>
        /// The name of the race
        /// </summary>
        public string Name { get; set; }

        public float MagicBonus { get; set; }
        public float AttackBonus { get; set; }
        public float ScienceBonus { get; set; }
        public float IncomeBonus { get; set; }
        public float PopulationBonus { get; set; }
        public float SpeedBonus { get; set; }
    }
}
