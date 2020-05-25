using System;

namespace codex_online
{
    public class ScavengerSlot : PatrolSlot
    {
        public ScavengerSlot(Card card) : base(card) { }

        public override void ApplyPatrolBuff()
        {
            throw new NotImplementedException();
        }
    }
}