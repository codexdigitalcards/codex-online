using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace codex_online
{
    public class Card
    {
        //Card Properties (Not Variables)
        protected Texture2D Texture { get; set; }
        protected Vector2 Position { get; set; }
        protected int Height { get; set; }
        protected int Width { get; set; }
        protected bool IsFaceUp { get; set; }
        protected bool IsTapped { get; set; }

        //Constructor Below
        public Card(Texture2D texture) //This is the specific information used to build a specific blueprint
        {
            Texture = texture;
        }
    }
}