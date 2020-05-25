using System;

namespace codex_online
{
    public class TechnicianSlot : PatrolSlot
    {
        public TechnicianSlot(Card card) : base(card) { }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}