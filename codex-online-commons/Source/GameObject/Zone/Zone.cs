using System;
using System.Collections.Generic;

namespace codex_online
{
    /// <summary>
    /// Represents a zone in the game
    /// A grouping of cards
    /// </summary>
    public abstract class Zone : GameObject
    {
        protected ICollection<Card> Cards;

        public String Name { get; protected set; }

        /// <summary>
        /// Creates a copy of Cards
        /// </summary>
        /// <returns>A copy of Cards</returns>
        public abstract ICollection<Card> GetCardsCopy();

        public int Count()
        {
            return Cards.Count;
        }
    }
}