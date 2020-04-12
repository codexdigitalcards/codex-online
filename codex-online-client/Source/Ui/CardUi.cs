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
            addComponent(new BoxCollider(CardWidth, CardHeight));

            CardToCardUiMap.Add(card, this);
        }
    }
}
