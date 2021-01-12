using System;

namespace codex_online
{
    public class TechnicianSlot : PatrolSlot
    {
        public TechnicianSlot() : base() 
        {
            Patrol = Patrol.Technician;
        }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}