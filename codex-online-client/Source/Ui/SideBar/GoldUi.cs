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
    public class GoldUi : SideBarEntity
    {
        protected String GoldString { get; } = "Gold";

        public GoldUi(NezSpriteFont font) : base(font)
        {
            TopDisplay.text = GoldString;
            MiddleDisplay.text = 0.ToString();
        }

        private void UpdateGold(int gold)
        {
            MiddleDisplay.text = gold.ToString();
        }
    }
}
