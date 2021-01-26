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

        public override bool Buildable()
        {
            throw new NotImplementedException();
        }
    }

    public class AddOnType
    {
        private static int count = 0;

        public static Dictionary<int, AddOnType> AddOnTypeDictionary = new Dictionary<int, AddOnType>();
        public static AddOnType Surplus { get; } = new AddOnType(0, "Surplus");
        public static AddOnType Tower { get; } = new AddOnType(1, "Tower");
        public static AddOnType TechLab { get; } = new AddOnType(2, "Tech Lab");
        public static AddOnType HeroesHall { get; } = new AddOnType(3, "Heroes Hall");

        private String Name { get; set; }
        public int Id { get; }

        public static int Count
        {
            get
            {
                return count;
            }
        }

        private AddOnType(int id, string name)
        {
            Id = id;
            Name = name;
            count++;
            AddOnTypeDictionary.Add(id, this);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
 