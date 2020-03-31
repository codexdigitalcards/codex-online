using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class TechBuilding : BaseBuilding
    {
        public static int TechOnePrice { get; set; } = 1;
        public static int TechTwoPrice { get; set; } = 4;
        public static int TechThreePrice { get; set; } = 5;

        public bool NeverBuilt { get; set; } = true;
        public TechLevel Level;
        public int Price { get; set; }
    }

    public enum TechLevel
    {
        One,
        Two,
        Three
    }
}
