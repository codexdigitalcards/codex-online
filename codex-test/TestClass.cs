﻿using codex_online;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace codex_test
{
    class TestClass
    {
        public static void Main(String[] args)
        {
            GameClient client = new GameClient();
            Thread thr = new Thread(new ThreadStart(client.Run));
            thr.Start();
            SpinWait.SpinUntil(() => GameClient.Scene != null);

            CodexNetServer server = new CodexNetServer(12345, new NetworkConstant());
            Thread serverThread = new Thread(new ThreadStart(server.ListenForMessages));
            serverThread.Start();
            SpinWait.SpinUntil(() => server.Status == NetPeerStatus.Running);

            //PatrolTest(client);
            //CardListWindowTest(client, 25, 5, 0);
            //NetworkTest(client);
            WorkerNetworkTest(client, server);
        }

        private static void CardListWindowTest(GameClient client, int numberOfCards, int minimumCards, int maximumCards)
        {
            Scene InGameScene = GameClient.Scene;
            Texture2D shadowBladeTexture = InGameScene.Content.Load<Texture2D>("crop_cards/0012_shadow_blade_crop");

            CardUi testCard = new CardUi(3, "shadowblade", shadowBladeTexture);
            InGameScene.AddEntity(testCard);

            List<CardUi> openWindowCards = new List<CardUi>();
            NezSpriteFont font2 = new NezSpriteFont(InGameScene.Content.Load<SpriteFont>("Debug"));
            for (int x = 0; x < numberOfCards; x++)
            {
                TextComponent text = new TextComponent(font2, x.ToString() + x.ToString() + x.ToString(), Vector2.Zero, Color.MediumVioletRed);

                CardUi cardUi3 = new CardUi(4, "shadowblade", shadowBladeTexture)
                {
                    Enabled = false
                };
                cardUi3.AddComponent(text);
                text.RenderLayer = -50;
                InGameScene.AddEntity(cardUi3);
                openWindowCards.Add(cardUi3);
            }

            client.CardListWindow.OpenWindow(openWindowCards, true, minimumCards, maximumCards);
        }

        private static void PatrolTest(GameClient client)
        {
            Scene InGameScene = GameClient.Scene;
            Texture2D shadowBladeTexture = InGameScene.Content.Load<Texture2D>("crop_cards/0012_shadow_blade_crop");

            

            for (int x = 0; x < 5; x++)
            {
                CardUi testCard = new CardUi(Convert.ToUInt16(2+x), "shadowblade", shadowBladeTexture);
                InGameScene.AddEntity(testCard);
                InGameScene.EntitiesOfType<PatrolSlotUi>()[x].PatrolCard(testCard);
            }
            
        }

        private static void NetworkTest(GameClient client)
        {
            var config = new NetPeerConfiguration("Codex")
            { Port = 12345 };
            NetServer server = new NetServer(config);
            server.Start();

            Unit unit = new Unit()
            {
                Cost = 2,
                Tapped = false
            };

            while (true)
            {
                client.NetworkClient.SendGameAction(MethodServerTarget.Worker, new Card[] { unit });
                System.Threading.Thread.Sleep(1000);
                NetIncomingMessage message;
                if ((message = server.ReadMessage()) != null)
                {
                    switch (message.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            MethodServerTarget target = (MethodServerTarget)message.ReadByte();
                            int cardNumber = message.ReadByte();
                            Card[] cards = new Card[cardNumber];
                            for (int x = 0; x < cardNumber; x++)
                            {
                                cards[x] = new Unit();
                                message.ReadAllFields(cards[x]);
                            }

                            Console.WriteLine(target);
                            Console.WriteLine(cardNumber);
                            Console.WriteLine(cards[0].Name);
                            Console.WriteLine(cards[0].Cost);
                            Console.WriteLine(cards[0].Tapped);
                            break;
                    }
                }
            }
        }

        private static void WorkerNetworkTest(GameClient client, CodexNetServer codexNetServer)
        {
            Scene InGameScene = GameClient.Scene;
            Texture2D shadowBladeTexture = InGameScene.Content.Load<Texture2D>("crop_cards/0012_shadow_blade_crop");
            CardUi testCard = new CardUi(2, "shadowblade", shadowBladeTexture);
            InGameScene.AddEntity(testCard);
            InGameScene.EntitiesOfType<HandUi>()[0].AddCards(new List<CardUi>() { testCard });
        }
    }
}
