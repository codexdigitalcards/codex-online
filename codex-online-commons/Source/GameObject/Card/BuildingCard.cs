﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class BuildingCard : Card, IAttackable
    {
        public int Health { get; set; }
    }
}
