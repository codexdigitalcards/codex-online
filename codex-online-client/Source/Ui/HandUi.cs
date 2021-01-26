using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codex_online
{

    /// <summary>
    /// UI representation of Hand. Controls movement of cards in the Hand zone
    /// </summary>
    public class HandUi : CardRowUi
    {
        public static int MaxHandSizeBeforeOverlap { get; } = 5;
        public static float HandWidth { get; } = CardUi.CardWidth * MaxHandSizeBeforeOverlap;
        

        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public HandUi()
        {
            Position = new Vector2(GameClient.ScreenWidth / 2, GameClient.ScreenHeight - CardUi.CardHeight / 2);
            AddComponent(new BoxCollider(HandWidth, CardUi.CardHeight));
        }

        /// <summary>
        /// Stacks cards sequentially and fans them out
        /// </summary>
        protected override void OrganizeHand()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                CardMap[Cards[i]] = new PositionScaleChange
                {
                    PreviousSize = Cards[i].Size,
                    TargetSize = new Vector2(CardUi.CardWidth, CardUi.CardHeight)
                };

                CardUi cardEntity = Cards[i];
                float distanceBetweenCards;

                if (Cards.Count <= MaxHandSizeBeforeOverlap)
                {
                    distanceBetweenCards = CardUi.CardWidth;
                }
                else
                {
                    distanceBetweenCards = (HandWidth - CardUi.CardWidth) / (Cards.Count - 1);
                    cardEntity.GetComponent<SpriteRenderer>().LayerDepth = 1 - (i * LayerConstant.LayerDepthIncriment);
                }

                float startOfHand = Position.X - HandWidth / 2 + CardUi.CardWidth / 2;
                CardMap[cardEntity].TargetPosition = new Vector2(distanceBetweenCards * i + startOfHand, Position.Y);
                CardMap[cardEntity].PreviousPosition = cardEntity.Position;
            }
        }
    }
}
