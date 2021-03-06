﻿using codex_online;
using System;

namespace codex_online
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameClient())
                game.Run();
        }
    }
}
