using System.Collections.Generic;

namespace codex_online
{
    /// <summary>
    /// Represents a zone in the game
    /// A grouping of cards
    /// </summary>
    public abstract class Zone : BoardArea
    {
        protected ICollection<Card> Cards;
        
        /// <summary>
        /// Creates a copy of Cards
        /// </summary>
        /// <returns>A copy of Cards</returns>
        public abstract ICollection<Card> GetCardsCopy();
    }
}