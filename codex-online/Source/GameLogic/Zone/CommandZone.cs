using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class CommandZone : Zone
    {
        public CommandZone(string name) : base(name)
        {
            Cards = new List<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }
    }
}
