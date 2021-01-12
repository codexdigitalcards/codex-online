using System;
using System.Collections.Generic;

namespace codex_online
{
    public abstract class Card : GameObject
    {
        public Dictionary<Rune, int> Runes { get; } = new Dictionary<Rune, int>();

        public Name Name { get; set; }
        public int Cost { get; set; }
        public Zone Zone { get; set; }
        public bool Tapped { get; set; }
        public Player Controller { get; set; }
        public List<Ability> Abilities { get; } = new List<Ability>();
        public virtual bool Playble()
        {
            //TODO
            return true;
        }

        public virtual bool CanAttack()
        {
            return false;
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