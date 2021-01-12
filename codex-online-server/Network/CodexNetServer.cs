using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lidgren.Network;

namespace codex_online
{
    public class CodexNetServer : NetServer
    {
        public List<GameObject> ChangedObjects;
        //TODO: move out
        public Player Turn { get; set; }
        public Phase Phase { get; set; }
        public bool PhaseChanged { get; set; }
        public bool[] GoldChanged { get; } = new bool[] { false, false };

        private static string appIdentifier = "Codex";
        private NetworkConstant networkConstant;
        private readonly PlayerConnections playerConnections;
        private readonly SynchronizationContext context;
        

        public CodexNetServer(int port, NetworkConstant networkConstant, Player playerOne, Player playerTwo) : base(new NetPeerConfiguration(appIdentifier) { Port = port })
        {
            Start();
            this.networkConstant = networkConstant;
            context = new SynchronizationContext();
            RegisterReceivedCallback(new SendOrPostCallback(this.MessageRecieved), context);
            playerConnections = new PlayerConnections(playerOne, playerTwo, this);
            ChangedObjects = new List<GameObject>();
        }

        public void ReadCodexMessage(NetIncomingMessage message)
        {
            MethodServerTarget target = (MethodServerTarget)message.ReadByte(networkConstant.ServerTargetBits);
            
            uint id;
            byte effect;
            uint attacked;
            uint prompt;
            uint response;
            Player player = playerConnections[message.SenderConnection];

            switch (target)
            {
                case MethodServerTarget.Worker:
                    id = message.ReadUInt32(networkConstant.IdBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("read worker: {0}", id);
                    //player.worker(cardid) //player based on client
                    break;
                case MethodServerTarget.PlayCard:
                    id = message.ReadUInt32(networkConstant.IdBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("read play card: {0}", id);
                    //player.play(cardid) //player based on client
                    break;
                case MethodServerTarget.ChangePhase:
                    NLog.LogManager.GetCurrentClassLogger().Debug("change phase");
                    //player.ChangePhase();
                    break;
                case MethodServerTarget.BuildTechOne:
                    NLog.LogManager.GetCurrentClassLogger().Debug("BuildTechOne");
                    //player.BuildTechOne();
                    break;
                case MethodServerTarget.BuildTechTwo:
                    NLog.LogManager.GetCurrentClassLogger().Debug("BuildTechTwo");
                    //player.BuildTechTwo();
                    break;
                case MethodServerTarget.BuildTechThree:
                    NLog.LogManager.GetCurrentClassLogger().Debug("BuildTechThree");
                    //player.BuildTechThree();
                    break;
                case MethodServerTarget.BuildAddOn:
                    id = message.ReadUInt32(networkConstant.IdBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("BuildAddOn");
                    //player.BuildAddOn(id);
                    break;
                case MethodServerTarget.DestroyAddOn:
                    NLog.LogManager.GetCurrentClassLogger().Debug("DestroyAddOn");
                    //player.DestroyAddOn();
                    break;
                case MethodServerTarget.ActivateEffect:
                    id = message.ReadUInt32(networkConstant.IdBits);
                    effect = message.ReadByte(networkConstant.AbilityBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("ActivateEffect");
                    //Card.GetCard(cardId).Effects[effect].Activate();
                    break;
                case MethodServerTarget.Attack:
                    id = message.ReadUInt32(networkConstant.IdBits);
                    attacked = message.ReadUInt32(networkConstant.IdBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("Attack");
                    //Card.GetCard(id).Attack(Card.GetCard(attacked));
                    break;
                case MethodServerTarget.Response:
                    prompt = message.ReadUInt32(networkConstant.IdBits);
                    response = message.ReadUInt32(networkConstant.IdBits);
                    NLog.LogManager.GetCurrentClassLogger().Debug("ActivateEffect");
                    //Game.Response(prompt, response);
                    break;
            }
        }

        public void SendChanges()
        {
            playerConnections.CreateNewMessages();
            foreach (GameObject changedObject in ChangedObjects)
            {
                if (changedObject is Card card)
                {
                    if (card.Zone is InPlay)
                    {
                        foreach (Player player in playerConnections.GetPlayers())
                        {
                            playerConnections[player].Write(SendCardInPlay(card, player));
                        }
                    }
                    else if (card.Zone is Deck)
                    {
                        foreach (Player player in playerConnections.GetPlayers())
                        {
                            playerConnections[player].Write(SendZone(card, player));
                        }
                    }
                    else
                    {
                        foreach (Player player in playerConnections.GetPlayers())
                        {
                            if (player == card.Controller || card.Zone is CommandZone)
                            {
                                playerConnections[player].Write(SendCardOutOfPlay(card, player));
                            }
                            else
                            {
                                playerConnections[player].Write(SendZone(card, player));
                            }
                        }
                    }
                }
                else if (changedObject is TechBuilding techBuilding)
                {
                    foreach (Player player in playerConnections.GetPlayers())
                    {
                        playerConnections[player].Write(SendTechBuilding(techBuilding, player));
                    }
                }
                else if (changedObject is Base playerBase)
                {
                    foreach (Player player in playerConnections.GetPlayers())
                    {
                        playerConnections[player].Write(SendBase(playerBase, player));
                    }
                }
                else if (changedObject is AddOn addOn)
                {
                    foreach (Player player in playerConnections.GetPlayers())
                    {
                        playerConnections[player].Write(SendAddOn(addOn, player));
                    }
                }    
            }
            for (int x = 0; x < playerConnections.GetPlayers().Length; x++)
            {
                if (GoldChanged[x])
                {
                    Player goldPlayer = playerConnections.GetPlayers()[x];
                    for (int y = 0; y < playerConnections.GetPlayers().Length; y++)
                    {
                        Player targetPlayer = playerConnections.GetPlayers()[y];

                        playerConnections[targetPlayer].Write((byte)MethodClientTarget.Gold, networkConstant.ClientTargetBits);
                        playerConnections[targetPlayer].Write(goldPlayer == targetPlayer);
                        playerConnections[targetPlayer].Write(goldPlayer.Gold, networkConstant.GoldBits);
                    }

                }
            }
            if (PhaseChanged)
            {
                foreach (Player player in playerConnections.GetPlayers())
                {
                    playerConnections[player].Write(Convert.ToByte(MethodClientTarget.PhaseTurn), networkConstant.ClientTargetBits);
                    playerConnections[player].Write(Turn == player);
                    playerConnections[player].Write((byte)Phase, networkConstant.PhaseBits);
                }
            }
            
        }

        public void SendTargetRequest(Player playerSending, List<GameObject> gameObjects)
        {
            foreach (Player player in playerConnections.GetPlayers())
            {
                if (player == playerSending)
                {
                    NetOutgoingMessage message = CreateMessage();
                    message.Write(Convert.ToByte(MethodClientTarget.RequestTarget), networkConstant.ClientTargetBits);
                    message.Write(gameObjects.Count, networkConstant.ObjectCountBits);
                    foreach (GameObject gameObject in gameObjects)
                    {
                        message.Write(gameObject.Id, networkConstant.IdBits);
                    }
                }
            }
        }

        public void SendOptionRequest(Player playerSending, Name title, List<Name> options)
        {
            foreach (Player player in playerConnections.GetPlayers())
            {
                if (player == playerSending)
                {
                    NetOutgoingMessage message = CreateMessage();
                    message.Write(Convert.ToByte(MethodClientTarget.RequestOption), networkConstant.ClientTargetBits);
                    message.Write((byte)title, networkConstant.NameBits);
                    message.Write(options.Count, networkConstant.ObjectCountBits);
                    foreach (Name option in options)
                    {
                        message.Write((byte)option, networkConstant.NameBits);
                    }
                }
            }
        }

        private NetOutgoingMessage SendAddOn(AddOn addOn, Player owner)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.AddOn), networkConstant.ClientTargetBits);
            message.Write(addOn.Owner == owner);
            message.Write(addOn.Health, networkConstant.AttackHealthBits);
            message.Write((byte)addOn.Status, networkConstant.BuildingStatusBits);
            message.Write(addOn.Type.Id, networkConstant.AddOnTypeBits);
            return message;
        }

        private NetOutgoingMessage SendBase(Base playerBase, Player owner)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.Base), networkConstant.ClientTargetBits);
            message.Write(playerBase.Owner == owner);
            message.Write(playerBase.Health, networkConstant.AttackHealthBits);
            message.Write((byte)playerBase.Status, networkConstant.BuildingStatusBits);
            return message;
        }

