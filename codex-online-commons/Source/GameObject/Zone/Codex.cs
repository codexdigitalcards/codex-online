using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class Codex : Zone
    {
        public static string CodexString { get; } = "Codex";

        public Codex()
        {
            Name = CodexString;
            Cards = new HashSet<Card>();
        }

        public override ICollection<Card> GetCardsCopy()
        {
            return new HashSet<Card>(Cards);
        }
    }
}
