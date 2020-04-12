using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class CommandZone : Zone
    {
        private static readonly string commandString = "Command Zone";

        public CommandZone()
        {
            Name = commandString;
            Cards = new List<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }
    }
}
