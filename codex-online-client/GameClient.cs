using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;

namespace codex_online
{

    /// <summary>
    /// Game class responsible for intializing and switching between scenes
    /// </summary>
    public class GameClient : Core
    {
        public static float ScreenWidth { get; } = 1366;
        public static float ScreenHeight { get; } = 768;

        private static readonly string boardTextureName = "board";
        private static readonly string boardEntityName = "board";
        private static readonly string mouseColliderEntityName = "mouse-collider";
        private static readonly string handTextureName = "hand-holder";

        private bool localPlayerFirst;

        public GameMode GameMode { get; set; } = GameMode.OneHero;
        public TargetMethodSwitch TargetMethod { get; } = new TargetMethodSwitch();
        public GameConstants GameConstants { get; } = new GameConstants();
        public CardListWindow CardListWindow { get; private set; }
        public ClientState ClientState = new ClientState(ClientState.InGame);

        protected Scene InGameScene { get; set; }

        public GameClient() : base(width: Convert.ToInt32(ScreenWidth), height: Convert.ToInt32(ScreenHeight), isFullScreen: false, enableEntitySystems: false) { }

        //TODO: test
        public static Texture2D shadowBladeTexture;
        public static CardUi testCard;
        /// <summary>
        /// Initialize game
        /// </summary>
        protected override void Initialize()
        {
            //initialize scene
            base.Initialize();
            Window.AllowUserResizing = true;
            InGameScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);
            InGameScene.SetDesignResolution(Convert.ToInt32(ScreenWidth), Convert.ToInt32(ScreenHeight), Scene.SceneResolutionPolicy.ShowAll);
            
            //background
            Texture2D backgroundTexture = InGameScene.Content.Load<Texture2D>(boardTextureName);
            Entity background = InGameScene.CreateEntity(boardEntityName);
            background.AddComponent(new SpriteRenderer(backgroundTexture));
            background.Position = new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2);
            background.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.BoardRenderLayer;

            CardListWindow = new CardListWindow(ClientState);
            InGameScene.AddEntity(CardListWindow);

            Entity mouseCollider = InGameScene.CreateEntity(mouseColliderEntityName);
            mouseCollider.AddComponent(new MouseCollider(ClientState, CardListWindow));

            Hero[] myHeroes = new Hero[3];
            Hero[] opponentHeroes = new Hero[3];

            //TODO: remove test data
            myHeroes[0] = new Hero
            {
                Name = "Hero!",
                Cost = 2
            };
            opponentHeroes[0] = new Hero
            {
                Name = "Hero!",
                Cost = 2
            };
            Texture2D vandyTexture = InGameScene.Content.Load<Texture2D>("Cards/Black/black_hero_demonology_sidebar");
            shadowBladeTexture = InGameScene.Content.Load<Texture2D>("Cards/Black/black_starter_T0_03_thieving_imp");
            CardUi cardUi = new CardUi(myHeroes[0], vandyTexture);
            CardUi cardUi2 = new CardUi(opponentHeroes[0], vandyTexture);

            List<CardUi> openWindowCards = new List<CardUi>();
            NezSpriteFont font2 = new NezSpriteFont(InGameScene.Content.Load<SpriteFont>("Debug"));
            for (int x = 0; x < 5; x++)
            {
                TextComponent text = new TextComponent(font2, x.ToString() + x.ToString()+ x.ToString(), Vector2.Zero, Color.MediumVioletRed);

                CardUi cardUi3 = new CardUi(new Card() { Name = "imp" + x }, shadowBladeTexture);
                cardUi3.Enabled = false;
                cardUi3.AddComponent(text);
                text.RenderLayer = -50;
                InGameScene.AddEntity(cardUi3);
                openWindowCards.Add(cardUi3);
            }

            CardListWindow.OpenWindow(openWindowCards, true, 3, 4);

            //TODO:test
            testCard = new CardUi(new Card() { Name = "imp" }, shadowBladeTexture);
            InGameScene.AddEntity(testCard);

            InitializeGameUiComponents(myHeroes, opponentHeroes);

