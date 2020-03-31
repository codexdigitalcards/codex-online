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

        protected Text DisplayName { get; set; }
        protected Text DisplayNumber { get; set; }
        protected Text DisplayStatus { get; set; }

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
