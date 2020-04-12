using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class WorkerCountUi : BoardAreaUi
    {
        protected String WorkersString { get; } = "Workers";
        protected Text Workers { get; set; }

        public WorkerCountUi(NezSpriteFont font, int workerCount)
        {
            Workers = new Text(font, workerCount.ToString(), Vector2.Zero, Color.Black);
            addComponent(Workers);
        }

        private void UpdateWorkers(int workerCount)
        {
            Workers.text = workerCount.ToString();
        }
    }
}
