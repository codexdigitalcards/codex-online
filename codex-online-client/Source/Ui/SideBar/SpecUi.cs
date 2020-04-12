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
        protected String ExtraSpecPrefix { get; set; } = "Tech Lab: ";

        protected Text DisplayName { get; set; }
        protected Text Spec { get; set; }
        protected Text ExtraSpec { get; set; }

        public SpecUi(NezSpriteFont font)
        {
            DisplayName = new Text(font, SpecTitle, Vector2.Zero, Color.Black);
            Spec = new Text(font, string.Empty, Vector2.Zero, Color.Black);
            ExtraSpec = new Text(font, string.Empty, Vector2.Zero, Color.Black);

            addComponent(DisplayName);
            addComponent(Spec);
            addComponent(ExtraSpec);
        }

        public void UpdateSpec(Spec spec)
        {
            Spec.text = spec.Name;
        }

        public void UpdateExtraSpec(Spec spec)
        {
            ExtraSpec.text = ExtraSpecPrefix + spec.Name;
        }
    }
}
