using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace codex_online
{
    abstract class Zone
    {
        //Zone Properties
        protected Vector2 Position { get; set; }
        protected int Height { get; set; }
        protected int Width { get; set; }
        public List<Card> cards;
        
        public Zone(Vector2 position, int height, int width)
        {
            
        }

        public abstract void CardDisplayMode();
    }
}