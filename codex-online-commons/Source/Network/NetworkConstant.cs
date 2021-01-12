using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class NetworkConstant
    {

        //TODO: make the convert from enum to bits a method
        public int IdBits { get; } = 10;
        public int ServerTargetBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(MethodServerTarget)).Length, 2)));
        public int ClientTargetBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(MethodClientTarget)).Length, 2)));
        public int AbilityBits { get; } = 8;
        public int AbilityCountBits { get; } = 3;
        public int RuneBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(Rune)).Length, 2)));
        public int RuneCountBits { get; } = 10;
        public int AttackHealthBits { get; } = 6;
        public int NameBits { get; } = 8;
        public int CostBits { get; } = 8;
        public int ZoneCountBits { get; } = 8;
        public int ObjectCountBits { get; } = 8;

        public int BuildingStatusBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(BaseBuildingStatus)).Length, 2)));
        public int AddOnTypeBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(AddOnType.Count, 2)));
        public int PhaseBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(Phase)).Length, 2)));
        public int PatrolSlotBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(Patrol)).Length, 2)));
        public int TechLevelBits { get; } = Convert.ToInt32(Math.Ceiling(Math.Log(Enum.GetValues(typeof(TechLevel)).Length, 2)));
        public int GoldBits { get; } = 15;
    }
}
