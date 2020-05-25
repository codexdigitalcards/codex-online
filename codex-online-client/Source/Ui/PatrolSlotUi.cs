using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class PatrolSlotUi : BoardAreaUi
    {
        private TextComponent displayText;
        private Type patrolType;

        private CardUi card;

        public PatrolSlotUi(Type patrolType, String textString, NezSpriteFont font)
        {
            this.patrolType = patrolType;
            Name = patrolType.Name;
            displayText = new TextComponent(font, textString, new Vector2(0, CardUi.CardHeight / 2), Color.Black);
            displayText.SetHorizontalAlign(HorizontalAlign.Center);
            displayText.SetVerticalAlign(VerticalAlign.Bottom);

            AddComponent(displayText);
        }

        public void PatrolCard(CardUi card)
        {
            this.card = card;
            card.Enabled = true;
            card.Parent = Transform;
            card.LocalPosition = new Vector2(0, -displayText.Height);
            //card.GetComponent<SpriteRenderer>().RenderLayer
            card.GetComponent<SpriteRenderer>().LayerDepth = LayerConstant.LayerDepthIncriment;
        }
    }
}
