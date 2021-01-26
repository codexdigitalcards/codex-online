using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Discard : Zone
    {
        public Discard()
        {
            Name = Name.Discard;
            Cards = new List<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new List<Card>(Cards);
        }
    }
}
