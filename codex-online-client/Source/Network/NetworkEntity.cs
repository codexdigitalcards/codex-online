using Lidgren.Network;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codex_online
{
    public class NetworkEntity : Entity
    {
        private readonly CodexNetClient client;

        public NetworkEntity(CodexNetClient client)
        {
            this.client = client;
        }

        public override void Update()
        {
            client.ListenForMessages();
        }
    }
}
