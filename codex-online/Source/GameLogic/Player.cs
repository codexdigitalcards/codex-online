using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Player : GameObject
    {
        public AddOn AddOn { get; set; }
        public Spec Spec { get; set; }
        public Spec ExtraSpec { get; set; }
        public int Gold { get; set; }
    }
}
