using System;

namespace codex_online
{
    public class LookoutSlot : PatrolSlot
    {
        public LookoutSlot() : base() 
        {
            Patrol = Patrol.Lookout;
        }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}