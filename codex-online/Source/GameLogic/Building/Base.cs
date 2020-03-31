using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Base : Building
    {
        public BaseStatus Status { get; set; }
    }
    public enum BaseStatus
    {
        Nothing,
        Flying
    }
}
