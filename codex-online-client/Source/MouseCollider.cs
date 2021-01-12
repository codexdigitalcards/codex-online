using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;

namespace codex_online
{
    
    /// <summary>
    /// Responsible for checking mouse collisions (clicks, hovers, etc) 
    /// and interpretting what they mean
    /// </summary>
    public class MouseCollider : BoxCollider, IUpdatable
    {
        private readonly ClientState clientState;
        private readonly CardListWindow cardListWindow;

        private bool dragging = false;
        private CardUi draggedCard = null;
        private Vector2 dragOffsetPosition = Vector2.Zero;
        private CardUi cardPressed = null;
        private CardUi topCard = null;
        private BoardAreaUi currentBoardArea = null;

        public MouseCollider(ClientState clientState, CardListWindow cardListWindow)
        {
            this.clientState = clientState;
            this.cardListWindow = cardListWindow;
        }

        public void Update()
        {
            if (!AnimationHandler.IsAnimationRunning())
            {
                Entity.Position = Input.MousePosition;
                if (clientState.State == ClientState.InGame)
                {
                    CalculateCollisions(CollidesWithLayers);
                    DragCard();
                    currentBoardArea?.BoardClicked();
                }

                else if(clientState.State == ClientState.CardListWindow)
                {
                    int physicsLayer = 0;
                    Flags.SetFlag(ref physicsLayer, Convert.ToInt32(PhysicsLayerFlag.WindowOpen));
                    if (Input.LeftMouseButtonPressed)
                    {
                        CalculateCollisions(physicsLayer);
                    }
                    else if(cardPressed != null && Input.LeftMouseButtonReleased)
                    {
                        CalculateCollisions(physicsLayer);
                        CardUi cardReleased = topCard;
                        if (cardReleased == cardPressed)
                        {
                            cardListWindow.SelectCard(cardPressed);
                        }
                        cardPressed = null;
                    }
                }

                topCard = null;
                currentBoardArea = null;
            }
        }

        //TODO: remove side effects; return a dictionary of entities instead of void
        private void CalculateCollisions(int physicsLayer)
        {
            float topLayerDepth = LayerConstant.LowestLayerDepth;
            IEnumerable<Collider> neighbors = Physics.BoxcastBroadphaseExcludingSelf(this, physicsLayer);

            foreach (var neighbor in neighbors)
            {
                CollisionResult collisionResult = new CollisionResult();
                if (neighbor.IsTrigger)
                {
                    continue;
                }

                if (CollidesWith(neighbor, out collisionResult))
                {
                    Entity collidedEntity = collisionResult.Collider.Entity;

                    switch (collidedEntity)
                    {
                        case CardUi cardEntity:
                            float currentLayerDepth = cardEntity.GetComponent<SpriteRenderer>().LayerDepth;

                            if (currentLayerDepth < topLayerDepth)
                            {
                                topCard = cardEntity;
                                topLayerDepth = currentLayerDepth;
                            }
                            break;
                        case BoardAreaUi boardAreaEntity:
                            currentBoardArea = boardAreaEntity;
                            break;
                    }
                }
            }
        }

        private void DragCard()
        {
            if (!dragging && Input.LeftMouseButtonPressed)
            {
                if (topCard != null && topCard.Draggable)
                {
                    draggedCard = topCard;
                    dragOffsetPosition = draggedCard.Position - Input.MousePosition;
                    draggedCard.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.PickedUpRenderLayer;
                    dragging = true;
                }
            }
            else if (dragging && Input.LeftMouseButtonDown)
            {
                draggedCard.Position = Input.MousePosition + dragOffsetPosition;
            }
            else if (dragging && !Input.LeftMouseButtonDown)
            {
                draggedCard.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.DefaultRenderLayer;
                //return if currentBoardArea is null
                currentBoardArea.CardDropped(draggedCard);
                dragging = false;
            }
        }
    }
}
