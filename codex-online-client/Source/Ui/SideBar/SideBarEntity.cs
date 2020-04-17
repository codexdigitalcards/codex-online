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
    public class SideBarEntity : BoardAreaUi
    {
        public static float Width { get; } = 221;
        public static int NumberOfEntities { get; } = 30;
        public static int NumberOfColumns { get; } = 3;

        public Text TopDisplay { get; set; }
        public Text MiddleDisplay { get; set; }
        public Text BottomDisplay { get; set; }

        public SideBarEntity(NezSpriteFont font)
        {
            TopDisplay = new Text(font, String.Empty, Vector2.Zero, Color.Black);
            MiddleDisplay = new Text(font, String.Empty, Vector2.Zero, Color.Black);
            BottomDisplay = new Text(font, String.Empty, Vector2.Zero, Color.Black);

            addComponent(TopDisplay);
            addComponent(MiddleDisplay);
            addComponent(BottomDisplay);

            TopDisplay.horizontalOrigin = HorizontalAlign.Center;
            MiddleDisplay.horizontalOrigin = HorizontalAlign.Center;
            BottomDisplay.horizontalOrigin = HorizontalAlign.Center;

            TopDisplay.verticalOrigin = VerticalAlign.Top;
            MiddleDisplay.verticalOrigin = VerticalAlign.Center;
            BottomDisplay.verticalOrigin = VerticalAlign.Bottom;

            int numberOfRows = NumberOfEntities / NumberOfColumns;
            float sideBarEntityHeight = GameClient.ScreenHeight / numberOfRows;

            TopDisplay.localOffset = new Vector2(0f, -sideBarEntityHeight / 2);
            BottomDisplay.localOffset = new Vector2(0f, sideBarEntityHeight / 2);
        }
    }
}
