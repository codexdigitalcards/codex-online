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
        public ZoneButton(NezSpriteFont font, string name, int count) : base(font)
        {
            DisplayName.text = name;
            DisplayNumber.text = count.ToString();
        }

        public void UpdateZone(int count)
        {
            DisplayNumber.text = count.ToString();
        }
    }
}
