using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework.Graphics;

namespace codex_online
{
    public class BaseUi : BoardAreaUi
    {
        private Base gameBase;
        private Text displayedHealth;
                
        public BaseUi(SpriteFont font, Base gameBase)
        {
            this.gameBase = gameBase;
            displayedHealth = new Text(new NezSpriteFont(font), gameBase.Health.ToString(), Vector2.Zero, Color.Black);
            gameBase.Updated += BaseUpdated;
            //addComponent(new BoxCollider(texture.Width, texture.Height));

            addComponent(displayedHealth);
        }

        /// <summary>
        /// When Base zone is updated reflect changes in UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseUpdated(object sender, EventArgs e)
        {
            displayedHealth.text = gameBase.Health.ToString();
        }
    }
}