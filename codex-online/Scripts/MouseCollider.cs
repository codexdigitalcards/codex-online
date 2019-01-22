using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace codex_online
{
    
    /// <summary>
    /// Responsible for checking mouse collisions (clicks, hovers, etc) 
    /// and interpretting what they mean
    /// </summary>
    class MouseCollider : BoxCollider, IUpdatable
    {
        public static int PickedUpRenderLayer = -1;
        public static int DefaultRenderLayer = 0;

        private bool isDragging;
        private CardUi draggedCard;
        private Vector2 dragOffsetPosition;
        
        public void update()
        {
            entity.setPosition(Input.mousePosition);
            CollisionResult collisionResult;

            if (!isDragging && Input.leftMouseButtonPressed)
            {
                //TODO: collide with top card if there are multiple options
                collidesWithAny(out collisionResult);
                Collider collider = collisionResult.collider;
                if (collider != null && collider.entity is CardUi)
                {
                    draggedCard = (CardUi)collider.entity;
                    dragOffsetPosition = draggedCard.position - Input.mousePosition;
                    draggedCard.getComponent<Sprite>().renderLayer = PickedUpRenderLayer;
                    isDragging = true;
                }
            }
            else if (isDragging && Input.leftMouseButtonDown)
            {
                //TODO: check zone colliding with
                collidesWithAny(out collisionResult);
                draggedCard.position = Input.mousePosition + dragOffsetPosition;
            }
            else if (isDragging && !Input.leftMouseButtonDown)
            {
                draggedCard.getComponent<Sprite>().renderLayer = DefaultRenderLayer;
                isDragging = false;
            }
        }
    }
}
