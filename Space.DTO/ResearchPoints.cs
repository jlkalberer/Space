using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space.DTO
{
    /// <summary>
    /// The total points allocated to each research area
    /// </summary>
    public class ResearchPoints
    {
        public int PlayerID { get; set; }

        public int MilitaryPoints { get; set; }
        public int WelfarePoints { get; set; }
        public int EconomyPoints { get; set; }
        public int ConstructionPoints { get; set; }
        public int ResourcePoints { get; set; }
    }
}
