using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Space.Repository.Entities;

namespace Space.DTO
{
    public class Race : IDataObject
    {
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
