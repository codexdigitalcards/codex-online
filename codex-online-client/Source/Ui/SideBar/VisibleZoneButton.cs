using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class VisibleZoneButton : ZoneButton
    {
        private List<CardUi> Cards { get; } = new List<CardUi>();

        public VisibleZoneButton(NezSpriteFont font, Name name, int initialCount, bool owner) : base(font, name, initialCount, owner)
        {
            AddComponent(new BoxCollider(SideBarEntity.CellWidth, SideBarEntity.CellHeight));
        }

        public void AddCard(CardUi card)
        {
            Cards.Add(card);
            UpdateZone(Cards.Count);
        }
    }
}
