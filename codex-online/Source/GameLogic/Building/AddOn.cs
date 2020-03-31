using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class AddOn : BaseBuilding
    {
        public AddOnName Name { get; set; }
    }

    public class AddOnName
    {
        public static readonly AddOnName Surplus = new AddOnName("Surplus");
        public static readonly AddOnName Tower = new AddOnName("Tower");
        public static readonly AddOnName TechLab = new AddOnName("Tech Lab");
        public static readonly AddOnName HeroesHall = new AddOnName("Heroes Hall");

        private String Name { get; set; }

        private AddOnName(string name)
        {
            Name = name;
        }

        override
        public string ToString()
        {
            return Name;
        }
    }
}
 