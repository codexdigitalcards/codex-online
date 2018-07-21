using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Ruge.DragonDrop;
using MonoGame.Ruge.ViewportAdapters;
using Nez;
using Nez.Sprites;

namespace codex_online
{
    public class Game1 : Core
    {
        SpriteBatch spriteBatch;
        DragonDrop<Card> dragAndDrop;
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
            var texture = myScene.content.Load<Texture2D>("card-back");

            // setup our Scene by adding some Entities
            entityOne = myScene.createEntity("entity-one");
            entityOne.addComponent(new Sprite(texture));

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, Screen.width, Screen.height);
            dragAndDrop = new DragonDrop<Card>(this, viewportAdapter);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            card = new Card(texture, spriteBatch);
            dragAndDrop.Add(card);
            
            // set the scene so Nez can take over
            scene = myScene;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            dragAndDrop.Update(gameTime);
            entityOne.transform.position = card.Position;
        }
    }
}
