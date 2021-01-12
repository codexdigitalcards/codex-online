using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class InPlay : Zone
    {
        public SquadLeaderSlot SquadLeader { get; } = new SquadLeaderSlot();
        public EliteSlot Elite { get; } = new EliteSlot();
        public ScavengerSlot Scavenger { get; } = new ScavengerSlot();
        public TechnicianSlot Technician { get; } = new TechnicianSlot();
        public LookoutSlot Lookout { get; } = new LookoutSlot();
        public List<PatrolSlot> PatrolZone;
        public InPlay()
        {
            PatrolZone = new List<PatrolSlot> { SquadLeader, Elite, Scavenger, Technician, Lookout };
            Name = Name.InPlay;
            Cards = new HashSet<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(Card card)
        {
            Cards.Remove(card);
        }
    }
}
