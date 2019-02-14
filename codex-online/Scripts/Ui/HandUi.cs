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
    public class HandUi : ZoneUi
    {
        private static readonly float LayerDepthIncriment = .0001f;

        public static int MaxHandSizeBeforeOverlap { get; } = 5;
        public static float HandWidth { get; } = CardUi.CardWidth * MaxHandSizeBeforeOverlap;
        public static float SecondsToMove { get; } = 3;

        protected List<CardUi> Cards { get; } = new List<CardUi>();
        protected Dictionary<CardUi, Vector2> CardSpeeds { get; } = new Dictionary<CardUi, Vector2>();
        protected float TimeMoving { get; set; } = 0;
        protected bool Animating { get; set; } = false; 
        protected Hand HandZone {
            get
            {
                return (Hand)Zone;
            }
            set
            {
                Zone = value;
            }
        }


        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public HandUi(Hand hand)
        {
            HandZone = hand;
            HandZone.CardsUpdated += HandUpdated;
            position = new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight - CardUi.CardHeight / 2);
        }


        /// <summary>
        /// Move cards to this hand
        /// </summary>
        /// <param name="cards">cards to add</param>
        public virtual void Add(List<CardUi> cards)
        {
            Cards.AddRange(cards);
            OrganizeHand();
        }

        /// <summary>
        /// Allow another zone to handle these cards
        /// </summary>
        /// <param name="cards">cards to remove</param>
        public virtual void Remove(List<CardUi> cards)
        {
            cards.ForEach(card => card.getComponent<Sprite>().layerDepth = 0);
            Cards.RemoveAll(card => cards.Contains(card));
            OrganizeHand();
        }


        /// <summary>
        /// When Hand zone is updated moves/removes cards away from hand
        /// to match the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void HandUpdated(object sender, EventArgs e)
        {
            ICollection<Card> currentHand = HandZone.GetCardsCopy();
            List<CardUi> cardsDifference = new List<CardUi>(
                Cards.Except(
                    currentHand
                        .Select(cardComponent => CardUi.CardToCardUiMap[cardComponent])
                )
            );

            if (Cards.Count > currentHand.Count)
            {
                Remove(cardsDifference);
            }
            else if (Cards.Count < currentHand.Count)
            {
                Add(cardsDifference);
            }
        }


        /// <summary>
        /// Stacks cards sequentially and fans them out
        /// </summary>
        protected virtual void OrganizeHand()
        {
            AnimationHandler.AddAnimation();
            TimeMoving = SecondsToMove;
            Animating = true;
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
                float distanceBetweenCards = HandWidth / Cards.Count;
                for (int i = 0; i < Cards.Count; i++)
                {
                    CardUi cardEntity = Cards[i];
                    cardEntity.getComponent<Sprite>().layerDepth = 1 - (i * LayerDepthIncriment);
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
            Vector2 destination = new Vector2(distanceBetweenCards * index + position.X - HandWidth / 2 + CardUi.CardWidth / 2, position.Y);
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
                        cardSpeedPair.Key.position += cardSpeedPair.Value * Time.deltaTime;
                    }
                    TimeMoving -= Time.deltaTime;
                }
                else
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        cardSpeedPair.Key.position += cardSpeedPair.Value * TimeMoving;
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
