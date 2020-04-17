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
    public class SpecUi : SideBarEntity
    {
        protected String SpecTitle { get; set; } = "Spec";
        protected String ExtraSpecPrefix { get; set; } = "Tech Lab: ";

        public SpecUi(NezSpriteFont font) : base(font)
        {
            TopDisplay.text = SpecTitle;
        }

        public void UpdateSpec(Spec spec)
        {
            MiddleDisplay.text = spec.Name;
        }

        public void UpdateExtraSpec(Spec spec)
        {
            BottomDisplay.text = ExtraSpecPrefix + spec.Name;
        }
    }
}
