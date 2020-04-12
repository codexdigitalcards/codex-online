using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class GameConstants
    {
        public int StartingBaseHealth { get; } = 20;
        public int StartingDeckSize { get; } = 10;
        public int StartingHandSize { get; } = 5;
        public int StartingWorkerCountFirstPlayer { get; } = 4;
        public int StartingWorkerCountSecondPlayer { get; } = 5;
    }
}
