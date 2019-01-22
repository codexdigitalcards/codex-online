using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;

namespace codex_online
{

    /// <summary>
    /// Graphical representation of a Card
    /// </summary>
    public class CardUi : Entity
    {
        public static float CardWidth { get; } = 90;
        public static float CardHeight { get; } = 126;
        public static Dictionary<Card, CardUi> CardToCardUiMap { get; } = new Dictionary<Card, CardUi>();

        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float TimeMoving { get; set; } = 0;

        protected Card card;


        /// <summary>
        /// Creates a card based on a Card and texture
        /// </summary>
        /// <param name="card"></param>
        /// <param name="texture"></param>
        public CardUi(Card card, Texture2D texture)
        {
            this.card = card;
            addComponent(new Sprite(texture));
            addComponent(new BoxCollider(texture.Width, texture.Height));

            CardToCardUiMap.Add(card, this);
        }

        public override void update()
        {
            base.update();

            if (TimeMoving != 0)
            {
                if (TimeMoving > Time.deltaTime)
                {
                    position += Velocity * Time.deltaTime;
                    TimeMoving -= Time.deltaTime;
                }
                else
                {
                    position += Velocity * TimeMoving;
                    TimeMoving = 0;
                }
            }
        }
    }
}
