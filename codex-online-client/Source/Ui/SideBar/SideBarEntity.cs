using Microsoft.Xna.Framework;
using Nez;
using System;

namespace codex_online
{
    public class SideBarEntity : BoardAreaUi
    {
        public static float Width { get; } = 221;
        public static int NumberOfEntities { get; } = 30;
        public static int NumberOfColumns { get; } = 3;

        public TextComponent TopDisplay { get; set; }
        public TextComponent MiddleDisplay { get; set; }
        public TextComponent BottomDisplay { get; set; }

        public SideBarEntity(NezSpriteFont font)
        {
            TopDisplay = new TextComponent(font, String.Empty, Vector2.Zero, Color.Black);
            MiddleDisplay = new TextComponent(font, String.Empty, Vector2.Zero, Color.Black);
            BottomDisplay = new TextComponent(font, String.Empty, Vector2.Zero, Color.Black);

            AddComponent(TopDisplay);
            AddComponent(MiddleDisplay);
            AddComponent(BottomDisplay);

            TopDisplay.HorizontalOrigin = HorizontalAlign.Center;
            MiddleDisplay.HorizontalOrigin = HorizontalAlign.Center;
            BottomDisplay.HorizontalOrigin = HorizontalAlign.Center;

            TopDisplay.VerticalOrigin = VerticalAlign.Top;
            MiddleDisplay.VerticalOrigin = VerticalAlign.Center;
            BottomDisplay.VerticalOrigin = VerticalAlign.Bottom;

            int numberOfRows = NumberOfEntities / NumberOfColumns;
            float sideBarEntityHeight = GameClient.ScreenHeight / numberOfRows;

            TopDisplay.LocalOffset = new Vector2(0f, -sideBarEntityHeight / 2);
            BottomDisplay.LocalOffset = new Vector2(0f, sideBarEntityHeight / 2);
        }
    }
}
