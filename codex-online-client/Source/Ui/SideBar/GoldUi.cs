using Nez;
using System;

namespace codex_online
{
    public class GoldUi : SideBarEntity
    {
        protected String GoldString { get; } = "Gold";

        public GoldUi(NezSpriteFont font) : base(font)
        {
            TopDisplay.Text = GoldString;
            MiddleDisplay.Text = 0.ToString();
        }

        private void UpdateGold(int gold)
        {
            MiddleDisplay.Text = gold.ToString();
        }
    }
}