        private NetOutgoingMessage SendCard(Card card, Player controller)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(card.Id, networkConstant.IdBits);
            message.Write((byte)card.Name, networkConstant.NameBits);
            message.Write(card.Controller == controller);

            message.Write(card is FightableCard);
            if (card is FightableCard fightableCard)
            {
                message.Write(fightableCard.Attack, networkConstant.AttackHealthBits);

            }

            message.Write(card is IAttackable);
            if (card is IAttackable attackableCard)
            {
                message.Write(attackableCard.Health, networkConstant.AttackHealthBits);
            }
            return message;
        }

        private NetOutgoingMessage SendCardInPlay(Card card, Player controller)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.CardInPlay), networkConstant.ClientTargetBits);
            message.Write(SendCard(card, controller));
            message.Write(card.Tapped);
            message.Write(card.Runes.Count, networkConstant.RuneBits);
            foreach (KeyValuePair<Rune, int> rune in card.Runes)
            {
                message.Write(Convert.ToUInt16(rune.Key), networkConstant.RuneBits);
                message.Write(rune.Value, networkConstant.RuneCountBits);
            }

            List<Ability> activatedAbilities = card.Abilities.FindAll(ability => ability.Activated);
            message.Write(activatedAbilities.Count, networkConstant.AbilityCountBits);
            foreach (Ability ability in activatedAbilities)
            {
                message.Write(ability.Id, networkConstant.AbilityBits);
            }

            List<Ability> statuses = card.Abilities.FindAll(ability => ability.Tags.Contains(AbilityTag.Status));
            message.Write(statuses.Count, networkConstant.AbilityCountBits);
            foreach (Ability status in statuses)
            {
                message.Write(status.Id, networkConstant.AbilityBits);
            }

            PatrolSlot patrol = controller.InPlay.PatrolZone.FirstOrDefault(patrolSlot => patrolSlot.Card == card);
            message.Write(patrol != null);
            if (patrol != null)
            {
                message.Write((byte)patrol.Patrol, networkConstant.PatrolSlotBits);
            }

            return message;
        }

        private NetOutgoingMessage SendCardOutOfPlay(Card card, Player controller)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.CardOutOfPlay), networkConstant.ClientTargetBits);
            message.Write(SendCard(card, controller));
            message.Write((byte)card.Zone.Name, networkConstant.NameBits);
            message.Write(card.Cost, networkConstant.CostBits);
            message.Write(card.Playble());
            return message;
        }

        private NetOutgoingMessage SendZone(Card card, Player owner)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.Zone), networkConstant.ClientTargetBits);
            message.Write(card.Owner == owner);
            message.Write((byte)card.Zone.Name, networkConstant.NameBits);
            message.Write(card.Zone.Count(), networkConstant.ZoneCountBits);
            return message;
        }

        private NetOutgoingMessage SendTechBuilding(TechBuilding techBuilding, Player owner)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodClientTarget.TechBuilding), networkConstant.ClientTargetBits);
            message.Write(techBuilding.Owner == owner);
            message.Write((byte)techBuilding.Level, networkConstant.TechLevelBits);
            message.Write(techBuilding.Health, networkConstant.AttackHealthBits);
            message.Write(techBuilding.Buildable());
            message.Write((byte)techBuilding.Status, networkConstant.BuildingStatusBits);
            message.Write(techBuilding.Cost, networkConstant.CostBits);
            return message;
        }

        private void MessageRecieved(object NetServer)
        {
            NetIncomingMessage message;

            if ((message = ReadMessage()) == null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        ReadCodexMessage(message);
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus connectionStatus = (NetConnectionStatus) message.ReadByte();
                        if (connectionStatus == NetConnectionStatus.Connected)
                        {
                            playerConnections.AddConnection(message.SenderConnection);
                        }
                        break;
                    default:

                        break;
                }
            }
        }

        private class PlayerConnections
        {
            private readonly NetServer server;

            public Player PlayerOne { get; set; }
            public Player PlayerTwo { get; set; }
            public NetConnection ConnectionOne { get; set; }
            public NetConnection ConnectionTwo { get; set; }
            public NetOutgoingMessage MessageOne { get; set; }
            public NetOutgoingMessage MessageTwo { get; set; }

            public PlayerConnections(Player playerOne, Player playerTwo, NetServer server)
            {
                PlayerOne = playerOne;
                PlayerTwo = playerTwo;
                this.server = server;
            }

            public Player this[NetConnection connection]
            {
                get {
                    if (connection == ConnectionOne)
                    {
                        return PlayerOne;
                    }
                    else if (connection == ConnectionTwo)
                    {
                        return PlayerTwo;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public NetOutgoingMessage this[Player player]
            {
                get
                {
                    if (player == PlayerOne)
                    {
                        return MessageOne;
                    }
                    else if (player == PlayerTwo)
                    {
                        return MessageTwo;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public void AddConnection(NetConnection connection)
            {
                if (ConnectionOne is null)
                {
                    ConnectionOne = connection;
                }
                else if (ConnectionTwo is null)
                {
                    ConnectionTwo = connection;
                }
                else
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn("extra connection attempted: {1}", connection.RemoteEndPoint);
                }
            }

            public Player[] GetPlayers()
            {
                Player[] players = { PlayerOne, PlayerTwo};
                return players;
            }

            public void CreateNewMessages()
            {
                MessageOne = server.CreateMessage();
                MessageTwo = server.CreateMessage();
            }
        }
    }
}
