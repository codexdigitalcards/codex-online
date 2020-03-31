using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class InPlay : Zone
    {
        public InPlay(string name) : base(name)
        {
            Cards = new HashSet<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }
}
