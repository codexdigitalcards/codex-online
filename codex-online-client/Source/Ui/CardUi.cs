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
        public static float CardWidth { get; } = 119;
        public static float CardHeight { get; } = 112;
        public static Dictionary<Card, CardUi> CardToCardUiMap { get; } = new Dictionary<Card, CardUi>();

        protected Card card;


        /// <summary>
        /// Creates a card based on a Card and texture
        /// </summary>
        /// <param name="card"></param>
        /// <param name="texture"></param>
        public CardUi(Card card, Texture2D texture)
        {
            this.card = card;
            Name = card.Name;

            AddComponent(new SpriteRenderer(texture));
            AddComponent(new BoxCollider(CardWidth, CardHeight));

            float scale = CardHeight / texture.Height;
            LocalScale = new Vector2(scale, scale);

            CardToCardUiMap.Add(card, this);
        }
    }
}
