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
    public class SpecUi : BoardAreaUi
    {
        protected String SpecTitle { get; set; } = "Spec";

        protected Text DisplayName { get; set; }
        protected Text Spec { get; set; }
        protected Text ExtraSpec { get; set; }
        protected Player Player { get; set; }

        public SpecUi(NezSpriteFont font, Player player)
        {
            Player = player;

            DisplayName = new Text(font, SpecTitle, Vector2.Zero, Color.Black);
            Spec = new Text(font, SpecString(), Vector2.Zero, Color.Black);
            ExtraSpec = new Text(font, ExtraSpecString(), Vector2.Zero, Color.Black);

            addComponent(DisplayName);
            addComponent(Spec);
            addComponent(ExtraSpec);

            Player.Updated += SpecUpdated;
        }

        private void SpecUpdated(object sender, EventArgs e)
        {
            Spec.text = SpecString();
            ExtraSpec.text = ExtraSpecString();
        }

        protected String SpecString()
        {
            return Player.Spec?.Name ?? String.Empty;
        }

        protected String ExtraSpecString()
        {
            if (Player.AddOn?.Name == AddOnName.TechLab)
            {
                return Player.ExtraSpec.Name;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
