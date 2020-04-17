using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class WorkerCountUi : SideBarEntity
    {
        protected String WorkersString { get; } = "Workers";

        public WorkerCountUi(NezSpriteFont font, int workerCount) : base(font)
        {
            TopDisplay.text = WorkersString;
            MiddleDisplay.text = workerCount.ToString();
        }

        private void UpdateWorkers(int workerCount)
        {
            MiddleDisplay.text = workerCount.ToString();
        }
    }
}