            Scene = InGameScene;
        }

        private void InitializeGameUiComponents(Hero[] myHeroes, Hero[] opponentHeroes)
        {
            NezSpriteFont font = new NezSpriteFont(InGameScene.Content.Load<SpriteFont>("Arial"));

            BoardAreaUi[] sideBarEntities = new BoardAreaUi[SideBarEntity.NumberOfEntities];

            int localPlayerWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountFirstPlayer : GameConstants.StartingWorkerCountSecondPlayer;
            int opponentWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountSecondPlayer : GameConstants.StartingWorkerCountFirstPlayer;

            for (int x = 0; x < sideBarEntities.Length; x += sideBarEntities.Length / 2)
            {
                HandUi handUi = new HandUi();
                Texture2D handTexture = InGameScene.Content.Load<Texture2D>(handTextureName);
                handUi.AddComponent(new SpriteRenderer(handTexture));
                handUi.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.BoardRenderLayer;
                InGameScene.AddEntity(handUi);

                InPlayUi inPlayUi = new InPlayUi();
                handUi.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.BoardRenderLayer;
                InGameScene.AddEntity(inPlayUi);

                bool isOpponent = x < 15;

                Hero[] currentHeroes = isOpponent ? opponentHeroes : myHeroes;
                sideBarEntities[0 + x] = new HeroButton(font, CardUi.CardToCardUiMap[currentHeroes[0]].GetComponent<SpriteRenderer>(), currentHeroes[0]);
                if (GameMode == GameMode.ThreeHero)
                {
                    sideBarEntities[1 + x] = new HeroButton(font, CardUi.CardToCardUiMap[currentHeroes[1]].GetComponent<SpriteRenderer>(), currentHeroes[1]);
                    sideBarEntities[2 + x] = new HeroButton(font, CardUi.CardToCardUiMap[currentHeroes[2]].GetComponent<SpriteRenderer>(), currentHeroes[2]);
                }

                sideBarEntities[3 + x] = new BaseButton(font, GameConstants.StartingBaseHealth);
                sideBarEntities[4 + x] = new AddOnButton(font);
                sideBarEntities[5 + x] = new SpecUi(font);

                sideBarEntities[6 + x] = new TechBuildingButton(font, TechLevel.One);
                sideBarEntities[7 + x] = new TechBuildingButton(font, TechLevel.Two);
                sideBarEntities[8 + x] = new TechBuildingButton(font, TechLevel.Three);

                sideBarEntities[9 + x] = new ZoneButton(font, Discard.DiscardString, 0);
                sideBarEntities[10 + x] = new ZoneButton(font, Deck.DeckString, GameConstants.StartingDeckSize);
                sideBarEntities[11 + x] = new ZoneButton(font, Hand.HandString, GameConstants.StartingHandSize);

                sideBarEntities[12 + x] = new GoldUi(font);

                int currentWorkerCount = isOpponent ? opponentWorkerCount : localPlayerWorkerCount; 
                sideBarEntities[13 + x] = new WorkerCountUi(font, currentWorkerCount);

                SideBarEntity CodexButton = new SideBarEntity(font);
                CodexButton.TopDisplay.Text = Codex.CodexString;
                sideBarEntities[14 + x] = CodexButton;
            }

            int numberOfColumns = SideBarEntity.NumberOfColumns;
            int numberOfRows = sideBarEntities.Length / numberOfColumns;
            float sideBarEntityHeight = ScreenHeight / numberOfRows;
            float sideBarEntityWidth = SideBarEntity.Width / numberOfColumns;
            
            for (int x = 0; x < sideBarEntities.Length; x += numberOfColumns)
            {
                int currentRow = x / numberOfColumns;
                float verticalPosition = currentRow * sideBarEntityHeight + sideBarEntityHeight / 2;
                for (int y = 0; y < numberOfColumns; y++)
                {
                    BoardAreaUi currentEntity = sideBarEntities[x + y];
                    if (currentEntity != null)
                    {
                        currentEntity.Position = new Vector2(
                            y * sideBarEntityWidth + sideBarEntityWidth / 2,
                            verticalPosition
                        );
                        InGameScene.AddEntity(currentEntity);
                    }
                }
            }
        }
    }
}
