using System;

namespace codex_online
{
    public class ScavengerSlot : PatrolSlot
    {
        public ScavengerSlot() : base() 
        {
            Patrol = Patrol.Scavenger;
        }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}