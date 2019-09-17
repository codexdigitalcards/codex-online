using NLog;

namespace codex_online
{
    public static class AnimationHandler
    {
        private static int AnimationsRunning = 0;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// call when adding an animation that needs the rest of the game to pause while executing.
        /// </summary>
        public static void AddAnimation()
        {
            AnimationsRunning++;
        }

        /// <summary>
        /// call once after each animation ends.
        /// </summary>
        public static void EndAnimation()
        {
            AnimationsRunning--;
            if (AnimationsRunning < 0)
            {
                AnimationsRunning = 0;
                logger.Error("AnimationHandler.EndAnimation: tried to end an animation that did not exist");
            }
        }

        /// <summary>
        /// returns true if any animations are running.
        /// </summary>
        public static bool IsAnimationRunning()
        {
            return AnimationsRunning > 0;
        }
    }
}
