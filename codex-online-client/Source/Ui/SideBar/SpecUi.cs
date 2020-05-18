using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;

namespace codex_online
{
    public class SpecUi : SideBarEntity
    {
        protected String SpecTitle { get; set; } = "Spec";
        protected String ExtraSpecPrefix { get; set; } = "Tech Lab: ";

        public SpecUi(NezSpriteFont font) : base(font)
        {
            TopDisplay.Text = SpecTitle;
        }

        public void UpdateSpec(Spec spec)
        {
            MiddleDisplay.Text = spec.Name;
        }

        public void UpdateExtraSpec(Spec spec)
        {
            BottomDisplay.Text = ExtraSpecPrefix + spec.Name;
        }
    }
}
