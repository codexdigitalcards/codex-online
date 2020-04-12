using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class SideBarButton : BoardAreaUi
    {
        public static float SideBarWidth { get; } = 221;

        public Text DisplayName { get; set; }
        public Text DisplayNumber { get; set; }
        public Text DisplayStatus { get; set; }

        public SideBarButton(NezSpriteFont font)
        {
            DisplayName = new Text(font, String.Empty, Vector2.Zero, Color.Black);
            DisplayNumber = new Text(font, String.Empty, Vector2.Zero, Color.Black);
            DisplayStatus = new Text(font, String.Empty, Vector2.Zero, Color.Black);

            addComponent(DisplayName);
            addComponent(DisplayNumber);
            addComponent(DisplayStatus);
        }
    }
}
