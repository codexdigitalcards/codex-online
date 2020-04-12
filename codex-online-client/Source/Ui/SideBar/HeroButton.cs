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
    public class HeroButton : SideBarButton
    {
        protected String CantPlay = "Can't Play";
        protected static CommandZone CommandZone { get; set; }

        public Hero Hero { get; set; }

        public HeroButton(NezSpriteFont font, Texture2D texture, Hero hero) : base(font)
        {
            Hero = hero;

            DisplayName.text = Hero.Name;
            DisplayNumber.text = Hero.Cost.ToString();

            Hero.Updated += HeroUpdated;

            addComponent(new Sprite(texture));
        }

        private void HeroUpdated(object sender, EventArgs e)
        {
            if (Hero.Zone != CommandZone)
            {
                enabled = false;
            }
            else
            {
                enabled = true;
                if (Hero.Died)
                {
                    DisplayNumber.text = String.Empty;
                    DisplayStatus.text = CantPlay;
                }
                else
                {
                    DisplayNumber.text = Hero.Cost.ToString();
                    DisplayStatus.text = String.Empty;
                }
            }
        }
    }
}
