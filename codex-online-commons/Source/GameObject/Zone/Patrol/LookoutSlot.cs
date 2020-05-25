using System;

namespace codex_online
{
    public class LookoutSlot : PatrolSlot
    {
        public LookoutSlot(Card card) : base(card) { }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}