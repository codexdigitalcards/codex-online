using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public abstract class BaseBuilding : Building
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

        public abstract bool Buildable();
    }

    public enum BaseBuildingStatus
    {
        Unbuilt,
        Building,
        Active
    }
}
