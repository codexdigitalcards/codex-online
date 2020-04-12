using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class AddOn : BaseBuilding
    {
        public AddOnType Type { get; set; }
    }

    public class AddOnType
    {
        public static readonly AddOnType Surplus = new AddOnType("Surplus");
        public static readonly AddOnType Tower = new AddOnType("Tower");
        public static readonly AddOnType TechLab = new AddOnType("Tech Lab");
        public static readonly AddOnType HeroesHall = new AddOnType("Heroes Hall");

        private String Name { get; set; }

        private AddOnType(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
 