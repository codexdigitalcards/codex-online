﻿using Lidgren.Network;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace codex_online
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        private static string LogName { get; } = "logfile";
        private static string LogFileName { get; } = "log_file.txt";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget logfile = new FileTarget(LogName) { FileName = LogFileName };

#if DEBUG
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
#else
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
#endif

            LogManager.Configuration = config;

            CodexNetServer server = new CodexNetServer(12345, new NetworkConstant(), new Player(), new Player());
            NLog.LogManager.GetCurrentClassLogger().Debug("read worker: ");
        }
    }
}
