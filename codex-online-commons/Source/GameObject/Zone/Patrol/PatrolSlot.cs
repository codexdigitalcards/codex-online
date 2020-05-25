using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public abstract class PatrolSlot
    {
        public Card Card { get; set; }

        public PatrolSlot(Card card)
        {
            Card = card;
        }

        public abstract void ApplyPatrolBuff();
    }
}
