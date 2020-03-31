using System;

namespace codex_online
{
    public class Card : GameObject
    {
        public String Name { get; set; }
        public int Cost { get; set; }
        public Zone Zone { get; set; }

        public Card()
        {
        }

        public static bool StaysInPlay(Card card)
        {
            //TODO: ongoing spells
            if (card is Unit || card is Hero || card is BuildingCard || card is Upgrade)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}