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
    class MouseCollider : BoxCollider, IUpdatable
    {
        public static int PickedUpRenderLayer { get; } = -1;
        public static int DefaultRenderLayer { get; } = 0;

        protected bool Dragging { get; set; } = false;
        protected CardUi DraggedCard { get; set; } = null;
        protected Vector2 DragOffsetPosition { get; set; } = Vector2.Zero;

        private readonly static float LowestLayerDepth = 2;

        public void update()
        {
            if (!AnimationHandler.IsAnimationRunning())
            {
                entity.setPosition(Input.mousePosition);
                CardUi topCard = null;
                BoardAreaUi clickedBoardArea = null;
                float topLayerDepth = LowestLayerDepth;
                IEnumerable<Collider> neighbors = Physics.boxcastBroadphaseExcludingSelf(this, collidesWithLayers);

                if (!Dragging && Input.leftMouseButtonPressed)
                {
                    foreach (var neighbor in neighbors)
                    {
                        CollisionResult collisionResult = new CollisionResult();
                        if (neighbor.isTrigger)
                        {
                            continue;
                        }

                        if (collidesWith(neighbor, out collisionResult))
                        {
                            Entity collidedEntity = collisionResult.collider.entity;

                            if (collidedEntity is CardUi)
                            {
                                float currentLayerDepth;
                                currentLayerDepth = collisionResult.collider.entity.getComponent<Sprite>().layerDepth;

                                if (currentLayerDepth < topLayerDepth)
                                {
                                    topCard = (CardUi)collidedEntity;
                                    topLayerDepth = currentLayerDepth;
                                }
                            }
                            else if (collidedEntity is BoardAreaUi)
                            {
                                clickedBoardArea = (BoardAreaUi)collidedEntity;
                            }
                        }
                    }

                    if (topCard != null)
                    {
                        DraggedCard = topCard;
                        DragOffsetPosition = DraggedCard.position - Input.mousePosition;
                        DraggedCard.getComponent<Sprite>().renderLayer = PickedUpRenderLayer;
                        Dragging = true;
                    }
                    else if (clickedBoardArea != null)
                    {
                        //TODO: remove
                        Console.WriteLine(clickedBoardArea.GetType());
                    }
                }
                else if (Dragging && Input.leftMouseButtonDown)
                {
                    //TODO: check zone colliding with
                    //collidesWithAny(out collisionResult);
                    DraggedCard.position = Input.mousePosition + DragOffsetPosition;
                }
                else if (Dragging && !Input.leftMouseButtonDown)
                {
                    DraggedCard.getComponent<Sprite>().renderLayer = DefaultRenderLayer;
                    Dragging = false;
                }
            }
        }
    }
}
