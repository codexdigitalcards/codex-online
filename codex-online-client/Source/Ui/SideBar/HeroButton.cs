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

        public HeroButton(NezSpriteFont font, SpriteRenderer sprite, Hero hero) : base(font)
        {
            cost = hero.Cost;

            TopDisplay.Text = hero.Name;
            MiddleDisplay.Text = cost.ToString();

            AddComponent(sprite);
        }

        public void HeroPlayed()
        {
            Enabled = false;
        }

        public void HeroNotPlayable()
        {
            Enabled = true;
            MiddleDisplay.Text = String.Empty;
            BottomDisplay.Text = CantPlay;
        }

        public void HeroPlayable()
        {
            MiddleDisplay.Text = cost.ToString();
            BottomDisplay.Text = String.Empty;
        }
    }
}
