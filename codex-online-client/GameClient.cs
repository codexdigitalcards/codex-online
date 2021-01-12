using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codex_online
{

    /// <summary>
    /// Game class responsible for intializing and switching between scenes
    /// </summary>
    public class GameClient : Core
    {
        public static float ScreenWidth { get; } = 1366;
        public static float ScreenHeight { get; } = 768;

        private readonly string boardTextureName = "board";
        private readonly string boardEntityName = "board";
        private readonly string mouseColliderEntityName = "mouse-collider";
        private readonly string handTextureName = "hand-holder";
        private readonly string squadLeaderBuff = "+1 Armor/Taunt";
        private readonly string eliteBuff = "+1 ATK";
        private readonly string scavengerBuff = "Dies: +1 Gold";
        private readonly string technicianBuff = "Dies: +1 Card";
        private readonly string lookoutBuff = "Resist 1";

        private bool localPlayerFirst;
        private NezSpriteFont font;
        private NetworkEntity networkEntity;

        public GameMode GameMode { get; set; } = GameMode.OneHero;
        public GameConstant GameConstants { get; } = new GameConstant();
        public CardListWindow CardListWindow { get; private set; }
        public ClientState ClientState = new ClientState(ClientState.InGame);
        public CodexNetClient NetworkClient { get; private set; }
        public NetworkConstant NetworkConstant { get; private set; }

        protected Scene InGameScene { get; set; }

        public GameClient() : base(width: Convert.ToInt32(ScreenWidth), height: Convert.ToInt32(ScreenHeight), isFullScreen: false, enableEntitySystems: false) { }

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

            font = new NezSpriteFont(InGameScene.Content.Load<SpriteFont>("Arial"));

            List<CardUi> myHeroes = new List<CardUi>();
            List<CardUi> oppHeroes = new List<CardUi>();

            //TODO: remove test data
            Texture2D vandyTexture = InGameScene.Content.Load<Texture2D>("crop_cards/hero_demonology_crop");
            Texture2D shadowBladeTexture = InGameScene.Content.Load<Texture2D>("crop_cards/0012_shadow_blade_crop");
            myHeroes.Add(new CardUi(0, "Hero", vandyTexture));
            myHeroes.Add(new CardUi(1, "Hero", vandyTexture));
            myHeroes.Add(new CardUi(2, "Hero", vandyTexture));
            oppHeroes.Add(new CardUi(3, "Hero", vandyTexture));
            oppHeroes.Add(new CardUi(4, "Hero", vandyTexture));
            oppHeroes.Add(new CardUi(5, "Hero", vandyTexture));

            InitializeGameUiComponents(myHeroes, oppHeroes);
            InitiliazePatrolZones();

            NetworkConstant = new NetworkConstant();
            //TODO: pull out test data
            NetworkClient = new CodexNetClient("127.0.0.1", 12345, NetworkConstant, InGameScene.EntitiesOfType<BoardAreaUi>());
            networkEntity = new NetworkEntity(NetworkClient);
            InGameScene.AddEntity(networkEntity);

            Scene = InGameScene;
        }
        
        private void InitiliazePatrolZones()
        {
            int numberOfRows = 6;
            float rowHeight = ScreenHeight / numberOfRows;
            PatrolSlotUi[] patrolSlots = new PatrolSlotUi[5];

            patrolSlots[0] = new PatrolSlotUi(Patrol.SquadLeader, Name.SquadLeader, squadLeaderBuff, font);
            patrolSlots[1] = new PatrolSlotUi(Patrol.Elite, Name.Elite, eliteBuff, font);
            patrolSlots[2] = new PatrolSlotUi(Patrol.Scavenger, Name.Scavenger, scavengerBuff, font);
            patrolSlots[3] = new PatrolSlotUi(Patrol.Technician, Name.Technician, technicianBuff, font);
            patrolSlots[4] = new PatrolSlotUi(Patrol.Lookout, Name.Lookout, lookoutBuff, font);

            for (int x = 0; x < patrolSlots.Length; x++)
            {
                patrolSlots[x].Position = new Vector2(CardUi.CardWidth * (x - 2) + ScreenWidth / 2, rowHeight * 2.5f);
                InGameScene.AddEntity(patrolSlots[x]);
            }
        }

        private void InitializeGameUiComponents(List<CardUi> myHeroes, List<CardUi> opponentHeroes)
        { 
            BoardAreaUi[] sideBarEntities = new BoardAreaUi[SideBarEntity.NumberOfEntities];

            int localPlayerWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountFirstPlayer : GameConstants.StartingWorkerCountSecondPlayer;
            int opponentWorkerCount = localPlayerFirst ? GameConstants.StartingWorkerCountSecondPlayer : GameConstants.StartingWorkerCountFirstPlayer;

            InPlayUi inPlayUi = new InPlayUi();
            inPlayUi.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.BoardRenderLayer;
            InGameScene.AddEntity(inPlayUi);

            HandUi handUi = new HandUi();
            Texture2D handTexture = InGameScene.Content.Load<Texture2D>(handTextureName);
            handUi.AddComponent(new SpriteRenderer(handTexture));
            handUi.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.BoardRenderLayer;
            InGameScene.AddEntity(handUi);

            for (int x = 0; x < sideBarEntities.Length; x += sideBarEntities.Length / 2)
            {
                bool isOpponent = x < 15;

                List<CardUi> currentHeroes = isOpponent ? opponentHeroes : myHeroes;
                sideBarEntities[0 + x] = new HeroButton(font, currentHeroes[0].GetComponent<SpriteRenderer>(), currentHeroes[0], !isOpponent);
                if (GameMode == GameMode.ThreeHero)
                {
                    sideBarEntities[1 + x] = new HeroButton(font, currentHeroes[1].GetComponent<SpriteRenderer>(), currentHeroes[1], !isOpponent);
                    sideBarEntities[2 + x] = new HeroButton(font, currentHeroes[2].GetComponent<SpriteRenderer>(), currentHeroes[2], !isOpponent);
                }

                sideBarEntities[3 + x] = new BaseButton(font, GameConstants.StartingBaseHealth, !isOpponent);
                sideBarEntities[4 + x] = new AddOnButton(font, !isOpponent);
                sideBarEntities[5 + x] = new SpecUi(font, !isOpponent);

                sideBarEntities[6 + x] = new TechBuildingButton(font, TechLevel.One, !isOpponent);
                sideBarEntities[7 + x] = new TechBuildingButton(font, TechLevel.Two, !isOpponent);
                sideBarEntities[8 + x] = new TechBuildingButton(font, TechLevel.Three, !isOpponent);

                sideBarEntities[9 + x] = isOpponent ? new ZoneButton(font, Name.Discard, 0, !isOpponent) : new VisibleZoneButton(font, Name.Discard, 0, !isOpponent);

                sideBarEntities[10 + x] = new ZoneButton(font, Name.Deck, GameConstants.StartingDeckSize, !isOpponent);

                if (isOpponent)
                {
                    sideBarEntities[11 + x] = new ZoneButton(font, Name.Hand, GameConstants.StartingHandSize, !isOpponent);
                }

                sideBarEntities[12 + x] = new ZoneButton(font, Name.Gold, 0, !isOpponent);
                sideBarEntities[13 + x] = isOpponent ? new ZoneButton(font, Name.Worker, 0, !isOpponent) : new WorkerCountUi(font, NetworkClient, !isOpponent);
                sideBarEntities[14 + x] = isOpponent ? new ZoneButton(font, Name.Codex, 0, !isOpponent) : new VisibleZoneButton(font, Name.Codex, 0, !isOpponent);
            }

            int numberOfColumns = SideBarEntity.NumberOfColumns;
            float sideBarEntityHeight = SideBarEntity.CellHeight;
            float sideBarEntityWidth = SideBarEntity.CellWidth;
            
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
