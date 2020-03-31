using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class BaseBuilding : Building
    {
        private BaseBuildingStatus status = BaseBuildingStatus.Unbuilt;
        public BaseBuildingStatus Status {
            get
            {
                return status;
            }
            set
            {
                status = value;
                InGameEventUpdated();
            }
        }
    }

    public enum BaseBuildingStatus
    {
        Unbuilt,
        Building,
        Active
    }
}
