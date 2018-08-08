using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace codex_online
{
    public class Game1 : Core
    {
        SpriteBatch spriteBatch;
        Card card;
        Entity entityOne;


        public Game1() : base(width: 1000, height: 568, isFullScreen: false, enableEntitySystems: false)
        { }


        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;
            
            // create our Scene with the DefaultRenderer and a clear color of CornflowerBlue
            var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
