using Microsoft.Xna.Framework;
using System;

namespace codex_online
{
    class Patrol : Zone
    {
        public Patrol(Vector2 position, int height, int width) : base (position, height, width)
        {
            
        }

        public override void CardDisplayMode()
        {
            throw new NotImplementedException();
        }
    }
}