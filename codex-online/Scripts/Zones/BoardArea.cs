using System;

namespace codex_online
{
    /// <summary>
    /// Representation for any area on the game board
    /// </summary>
    public class BoardArea
    {
        public event EventHandler Updated;

        public static float SideBarWidth { get; } = 144;

        /// <summary>
        /// Does something when anything on the board is updated
        /// </summary>
        //TODO: make protected
        public virtual void OnBoardEventUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
}