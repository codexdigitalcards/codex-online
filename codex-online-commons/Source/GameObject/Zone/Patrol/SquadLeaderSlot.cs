using System;

namespace codex_online
{
    public class SquadLeaderSlot : PatrolSlot
    {
        public SquadLeaderSlot() : base() 
        {
            Patrol = Patrol.SquadLeader;
        }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}
