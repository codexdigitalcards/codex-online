using System;

namespace codex_online
{
    /// <summary>
    /// Representation for any area on the game board
    /// </summary>
    public class GameObject
    {
        private static ushort nextId = 0;

        public ushort Id { get; } = GenerateId();
        public event EventHandler Updated;
        public Player Owner { get; set; }


        /// <summary>
        /// Does something when anything on the board is updated
        /// </summary>
        //TODO: make protected
        public virtual void InGameEventUpdated()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }

        protected static ushort GenerateId()
        {
            if (nextId == UInt16.MaxValue)
            {
                throw new OverflowException("Too many gameobjects created. Check for infinite loop.");
            }
            ushort newId = nextId;
            nextId++;
            return newId;
        }
    }
}