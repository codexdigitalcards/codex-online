using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class CodexButton : ZoneButton
    {
        public CodexButton(NezSpriteFont font, Codex codex) : base(font, codex)
        {
            DisplayNumber.enabled = false;
        }
    }
}
