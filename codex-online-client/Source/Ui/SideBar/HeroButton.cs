using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class HeroButton : SideBarEntity
    {
        private int cost;

        protected String CantPlay = "Can't Play";
        protected static CommandZone CommandZone { get; set; }

        public HeroButton(NezSpriteFont font, Sprite sprite, Hero hero) : base(font)
        {
            cost = hero.Cost;

            TopDisplay.text = hero.Name;
            MiddleDisplay.text = cost.ToString();

            addComponent(sprite);
        }

        public void HeroPlayed()
        {
            enabled = false;
        }

        public void HeroNotPlayable()
        {
            enabled = true;
            MiddleDisplay.text = String.Empty;
            BottomDisplay.text = CantPlay;
        }

        public void HeroPlayable()
        {
            MiddleDisplay.text = cost.ToString();
            BottomDisplay.text = String.Empty;
        }
    }
}
