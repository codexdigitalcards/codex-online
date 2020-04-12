using System;
using System.Collections.Generic;

namespace codex_online
{

    /// <summary>
    /// Game class responsible for intializing and switching between scenes
    /// </summary>
    public class Game1
    {
        public static int BoardRenderLayer { get; } = 1;
        public static int ScreenWidth { get; } = 1366;
        public static int ScreenHeight { get; } = 768;

        private static readonly string boardTextureName = "board";
        private static readonly string boardEntityName = "board";
        private static readonly string mouseColliderEntityName = "mouse-collider";
        private static readonly string handTextureName = "hand-holder";

        public static GameMode GameMode { get; set; } = GameMode.OneHero;


        public Hand Hand { get; set; }
        public InPlay InPlay { get; set; }

        /// <summary>
        /// Initialize game
        /// </summary>
        protected void Initialize()
        {

        }
    }
}
