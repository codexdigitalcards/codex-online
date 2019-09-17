using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Base : BoardArea
    {
        private int _health = 20;
        public int Health
        {
            get { return _health; }
            set {
                _health = value;
                OnBoardEventUpdated();
            }
        }
    }
}
