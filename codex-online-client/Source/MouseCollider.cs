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
                    if (!dragging && Input.LeftMouseButtonPressed)
                    {
                        CardUi topCard = GetTopCard(CollidesWithLayers);

                        if (topCard != null)
                        {
                            draggedCard = topCard;
                            dragOffsetPosition = draggedCard.Position - Input.MousePosition;
                            draggedCard.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.PickedUpRenderLayer;
                            dragging = true;
                        }
                    }
                    else if (dragging && Input.LeftMouseButtonDown)
                    {
                        //TODO: check zone colliding with
                        //collidesWithAny(out collisionResult);
                        draggedCard.Position = Input.MousePosition + dragOffsetPosition;
                    }
                    else if (dragging && !Input.LeftMouseButtonDown)
                    {
                        draggedCard.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.DefaultRenderLayer;
                        dragging = false;
                    }
                }

                else if(clientState.State == ClientState.CardListWindow)
                {
                    int physicsLayer = 0;
                    Flags.SetFlag(ref physicsLayer, Convert.ToInt32(PhysicsLayerFlag.CardListWindow));
                    if (Input.LeftMouseButtonPressed)
                    {
                        cardPressed = GetTopCard(physicsLayer);
                    }
                    else if(cardPressed != null && Input.LeftMouseButtonReleased)
                    {
                        CardUi cardReleased = GetTopCard(physicsLayer);
                        if (cardReleased == cardPressed)
                        {
                            cardListWindow.SelectCard(cardPressed);
                        }
                        cardPressed = null;
                    }
                    else if(Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.G))
                    {
                        cardListWindow.OpenWindow(new List<CardUi>() { GameClient.testCard }, true);
                    }
                }
            }
        }

        private CardUi GetTopCard(int physicsLayer)
        {
            CardUi topCard = null;
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

                    if (collidedEntity is CardUi)
                    {
                        float currentLayerDepth = collidedEntity.GetComponent<SpriteRenderer>().LayerDepth;

                        if (currentLayerDepth < topLayerDepth)
                        {
                            topCard = (CardUi)collidedEntity;
                            topLayerDepth = currentLayerDepth;
                        }
                    }
                }
            }

            return topCard;
        }
    }
}
