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
    public class HandUi : BoardAreaUi
    {
        private static readonly float layerDepthIncriment = .0001f;

        public static int MaxHandSizeBeforeOverlap { get; } = 5;
        public static float HandWidth { get; } = CardUi.CardWidth * MaxHandSizeBeforeOverlap;
        public static float SecondsToMove { get; } = 1;

        protected List<CardUi> Cards { get; } = new List<CardUi>();
        protected Dictionary<CardUi, Vector2> CardSpeeds { get; } = new Dictionary<CardUi, Vector2>();
        protected Dictionary<CardUi, float> PreviousScale { get; } = new Dictionary<CardUi, float>();
        protected float TimeMoving { get; set; } = 0;
        protected bool Animating { get; set; } = false;
        
        protected Hand HandZone { get; set; }


        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public HandUi(Hand hand)
        {
            HandZone = hand;
            HandZone.Updated += HandUpdated;
            position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight - CardUi.CardHeight / 2);
            addComponent(new BoxCollider(HandWidth, CardUi.CardHeight));
        }

        /// <summary>
        /// When Hand zone is updated moves/removes cards away from hand
        /// to match the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandUpdated(object sender, EventArgs e)
        {
            ICollection<Card> currentCards = HandZone.GetCardsCopy();
            IEnumerable<CardUi> currentCardUis = currentCards.Select(cardComponent => CardUi.CardToCardUiMap[cardComponent]);

            List<CardUi> removedCards = new List<CardUi>(Cards.Except(currentCardUis));
            foreach (CardUi card in removedCards)
            {
                card.getComponent<Sprite>().layerDepth = 0;
                Cards.Remove(card);
            }

            IEnumerable<CardUi> addedCards = new List<CardUi>(currentCardUis.Except(Cards));
            foreach (CardUi card in addedCards)
            {
                Cards.Add(card);
            }

            OrganizeHand();
        }


        /// <summary>
        /// Stacks cards sequentially and fans them out
        /// </summary>
        protected virtual void OrganizeHand()
        {
            AnimationHandler.AddAnimation();
            TimeMoving = SecondsToMove;
            Animating = true;

            foreach (CardUi card in Cards)
            {
                PreviousScale[card] = card.scale.X;
            }
            if (Cards.Count <= MaxHandSizeBeforeOverlap)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    CardUi cardEntity = Cards[i];
                    MoveToPositionInHand(cardEntity, i, CardUi.CardWidth);
                }
            }
            else
            {
                float distanceBetweenCards = (HandWidth - CardUi.CardWidth) / (Cards.Count - 1);
                for (int i = 0; i < Cards.Count; i++)
                {
                    CardUi cardEntity = Cards[i];
                    cardEntity.getComponent<Sprite>().layerDepth = 1 - (i * layerDepthIncriment);
                    MoveToPositionInHand(cardEntity, i, distanceBetweenCards);
                }
            }
        }


        /// <summary>
        /// Moves a card to a spot in hand based on the index and distanceBetweenCards
        /// </summary>
        /// <param name="cardEntity">Card to be moved</param>
        /// <param name="index">Position within hand to move to</param>
        /// <param name="distanceBetweenCards">how close each card in hand is</param>
        protected virtual void MoveToPositionInHand(CardUi cardEntity, int index, float distanceBetweenCards)
        {
            float startOfHand = position.X - HandWidth / 2 + CardUi.CardWidth / 2;
            Vector2 destination = new Vector2(distanceBetweenCards * index + startOfHand, position.Y);
            CardSpeeds[cardEntity] = (destination - cardEntity.position) / SecondsToMove;
        }

        public override void update()
        {
            base.update();

            if (Animating)
            {
                if (TimeMoving > Time.deltaTime)
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        CardUi card = cardSpeedPair.Key;
                        card.position += cardSpeedPair.Value * Time.deltaTime;
                        card.setScale(card.scale.X + (1 - PreviousScale[card]) * (Time.deltaTime / SecondsToMove));
                    }
                    TimeMoving -= Time.deltaTime;
                }
                else
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        CardUi card = cardSpeedPair.Key;
                        card.position += cardSpeedPair.Value * TimeMoving;
                        card.setScale(1);
                    }
                    TimeMoving = 0;
                    CardSpeeds.Clear();
                    AnimationHandler.EndAnimation();
                    Animating = false;
                }
            }
        }
    }
}
