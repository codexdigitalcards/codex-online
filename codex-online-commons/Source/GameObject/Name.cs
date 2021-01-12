using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    //TODO: move into a config/json/whatever
    public enum Name
    {
        InPlay,
        Hand,
        Discard,
        Deck,
        CommandZone,
        Worker,
        Codex,
        SquadLeader,
        Elite,
        Scavenger,
        Technician,
        Lookout,

        Gold,
        
        TimelyMessenger
    }
    public class NameDictionary
    {
        public static Dictionary<Name, string> Dictionary = new Dictionary<Name, string>()
        {
            { Name.InPlay, "In Play"},
            { Name.Hand, "Hand"},
            { Name.Discard, "Discard"},
            { Name.Deck, "Deck"},
            { Name.CommandZone, "Command Zone"},
            { Name.Worker, "Worker"},

            { Name.TimelyMessenger, "Timely Messenger"},
            
        };
    }
}
