using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Deck : Zone
    {
        public static string DeckString { get; } = "Deck";

        public Deck()
        {
            Name = DeckString;
            Cards = new List<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new List<Card>(Cards);
        }
    }
}
