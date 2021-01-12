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
        public static Dictionary<ushort, CardUi> CardToCardUiMap { get; } = new Dictionary<ushort, CardUi>();
        public static Dictionary<Name, Texture2D> NameToTextureMap { get; } = new Dictionary<Name, Texture2D>();
        public ushort CardId { get; }
        public bool Draggable { get; set; }

        public Name CardName { get; set; }
        public bool Controlled { get; set; }
        public int? Attack { get; set; }
        public int? Health { get; set; }

        //in play only
        public bool? Tapped { get; set; }
        public Dictionary<Rune, int> Runes { get; } = new Dictionary<Rune, int>();
        public List<ushort> ActivatedAbilities { get; } = new List<ushort>();
        public List<ushort> Statuses { get; } = new List<ushort>();

        //out of play only
        public short Cost { get; set; }
        public bool Playable { get; set; }

        public CardUi(ushort cardId, Name name) : this(cardId, name, NameToTextureMap[name])
        {
        }

        /// <summary>
        /// Creates a card based on a Card and texture
        /// </summary>
        /// <param name="card"></param>
        /// <param name="texture"></param>
        public CardUi(ushort cardId, Name name, Texture2D texture)
        {
            this.CardId = cardId;
            CardName = name;
            Draggable = true;
            AddComponent(new SpriteRenderer(texture));
            AddComponent(new BoxCollider(CardWidth, CardHeight));

            float scale = CardHeight / texture.Height;
            LocalScale = new Vector2(scale, scale);

            CardToCardUiMap.Add(cardId, this);
        }
    }
}
