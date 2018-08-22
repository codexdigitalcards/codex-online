using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    class MouseCollider : BoxCollider, IUpdatable
    {
        bool isDragging;
        Entity draggedEntity;
        Vector2 dragOffsetPosition;

        public void update()
        {
            entity.setPosition(Input.mousePosition);
            CollisionResult collisionResult;

            if (!isDragging && Input.leftMouseButtonPressed)
            {
                collidesWithAny(out collisionResult);
                var collider = collisionResult.collider;
                if (collider != null)
                {
                    draggedEntity = collider.entity;
                    dragOffsetPosition = draggedEntity.position - Input.mousePosition;
                    isDragging = true;
                }
            }
            else if (isDragging && Input.leftMouseButtonDown)
            {
                collidesWithAny(out collisionResult);
                draggedEntity.position = Input.mousePosition + dragOffsetPosition;
            }
            else if (isDragging && !Input.leftMouseButtonDown)
            {
                isDragging = false;
            }
        }
    }
}
