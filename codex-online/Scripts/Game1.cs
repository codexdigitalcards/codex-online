using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System.Collections.Generic;

namespace codex_online
{

    /// <summary>
    /// Game class responsible for intializing and switching between scenes
    /// </summary>
    public class Game1 : Core
    {
        public static int BoardRenderLayer { get; } = 1;
        public static int ScreenWidth { get; } = 1366;
        public static int ScreenHeight { get; } = 768;

        private static readonly string BoardTextureName = "board";
        private static readonly string BoardEntityName = "board";
        private static readonly string MouseColliderEntityName = "mouse-collider";
        private static readonly string HandTextureName = "hand-holder";

        public Game1() : base(width: ScreenWidth, height: ScreenHeight, isFullScreen: false, enableEntitySystems: false){}


        /// <summary>
        /// Initialize game
        /// </summary>
        protected override void Initialize()
        {
            //initialize scene
            base.Initialize();
            Window.AllowUserResizing = true;
            Scene myScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            //background
            Texture2D backgroundTexture = myScene.content.Load<Texture2D>(BoardTextureName);
            Entity background = myScene.createEntity(BoardEntityName);
            background.addComponent(new Sprite(backgroundTexture));
            background.setPosition(new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2));
            background.getComponent<Sprite>().renderLayer = BoardRenderLayer;

            //mouse collider to check what mouse is touching
            Entity mouseCollider = myScene.createEntity(MouseColliderEntityName);
            mouseCollider.addComponent(new MouseCollider());

            HandUi handUi = new HandUi(new Hand());
            Texture2D handTexture = myScene.content.Load<Texture2D>(HandTextureName);
            handUi.addComponent(new Sprite(handTexture));
            handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
            myScene.addEntity(handUi);
            
            //test cards
            List<CardUi> cards = new List<CardUi>();
            Texture2D cardTexture = myScene.content.Load<Texture2D>("jack-of-hearts");
            for (int x = 1; x <= 8; x++)
            {
                CardUi jackOfHearts = new CardUi(new Card(), cardTexture);
                jackOfHearts.setScale(.5f);
                jackOfHearts.setPosition(50 * x, 50 * x);
                myScene.addEntity(jackOfHearts);

                cards.Add(jackOfHearts);
            }

            //test to see if cards stack correctly
            System.Random rng = new System.Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }

            scene = myScene;

            //add to hand test
            handUi.Add(cards);

            //remove from hand test
            List<CardUi> cardsToRemove = new List<CardUi>(new CardUi[] { cards[0], cards[7] });
            handUi.Remove(cardsToRemove);
            cardsToRemove.ForEach(card => card.destroy());
        }
    }
}
