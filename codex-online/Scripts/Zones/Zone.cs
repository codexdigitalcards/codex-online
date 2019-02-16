using System;
using System.Collections.Generic;

namespace codex_online
{
    /// <summary>
    /// Represents a zone in the game
    /// A grouping of cards
    /// </summary>
    public abstract class Zone
    {
        protected ICollection<Card> Cards;
        public event EventHandler CardsUpdated;

        /// <summary>
        /// Does something when Cards is updated depending on the value of CardsUpdated
        /// </summary>
        protected virtual void OnCardsUpdated()
        {
            CardsUpdated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Creates a copy of Cards
        /// </summary>
        /// <returns>A copy of Cards</returns>
        public abstract ICollection<Card> GetCardsCopy();
    }
}