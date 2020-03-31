using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class ZoneButton : SideBarButton
    {
        protected Zone Zone { get; set; }

        public ZoneButton(NezSpriteFont font, Zone zone) : base(font)
        {
            Zone = zone;

            DisplayName.text = Zone.Name;
            DisplayNumber.text = Zone.Count().ToString();


            Zone.Updated += ZoneUpdated;
        }

        private void ZoneUpdated(object sender, EventArgs e)
        {
            DisplayNumber.text = Zone.Count().ToString();
        }
    }
}
