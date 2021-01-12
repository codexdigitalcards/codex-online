using Nez;
using System;

namespace codex_online
{
    public class WorkerCountUi : VisibleZoneButton
    {
        public WorkerCountUi(NezSpriteFont font, CodexNetClient networkClient, bool owner) : base(font, codex_online.Name.Worker, 0, owner)
        {
            NetworkClient = networkClient;
        }

        public override void CardDropped(CardUi card)
        {
            NetworkClient.SendWorker(card.CardId);
        }
    }
}
