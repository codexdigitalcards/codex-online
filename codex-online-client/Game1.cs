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

        private bool localPlayerFirst;

        public GameMode GameMode { get; set; } = GameMode.OneHero;
        public TargetMethodSwitch TargetMethod { get; } = new TargetMethodSwitch();
        public GameConstants GameConstants { get; } = new GameConstants();

        protected Scene InGameScene { get; set; }

        public Game1() : base(width: ScreenWidth - 100, height: ScreenHeight - 50, isFullScreen: false, enableEntitySystems: false) { }

        /// <summary>
        /// Initialize game
        /// </summary>
        protected override void Initialize()
        {
            //initialize scene
            base.Initialize();
            Window.AllowUserResizing = true;
            InGameScene = Scene.createWithDefaultRenderer(Color.CornflowerBlue);
            InGameScene.setDesignResolution(ScreenWidth, ScreenHeight, Scene.SceneResolutionPolicy.ShowAll);

            //fonts
            SpriteFont baseFont = InGameScene.content.Load<SpriteFont>("Arial");

            //background
            Texture2D backgroundTexture = InGameScene.content.Load<Texture2D>(boardTextureName);
            Entity background = InGameScene.createEntity(boardEntityName);
            background.addComponent(new Sprite(backgroundTexture));
            background.setPosition(new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2));
            background.getComponent<Sprite>().renderLayer = BoardRenderLayer;

            //mouse collider to check what mouse is touching
            Entity mouseCollider = InGameScene.createEntity(mouseColliderEntityName);
            mouseCollider.addComponent(new MouseCollider());

            InitializeGameComponentsUi();

            scene = InGameScene;
        }

        protected override void Update(GameTime gameTime)
        {
        }

        private void InitializeGameComponentsUi(Hero[] myHeroes, Hero[] opponentHeroes, Card[] myCards)
        {
            NezSpriteFont font = new NezSpriteFont(InGameScene.content.Load<SpriteFont>("Arial"));

            BoardAreaUi[] sideBarEntities = new BoardAreaUi[30];

            int localPlayerWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountFirstPlayer : GameConstants.StartingWorkerCountSecondPlayer;
            int opponentWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountSecondPlayer : GameConstants.StartingWorkerCountFirstPlayer;

            for (int x = 0; x < sideBarEntities.Length; x += sideBarEntities.Length / 2)
            {
                HandUi handUi = new HandUi();
                Texture2D handTexture = InGameScene.content.Load<Texture2D>(handTextureName);
                handUi.addComponent(new Sprite(handTexture));
                handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
                InGameScene.addEntity(handUi);

                InPlayUi inPlayUi = new InPlayUi();
                handUi.getComponent<Sprite>().renderLayer = BoardRenderLayer;
                InGameScene.addEntity(inPlayUi);

                bool isOpponent = x < 15;

                Hero[] currentHeroes = isOpponent ? opponentHeroes : myHeroes;
                sideBarEntities[0 + x] = new HeroButton(font, null, currentHeroes[0]);
                if (GameMode == GameMode.ThreeHero)
                {
                    sideBarEntities[1 + x] = new HeroButton(font, null, currentHeroes[1]);
                    sideBarEntities[2 + x] = new HeroButton(font, null, currentHeroes[2]);
                }

                sideBarEntities[3 + x] = new BaseButton(font);
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

                SideBarButton CodexButton = new SideBarButton(font);
                CodexButton.DisplayName.text = Codex.CodexString;
                sideBarEntities[14 + x] = new BoardAreaUi();
            }

            int numberOfColumns = 3;
            for (int x = 0; x < sideBarEntities.Length; x += numberOfColumns)
            {
                int verticalPosition = x / numberOfColumns * ScreenHeight / sideBarEntities.Length / numberOfColumns;
                for (int y = 0; y < numberOfColumns; y++)
                {
                    sideBarEntities[x + y]?.setPosition(new Vector2(
                        y * SideBarButton.SideBarWidth / numberOfColumns,
                        verticalPosition
                    ));
                }
            }
        }
    }

    
}
