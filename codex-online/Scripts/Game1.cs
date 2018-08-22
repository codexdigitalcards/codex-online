using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace codex_online
{
    public class Game1 : Core
    {

        public Game1() : base(width: 1200, height: 650, isFullScreen: false, enableEntitySystems: false){}

        protected override void Initialize()
        {
            //initialize scene
            base.Initialize();
            Window.AllowUserResizing = true;
            var myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            //test card
            var cardTexture = myScene.content.Load<Texture2D>("jack-of-hearts");
            var card = myScene.createEntity("jackOfHearts");
            card.addComponent(new Sprite(cardTexture));
            card.addComponent(new BoxCollider(cardTexture.Width, cardTexture.Height));
            card.setPosition(new Vector2(500, 300));
            
            //mouse collider to check what mouse is touching
            var mouseCollider = myScene.createEntity("mouse-collider");
            mouseCollider.addComponent(new MouseCollider());

            scene = myScene;
        }
    }
}
