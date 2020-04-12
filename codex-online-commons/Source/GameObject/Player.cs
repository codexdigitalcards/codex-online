using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Player : GameObject
    {
        public Base Base { get; set; }
        public AddOn AddOn { get; set; }
        public Spec Spec { get; set; }
        public Spec ExtraSpec { get; set; }
        public int Gold { get; set; }
        public int WorkerCount { get; set; }

        public TechBuilding TechOne { get; set; }
        public TechBuilding TechTwo { get; set; }
        public TechBuilding TechThree { get; set; }

        public Hand Hand { get; set; }
        public Deck Deck { get; set; }
        public Discard Discard { get; set; }
        public Codex Codex { get; set; }

        public Hero HeroOne { get; set; }
        public Hero HeroTwo { get; set; }
        public Hero HeroThree { get; set; }
    }
}
