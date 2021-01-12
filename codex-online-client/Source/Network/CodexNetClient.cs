using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;

namespace codex_online
{
    public class CodexNetClient : NetClient
    {
        private readonly static string appIdentifier = "Codex";
        private readonly NetworkConstant networkConstant;
        private readonly List<BoardAreaUi> boardAreas; 

        public CodexNetClient(String host, int port, NetworkConstant networkConstant, List<BoardAreaUi> boardAreas) : base(new NetPeerConfiguration(appIdentifier))
        {
            this.networkConstant = networkConstant;
            this.boardAreas = boardAreas;
            Start();
            Connect(host, port);
        }

        public void ListenForMessages()
        {
            NetIncomingMessage message;

            if ((message = ReadMessage()) == null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        MethodClientTarget target = (MethodClientTarget)message.ReadByte(networkConstant.ClientTargetBits);

                        CardUi card;
                        Name zoneName;
                        bool owner;
                        int health;
                        BaseBuildingStatus status;

                        switch (target)
                        {
                            case MethodClientTarget.CardInPlay:
                                card = ReadCard(message);
                                card.Tapped = message.ReadBoolean();

                                card.Runes.Clear();
                                int runeTypes = message.ReadInt32(networkConstant.RuneBits);
                                for (int x = 0; x < runeTypes; x++)
                                {
                                    Rune rune = (Rune)message.ReadUInt32(networkConstant.RuneBits);
                                    card.Runes[rune] = message.ReadInt32(networkConstant.RuneCountBits);
                                }

                                card.ActivatedAbilities.Clear();
                                int abilityCount = message.ReadInt32(networkConstant.AbilityCountBits);
                                for (int x = 0; x < abilityCount; x++)
                                {
                                    card.ActivatedAbilities.Add((ushort)message.ReadUInt32(networkConstant.AbilityBits));
                                }

                                card.Statuses.Clear();
                                int statusCount = message.ReadInt32(networkConstant.AbilityCountBits);
                                for (int x = 0; x < statusCount; x++)
                                {
                                    card.Statuses.Add((ushort)message.ReadUInt32(networkConstant.AbilityBits));
                                }

                                if (message.ReadBoolean())
                                {
                                    byte patrolType = message.ReadByte(networkConstant.PatrolSlotBits);
                                    PatrolSlotUi patrolSlot = GetBoardArea<PatrolSlotUi>(patrol => (byte)patrol.Patrol == patrolType);
                                    patrolSlot.PatrolCard(card);
                                }

                                InPlayUi inPlay = GetBoardArea<InPlayUi>();
                                inPlay.AddCard(card);
                                break;
                            case MethodClientTarget.CardOutOfPlay:
                                card = ReadCard(message);
                                zoneName = (Name)message.ReadByte(networkConstant.NameBits);
                                card.Cost = (short)message.ReadInt32(networkConstant.CostBits);
                                card.Playable = message.ReadBoolean();

                                switch (zoneName)
                                {
                                    case Name.CommandZone:
                                        HeroButton currentHeroButton = GetBoardArea<HeroButton>(heroZone => heroZone.Hero == card);
                                        currentHeroButton.HeroPlayable();
                                        break;
                                    case Name.Discard:
                                        GetBoardArea<VisibleZoneButton>(VisibleZone => VisibleZone.AreaName == Name.Discard).AddCard(card);
                                        break;
                                    case Name.Hand:
                                        GetBoardArea<HandUi>().AddCard(card);
                                        break;
                                    case Name.Codex:
                                        GetBoardArea<VisibleZoneButton>(VisibleZone => VisibleZone.AreaName == Name.Codex).AddCard(card);
                                        break;
                                    case Name.Worker:
                                        GetBoardArea<WorkerCountUi>().AddCard(card);
                                        break;
                                }
                                break;
                            case MethodClientTarget.Zone:
                                owner = message.ReadBoolean();
                                zoneName = (Name)message.ReadByte(networkConstant.NameBits);
                                ZoneButton currentZone = GetBoardArea<ZoneButton>(zoneButton => zoneButton.Owner == owner && zoneButton.AreaName == zoneName);
                                currentZone.UpdateZone(message.ReadInt32(networkConstant.ZoneCountBits));
                                break;
                            case MethodClientTarget.TechBuilding:
                                owner = message.ReadBoolean();
                                TechLevel techLevel = (TechLevel)message.ReadByte(networkConstant.TechLevelBits);
                                TechBuildingButton techBuildingButton = GetBoardArea<TechBuildingButton>
                                (
                                    techBuilding => 
                                        techBuilding.Owner == owner &&
                                        techBuilding.TechLevel == techLevel
                                );
                                health = message.ReadInt32(networkConstant.AttackHealthBits);
                                bool buildable = message.ReadBoolean();
                                status = (BaseBuildingStatus)message.ReadByte(networkConstant.BuildingStatusBits);
                                int cost = message.ReadInt32(networkConstant.CostBits);
                                techBuildingButton.UpdateBuilding(health, buildable, status, cost);
                                break;
                            case MethodClientTarget.Base:
                                owner = message.ReadBoolean();
                                BaseButton selectedBase = GetBoardArea<BaseButton>(baseButton => baseButton.Owner == owner);
                                selectedBase.UpdateHealth(message.ReadInt32(networkConstant.AttackHealthBits));
                                selectedBase.UpdateStatus((BaseStatus)message.ReadByte(networkConstant.BuildingStatusBits));
                                break;
                            case MethodClientTarget.AddOn:
                                owner = message.ReadBoolean();
                                AddOnButton selectedAddOn = GetBoardArea<AddOnButton>(addOnButton => addOnButton.Owner == owner);
                                selectedAddOn.UpdateHealth(message.ReadInt32(networkConstant.AttackHealthBits));
                                status = (BaseBuildingStatus)message.ReadByte(networkConstant.BuildingStatusBits);
                                AddOnType addOnType = AddOnType.AddOnTypeDictionary[message.ReadInt32(networkConstant.AddOnTypeBits)];
                                selectedAddOn.UpdateStatus(status, addOnType);
                                break;
                            case MethodClientTarget.Gold:
                                owner = message.ReadBoolean();
                                ZoneButton goldButton = GetBoardArea<ZoneButton>
                                (
                                    goldZone =>
                                        goldZone.Owner == owner &&
                                        goldZone.AreaName == Name.Gold
                                );
                                goldButton.UpdateZone(message.ReadInt32(networkConstant.GoldBits));
                                break;
                            case MethodClientTarget.PhaseTurn:
                                bool thisPlayerTurn = message.ReadBoolean();
                                Phase phase = (Phase)message.ReadByte(networkConstant.PhaseBits);
                                //TODO: do something with phase changes
                                break;
                        }
                        break;

                    default:

                        break;
                }
            }
        }

