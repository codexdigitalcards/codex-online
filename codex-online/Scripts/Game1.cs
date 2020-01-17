using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;
using System;
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

        //test variables TODO: remove
        KeyboardState previousKeys;
        static int numberOfCards = 20;
        Card[] cardArrayA = new Card[numberOfCards];
        Card[] cardArrayB = new Card[numberOfCards];
        Hand hand;
        InPlay inPlay;

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

            hand = new Hand();
            HandUi handUi = new HandUi(hand);
            Texture2D handTexture = inGameScene.content.Load<Texture2D>(handTextureName);
            handUi.addComponent(new Sprite(handTexture));
            handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
            inGameScene.addEntity(handUi);

            inPlay = new InPlay();
            InPlayUi inPlayUi = new InPlayUi(inPlay);
            handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
            inGameScene.addEntity(inPlayUi);

            //test cards
            List<CardUi> cards = new List<CardUi>();
            Texture2D cardTexture = inGameScene.content.Load<Texture2D>("Cards/Black/black_starter_T0_03_thieving_imp");
            for (int x = 1; x <= numberOfCards; x++)
            {
                Card card = new Card();
                cardArrayA[x - 1] = card;
                CardUi jackOfHearts = new CardUi(card, cardTexture);
                jackOfHearts.setPosition(50 * x, 50 * x);
                inGameScene.addEntity(jackOfHearts);
                hand.AddCard(card);
            }

            for (int x = 1; x <= numberOfCards; x++)
            {
                Card card = new Card();
                cardArrayB[x - 1] = card;
                CardUi jackOfHearts = new CardUi(card, cardTexture);
                jackOfHearts.setPosition(50 * x, 50 * x);
                inGameScene.addEntity(jackOfHearts);
                inPlay.AddCard(card);
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

            previousKeys = Keyboard.GetState();
            hand.OnBoardEventUpdated();
            inPlay.OnBoardEventUpdated();


            //remove from hand test
            //List<CardUi> cardsToRemove = new List<CardUi>(new CardUi[] { cards[0], cards[7] });
            //handUi.Remove(cardsToRemove);
            //cardsToRemove.ForEach(card => card.destroy());
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.K) && !previousKeys.IsKeyDown(Keys.K))
            {
                Console.WriteLine("pressed K");
                for (int x = 0; x < numberOfCards; x++)
                {
                    Card cardA = cardArrayA[x];
                    Card cardB = cardArrayB[x];

                    hand.RemoveCard(cardA);
                    inPlay.AddCard(cardA);

                    inPlay.RemoveCard(cardB);
                    hand.AddCard(cardB);
                }

                hand.OnBoardEventUpdated();
                inPlay.OnBoardEventUpdated();
            }
            
            previousKeys = state;
        }
    }
}
