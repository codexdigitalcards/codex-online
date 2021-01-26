using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codex_online
{
    public class InPlayUi : CardRowUi
    {
        public static float SpaceBetweenCards = 5;
        public static float InPlayWidth { get; } = GameClient.ScreenWidth - SideBarEntity.TotalWidth;
        public static float InPlayHeight { get; } = CardUi.CardHeight * 2 + SpaceBetweenCards * 3;
        public static int MaxCardsBeforeOverlap { get; } = (int)(InPlayWidth / (CardUi.CardWidth + SpaceBetweenCards));

        public int CardRowPosition { get; }
        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public InPlayUi(int cardRowPosition)
        {
            Position = new Vector2(InPlayWidth / 2 + SideBarEntity.TotalWidth, GameClient.ScreenHeight / 2);
            AddComponent(new BoxCollider(InPlayWidth, InPlayHeight));
            CardRowPosition = cardRowPosition;
        }


        /// <summary>
        /// Stacks cards sequentially and fans them out
        /// </summary>
        protected override void OrganizeHand()
        {
            float targetScale = Cards.Count > MaxCardsBeforeOverlap ? 
                (InPlayWidth / Cards.Count) / (CardUi.CardWidth + SpaceBetweenCards) : 
                1;
            for (int i = 0; i < Cards.Count; i++)
            {
                CardUi cardEntity = Cards[i];
                CardMap[cardEntity] = new PositionScaleChange()
                {
                    PreviousSize = cardEntity.Size,
                    PreviousPosition = cardEntity.Position,
                    TargetSize = new Vector2(CardUi.CardWidth, CardUi.CardHeight) * targetScale
                };
                
                float starOfInPlay = Position.X - InPlayWidth / 2;
                CardMap[cardEntity].TargetPosition = new Vector2(
                        (CardUi.CardWidth + SpaceBetweenCards) * i * targetScale
                            + starOfInPlay
                            + (CardUi.CardWidth + SpaceBetweenCards) * targetScale / 2,
                        Position.Y 
                            + (CardUi.CardHeight + SpaceBetweenCards) / 2
                            + (2 - CardRowPosition) * (CardUi.CardHeight + SpaceBetweenCards)
                    );
            }
            
        }
    }
}
