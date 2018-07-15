using Microsoft.Xna.Framework;
using Nez;

namespace codex_online
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
        public Game1() : base(width: 1280, height: 768, isFullScreen: false, enableEntitySystems: false)
        { }


        protected override void Initialize()
        {
            base.Initialize();
            Window.AllowUserResizing = true;

            // create our Scene with the DefaultRenderer and a clear color of CornflowerBlue
            var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            // set the scene so Nez can take over
            scene = myScene;
        }
    }
}
