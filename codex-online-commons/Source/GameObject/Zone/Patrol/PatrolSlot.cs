using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public abstract class PatrolSlot
    {
        public Patrol Patrol { get; protected set; }

        public Card Card { get; set; }

        public PatrolSlot()
        {
        }

        public abstract void ApplyPatrolBuff();
    }

    public enum Patrol
    {
        SquadLeader,
        Elite,
        Scavenger,
        Technician,
        Lookout
    }
}
