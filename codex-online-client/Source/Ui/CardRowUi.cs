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
    public abstract class CardRowUi : BoardAreaUi
    {
        public static float SecondsToMove { get; } = 2;

        protected List<CardUi> Cards { get; } = new List<CardUi>();
        protected Dictionary<CardUi, PositionScaleChange> CardMap { get; } = new Dictionary<CardUi, PositionScaleChange>();
        protected float TimeMoving { get; set; } = 0;
        protected bool Animating { get; set; } = false;

        public void RemoveCards(CardUi removedCard)
        {
            removedCard.GetComponent<SpriteRenderer>().LayerDepth = LayerConstant.DefaultLayerDepth;
            Cards.Remove(removedCard);
            StartMovement();
        }

        public void AddCard(CardUi addedCard)
        {
            Cards.Add(addedCard);
            StartMovement();
        }

        private void StartMovement()
        {
            AnimationHandler.AddAnimation();
            TimeMoving = SecondsToMove;
            Animating = true;
            OrganizeHand();
        }

        protected abstract void OrganizeHand();

        public override void Update()
        {
            base.Update();

            if (Animating)
            {
                if (TimeMoving > Time.DeltaTime)
                {
                    foreach (KeyValuePair<CardUi, PositionScaleChange> positionScale in CardMap)
                    {
                        CardUi card = positionScale.Key;
                        card.Position += (positionScale.Value.TargetPosition - positionScale.Value.PreviousPosition) * Time.DeltaTime / SecondsToMove;
                        card.Size += (positionScale.Value.TargetSize - positionScale.Value.PreviousSize) * Time.DeltaTime / SecondsToMove;
                    }
                    TimeMoving -= Time.DeltaTime;
                }
                else
                {
                    foreach (KeyValuePair<CardUi, PositionScaleChange> positionScale in CardMap)
                    {
                        CardUi card = positionScale.Key;
                        card.Position = positionScale.Value.TargetPosition;
                        card.Size =  positionScale.Value.TargetSize;
                    }
                    TimeMoving = 0;
                    CardMap.Clear();
                    AnimationHandler.EndAnimation();
                    Animating = false;
                }
            }
        }

        protected class PositionScaleChange
        {
            public Vector2 PreviousPosition { get; set; }
            public Vector2 TargetPosition { get; set; }
            public Vector2 PreviousSize { get; set; }
            public Vector2 TargetSize { get; set; }
        }
    }
}
