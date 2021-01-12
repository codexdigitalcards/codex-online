using System.Collections.Generic;

namespace codex_online
{
    public class Ability
    {
        public uint Id { get; }
        public bool Activated { get; }
        public List<AbilityTag> Tags { get; } = new List<AbilityTag>();
    }

    public enum AbilityTag
    {
        Status
    }
}