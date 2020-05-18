using Nez;
using System;

namespace codex_online
{
    public class WorkerCountUi : SideBarEntity
    {
        protected String WorkersString { get; } = "Workers";

        public WorkerCountUi(NezSpriteFont font, int workerCount) : base(font)
        {
            TopDisplay.Text = WorkersString;
            MiddleDisplay.Text = workerCount.ToString();
        }

        private void UpdateWorkers(int workerCount)
        {
            MiddleDisplay.Text = workerCount.ToString();
        }
    }
}