        public void SendWorker(ushort cardId)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.Worker), networkConstant.ServerTargetBits);
            message.Write(cardId, networkConstant.IdBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendPlayCard(ushort cardId)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.PlayCard), networkConstant.ServerTargetBits);
            message.Write(cardId, networkConstant.IdBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendChangePhase()
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.ChangePhase), networkConstant.ServerTargetBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendBuildTechOne()
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.BuildTechOne), networkConstant.ServerTargetBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendBuildTechTwo()
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.BuildTechTwo), networkConstant.ServerTargetBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendBuildTechThree()
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.BuildTechThree), networkConstant.ServerTargetBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendBuildAddOn(ushort addOnId)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.BuildAddOn), networkConstant.ServerTargetBits);
            message.Write(addOnId, networkConstant.IdBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendDestroyAddOn()
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.DestroyAddOn), networkConstant.ServerTargetBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendActivateEffect(ushort cardId, byte effect)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.DestroyAddOn), networkConstant.ServerTargetBits);
            message.Write(effect, networkConstant.AbilityBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendAttackCard(ushort attackerId, ushort targetId)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.Attack), networkConstant.ServerTargetBits);
            message.Write(attackerId, networkConstant.IdBits);
            message.Write(targetId, networkConstant.IdBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendResponse(ushort prompt, ushort respone)
        {
            NetOutgoingMessage message = CreateMessage();
            message.Write(Convert.ToByte(MethodServerTarget.Response), networkConstant.ServerTargetBits);
            message.Write(prompt, networkConstant.IdBits);
            message.Write(respone, networkConstant.IdBits);
            SendMessage(message, NetDeliveryMethod.ReliableOrdered);
        }

        private CardUi ReadCard(NetIncomingMessage message)
        {
            ushort cardId = (ushort)message.ReadUInt32(networkConstant.IdBits);
            Name cardName = (Name)message.ReadUInt32(networkConstant.NameBits);
            CardUi card;
            if (CardUi.CardToCardUiMap.ContainsKey(cardId))
            {
                card = CardUi.CardToCardUiMap[cardId];
                card.CardName = cardName;
            }
            else
            {
                card = new CardUi(cardId, cardName);
            }

            card.Controlled = message.ReadBoolean();
            if (message.ReadBoolean())
            {
                card.Attack = message.ReadInt32(networkConstant.AttackHealthBits);
            }
            else
            {
                card.Attack = null;
            }

            if (message.ReadBoolean())
            {
                card.Health = message.ReadInt32(networkConstant.AttackHealthBits);
            }
            else
            {
                card.Health = null;
            }

            return card;
        }

        private T GetBoardArea<T>() where T : BoardAreaUi
        {
            return GetBoardArea<T>(area => true);
        }
        private T GetBoardArea<T>(Func<T, bool> function) where T : BoardAreaUi
        {
            return boardAreas.Where(area => area is T).Select(area => (T)area).Where(function).Single();
        }
    }
}
