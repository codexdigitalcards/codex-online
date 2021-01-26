using System.Collections.Generic;

namespace codex_online
{
    //TODO: Make sure Cards is not null
    /// <summary>
    /// Zone that represents a player's hand
    /// </summary>
    public class Hand : Zone
    {
        public Hand()
        {
            Name = Name.Hand;
            Cards = new HashSet<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }


        /// <summary>
        /// Add's a card to hand
        /// </summary>
        /// <param name="card">Card added to hand</param>
        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Remove's a card from hand
        /// </summary>
        /// <param name="card">Card removed from hand</param>
        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }
}
