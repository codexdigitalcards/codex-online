using System;

namespace codex_online
{
    public class EliteSlot : PatrolSlot
    {
        public EliteSlot() : base() 
        {
            Patrol = Patrol.Elite;
        }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}