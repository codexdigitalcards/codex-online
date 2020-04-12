using System;

namespace codex_online
{
    public class Spec
    {
        public static readonly Spec Bashing = new Spec("Bashing");
        public static readonly Spec Finesse = new Spec("Finesse");

        public String Name { get; private set; }

        private Spec(String name)
        {
            Name = name;
        }
    }
}
