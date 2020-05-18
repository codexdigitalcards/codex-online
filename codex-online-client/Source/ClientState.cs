using System;
using System.Collections.Generic;

namespace codex_online
{
    public class ClientState
    {
        public static int InGame = 0;
        public static int CardListWindow = 1;
        public static List<int> AllStates { get; } = new List<int>() { InGame, CardListWindow };

        private int state;
        public int State
        {
            get
            {
                return state;
            }
            set
            {
                if (AllStates.Contains(value))
                {
                    state = value;
                }
                else
                {
                    throw new ArgumentException("invalid client state", "State");
                }
            }
        }

        public ClientState(int state)
        {
            State = state;
        }
    }
}
