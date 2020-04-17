using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class ZoneButton : SideBarEntity
    {
        public ZoneButton(NezSpriteFont font, string name, int count) : base(font)
        {
            TopDisplay.text = name;
            MiddleDisplay.text = count.ToString();
        }

        public void UpdateZone(int count)
        {
            MiddleDisplay.text = count.ToString();
        }
    }
}
