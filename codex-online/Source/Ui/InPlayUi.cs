using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codex_online
{
    public class InPlayUi : BoardAreaUi
    {
        private static readonly float layerDepthIncriment = .0001f;

        public static float SpaceBetweenCards = 5;
        public static float InPlayWidth { get; } = Game1.ScreenWidth - SideBarButton.SideBarWidth;
        public static float InPlayHeight { get; } = CardUi.CardHeight * 2 + SpaceBetweenCards * 3;
        public static float SecondsToMove { get; } = 1;
        public static int MaxCardsBeforeOverlap { get; } = (int) (InPlayWidth / (CardUi.CardWidth + SpaceBetweenCards));

        protected List<CardUi> FrontRowCards { get; } = new List<CardUi>();
        protected List<CardUi> BackRowCards { get; } = new List<CardUi>();
        protected List<CardUi>[] CardRows;


        protected Dictionary<CardUi, Vector2> CardSpeeds { get; } = new Dictionary<CardUi, Vector2>();
        protected float[] TargetScale { get; set; } = new float[] { 1, 1 };
        protected Dictionary<CardUi, float>[] PreviousScale { get; set; } = new Dictionary<CardUi, float>[] 
        {
            new Dictionary<CardUi, float>(),
            new Dictionary<CardUi, float>()
        };
        protected float TimeMoving { get; set; } = 0;
        protected bool Animating { get; set; } = false;
        protected InPlay InPlayZone { get; set; }


        /// <summary>
        /// Creates a new HandUi from a Hand zone
        /// </summary>
        /// <param name="hand"></param>
        public InPlayUi(InPlay inPlay)
        {
            InPlayZone = inPlay;
            InPlayZone.Updated += InPlayUpdated;
            //TODO: move inplay to the right for space for left side buttons
            position = new Vector2(InPlayWidth / 2 + SideBarButton.SideBarWidth, Game1.ScreenHeight / 2);
            CardRows = new List<CardUi>[] { FrontRowCards, BackRowCards };
            addComponent(new BoxCollider(InPlayWidth, InPlayHeight));
        }

        /// <summary>
        /// When Hand zone is updated moves/removes cards away from hand
        /// to match the changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void InPlayUpdated(object sender, EventArgs e)
        {
            ICollection<Card> currentInPlayCards = InPlayZone.GetCardsCopy();

            List<CardUi> currentFrontRowCardUis = new List<CardUi>();
            List<CardUi> currentBackRowCardUis = new List<CardUi>();

            foreach (Card card in currentInPlayCards)
            {
                if (card is Unit)
                {
                    currentFrontRowCardUis.Add(CardUi.CardToCardUiMap[card]);
                }
                else
                {
                    currentBackRowCardUis.Add(CardUi.CardToCardUiMap[card]);
                }
            }

            IEnumerable<CardUi> removedFrontCards = new List<CardUi>(FrontRowCards.Except(currentFrontRowCardUis));
            foreach (CardUi card in removedFrontCards)
            {
                FrontRowCards.Remove(card);
            }

            IEnumerable<CardUi> addeddFrontCards = new List<CardUi>(currentFrontRowCardUis.Except(FrontRowCards));
            foreach (CardUi card in addeddFrontCards)
            {
                FrontRowCards.Add(card);
            }

            IEnumerable<CardUi> removedBackCards = new List<CardUi>(BackRowCards.Except(currentBackRowCardUis));
            foreach (CardUi card in removedBackCards)
            {
                BackRowCards.Remove(card);
            }

            IEnumerable<CardUi> addedBackCards = new List<CardUi>(currentBackRowCardUis.Except(BackRowCards));
            foreach (CardUi card in addedBackCards)
            {
                BackRowCards.Add(card);
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

            List<CardUi>[] CardRows = new List<CardUi>[] { FrontRowCards, BackRowCards };
            for (int i = 0; i < 2; i++)
            {
                if (CardRows[i].Count > MaxCardsBeforeOverlap)
                {
                    TargetScale[i] = (InPlayWidth / CardRows[i].Count) / (CardUi.CardWidth + SpaceBetweenCards);
                }
                for (int j = 0; j < CardRows[i].Count; j++)
                {
                    CardUi cardEntity = CardRows[i][j];
                    PreviousScale[i][cardEntity] = cardEntity.scale.X;
                    MoveToPositionInPlay(cardEntity, j, i);
                }
            }
        }

        /// <summary>
        /// Moves a card to a spot in hand based on the index and distanceBetweenCards
        /// </summary>
        /// <param name="cardEntity">Card to be moved</param>
        /// <param name="index">Position within hand to move to</param>
        /// <param name="distanceBetweenCards">how close each card in hand is</param>
        protected virtual void MoveToPositionInPlay(CardUi cardEntity, int index, int cardRowIndex)
        {
            float starOfInPlay = position.X - InPlayWidth / 2;
            Vector2 destination = new Vector2(
                    (CardUi.CardWidth + SpaceBetweenCards) * index * TargetScale[cardRowIndex]
                        + starOfInPlay
                        + (CardUi.CardWidth + SpaceBetweenCards) * TargetScale[cardRowIndex] / 2,
                    position.Y + (CardUi.CardHeight + SpaceBetweenCards) / 2 
                );
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

                    List<CardUi>[] CardRows = new List<CardUi>[] { FrontRowCards, BackRowCards };
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < CardRows[i].Count; j++)
                        {
                            CardUi card = CardRows[i][j];
                            card.setScale(card.scale.X + (TargetScale[i] - PreviousScale[i][card]) * (Time.deltaTime / SecondsToMove));
                        }
                    }
                    TimeMoving -= Time.deltaTime;
                }
                else
                {
                    foreach (KeyValuePair<CardUi, Vector2> cardSpeedPair in CardSpeeds)
                    {
                        CardUi card = cardSpeedPair.Key;
                        card.position += cardSpeedPair.Value * TimeMoving;
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < CardRows[i].Count; j++)
                        {
                            CardRows[i][j].setScale(TargetScale[i]);
                        }
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
