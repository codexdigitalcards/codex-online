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
        protected Player Player { get; set; }

        public GoldUi(NezSpriteFont font, Player player)
        {
            Player = player;
            Gold = new Text(font, Player.Gold.ToString(), Vector2.Zero, Color.Black);
            Player.Updated += GoldUpdated;
            addComponent(Gold);
        }

        private void GoldUpdated(object sender, EventArgs e)
        {
            Gold.text = Player.Gold.ToString();
        }
    }
}
