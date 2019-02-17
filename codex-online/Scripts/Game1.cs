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

        private static readonly string boardTextureName = "board";
        private static readonly string boardEntityName = "board";
        private static readonly string mouseColliderEntityName = "mouse-collider";
        private static readonly string handTextureName = "hand-holder";

        public Game1() : base(width: ScreenWidth, height: ScreenHeight, isFullScreen: false, enableEntitySystems: false){}
        
        /// <summary>
        /// Initialize game
        /// </summary>
        protected override void Initialize()
        {
            //initialize scene
            base.Initialize();
            Window.AllowUserResizing = true;
            Scene inGameScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);

            //fonts
            SpriteFont baseFont = inGameScene.content.Load<SpriteFont>("Arial");
            
            //background
            Texture2D backgroundTexture = inGameScene.content.Load<Texture2D>(boardTextureName);
            Entity background = inGameScene.createEntity(boardEntityName);
            background.addComponent(new Sprite(backgroundTexture));
            background.setPosition(new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2));
            background.getComponent<Sprite>().renderLayer = BoardRenderLayer;
            
            //Your base
            Base gameBase = new Base();
            BaseUi baseUi = new BaseUi(baseFont, gameBase);            
            baseUi.setPosition(new Vector2(300f, 300f));
            inGameScene.addEntity(baseUi);

            //mouse collider to check what mouse is touching
            Entity mouseCollider = inGameScene.createEntity(mouseColliderEntityName);
            mouseCollider.addComponent(new MouseCollider());

            HandUi handUi = new HandUi(new Hand());
            Texture2D handTexture = inGameScene.content.Load<Texture2D>(handTextureName);
            handUi.addComponent(new Sprite(handTexture));
            handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
            inGameScene.addEntity(handUi);
            
            //test cards
            List<CardUi> cards = new List<CardUi>();
            Texture2D cardTexture = inGameScene.content.Load<Texture2D>("Cards/Black/black_starter_T0_03_thieving_imp");
            for (int x = 1; x <= 8; x++)
            {
                CardUi jackOfHearts = new CardUi(new Card(), cardTexture);
                jackOfHearts.setScale(.5f);
                jackOfHearts.setPosition(50 * x, 50 * x);
                inGameScene.addEntity(jackOfHearts);

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

            scene = inGameScene;

            //add to hand test
            handUi.Add(cards);

            //remove from hand test
            //List<CardUi> cardsToRemove = new List<CardUi>(new CardUi[] { cards[0], cards[7] });
            //handUi.Remove(cardsToRemove);
            //cardsToRemove.ForEach(card => card.destroy());
        }
    }
}
