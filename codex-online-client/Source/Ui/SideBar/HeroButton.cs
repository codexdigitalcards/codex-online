using Microsoft.Xna.Framework;
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

        private bool outOfPlay = true;
        public CardUi Hero { get; }

        public HeroButton(NezSpriteFont font, SpriteRenderer sprite, CardUi hero, bool owner) : base(font, owner)
        {
            Hero = hero;
            cost = hero.Cost;

            TopDisplay.Text = hero.CardName;
            MiddleDisplay.Text = cost.ToString();
            
            AddComponent(sprite);
            float scale = TotalWidth / NumberOfColumns / sprite.Width;
            LocalScale = new Vector2(scale, scale);
            AddComponent(new BoxCollider(sprite.Width, sprite.Height));
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
            Hero.Enabled = false;
            Enabled = true;
            MiddleDisplay.Text = cost.ToString();
            BottomDisplay.Text = String.Empty;
        }
    }
}
