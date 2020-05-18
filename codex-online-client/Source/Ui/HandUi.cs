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
        public static int MaxHandSizeBeforeOverlap { get; } = 5;
        public static float HandWidth { get; } = CardUi.CardWidth * MaxHandSizeBeforeOverlap;
        public static float SecondsToMove { get; } = 1;

        protected List<CardUi> Cards { get; } = new List<CardUi>();
        protected Dictionary<CardUi, Vector2> CardSpeeds { get; } = new Dictionary<CardUi, Vector2>();
        protected Dictionary<CardUi, float> PreviousScale { get; } = new Dictionary<CardUi, float>();
        protected float TimeMoving { get; set; } = 0;
        protected bool Animating { get; set; } = false;

        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public HandUi()
        {
            Position = new Vector2(GameClient.ScreenWidth / 2, GameClient.ScreenHeight - CardUi.CardHeight / 2);
            AddComponent(new BoxCollider(HandWidth, CardUi.CardHeight));
        }

        private void RemoveCards(List<CardUi> removedCardUis)
        {
            foreach (CardUi cardUi in removedCardUis)
            {
                cardUi.GetComponent<SpriteRenderer>().LayerDepth = LayerConstant.DefaultLayerDepth;
                Cards.Remove(cardUi);
            }
            OrganizeHand();
        }

        private void AddCards(List<CardUi> addedCardUis)
        {
            foreach (CardUi cardUi in addedCardUis)
            {
                Cards.Add(cardUi);
            }
            OrganizeHand();
        }

        /// <summary>
        /// Stacks cards sequentially and fans them out
        /// </summary>
        protected void OrganizeHand()
        {
            AnimationHandler.AddAnimation();
            TimeMoving = SecondsToMove;
            Animating = true;

            foreach (CardUi card in Cards)
            {
                PreviousScale[card] = card.Scale.X;
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
                    cardEntity.GetComponent<SpriteRenderer>().LayerDepth = 1 - (i * LayerConstant.LayerDepthIncriment);
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
        protected void MoveToPositionInHand(CardUi cardEntity, int index, float distanceBetweenCards)
        {
            float startOfHand = Position.X - HandWidth / 2 + CardUi.CardWidth / 2;
            Vector2 destination = new Vector2(distanceBetweenCards * index + startOfHand, Position.Y);
            CardSpeeds[cardEntity] = (destination - cardEntity.Position) / SecondsToMove;
        }

        public override void Update()
        {
            base.Update();

            if (Animating)
            {
                if (TimeMoving > Time.DeltaTime)
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        CardUi card = cardSpeedPair.Key;
                        card.Position += cardSpeedPair.Value * Time.DeltaTime;
                        card.SetScale(card.Scale.X + (1 - PreviousScale[card]) * (Time.DeltaTime / SecondsToMove));
                    }
                    TimeMoving -= Time.DeltaTime;
                }
                else
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        CardUi card = cardSpeedPair.Key;
                        card.Position += cardSpeedPair.Value * TimeMoving;
                        card.SetScale(1);
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
