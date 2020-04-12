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
    public class GoldUi : BoardAreaUi
    {
        protected String GoldString { get; } = "Gold";
        protected Text Gold { get; set; }

        public GoldUi(NezSpriteFont font)
        {
            Gold = new Text(font, 0.ToString(), Vector2.Zero, Color.Black);
            addComponent(Gold);
        }

        private void UpdateGold(int gold)
        {
            Gold.text = gold.ToString();
        }
    }
}
