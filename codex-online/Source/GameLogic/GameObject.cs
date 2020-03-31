using System;

namespace codex_online
{
    /// <summary>
    /// Representation for any area on the game board
    /// </summary>
    public class GameObject
    {
        public event EventHandler Updated;

        /// <summary>
        /// Does something when anything on the board is updated
        /// </summary>
        //TODO: make protected
        public virtual void InGameEventUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}