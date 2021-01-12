using Microsoft.Xna.Framework;
using Nez;
using System;

namespace codex_online
{
    public class SideBarEntity : BoardAreaUi
    {
        public static float TotalWidth { get; } = 221;
        public static int NumberOfEntities { get; } = 30;
        public static int NumberOfColumns { get; } = 3;
        public static float CellWidth { get; } = TotalWidth / NumberOfColumns;
        public static float CellHeight { get; } = GameClient.ScreenHeight / (NumberOfEntities / NumberOfColumns);

        public TextComponent TopDisplay { get; set; }
        public TextComponent MiddleDisplay { get; set; }
        public TextComponent BottomDisplay { get; set; }
        public bool Owner { get; }

        public SideBarEntity(NezSpriteFont font, bool owner)
        {
            Owner = owner;
            int numberOfRows = NumberOfEntities / NumberOfColumns;
            float sideBarEntityHeight = GameClient.ScreenHeight / numberOfRows;

            TopDisplay = new TextComponent(font, String.Empty, new Vector2(0f, -sideBarEntityHeight / 2), Color.Black);
            MiddleDisplay = new TextComponent(font, String.Empty, Vector2.Zero, Color.Black);
            BottomDisplay = new TextComponent(font, String.Empty, new Vector2(0f, sideBarEntityHeight / 2), Color.Black);

            AddComponent(TopDisplay);
            AddComponent(MiddleDisplay);
            AddComponent(BottomDisplay);

            TopDisplay.HorizontalOrigin = HorizontalAlign.Center;
            MiddleDisplay.HorizontalOrigin = HorizontalAlign.Center;
            BottomDisplay.HorizontalOrigin = HorizontalAlign.Center;

            TopDisplay.VerticalOrigin = VerticalAlign.Top;
            MiddleDisplay.VerticalOrigin = VerticalAlign.Center;
            BottomDisplay.VerticalOrigin = VerticalAlign.Bottom;
        }
    }
}
