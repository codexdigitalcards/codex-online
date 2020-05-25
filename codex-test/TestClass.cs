using codex_online;
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

            PatrolTest(client);
            //CardListWindowTest(client, 25, 5, 0);
        }

        private static void CardListWindowTest(GameClient client, int numberOfCards, int minimumCards, int maximumCards)
        {
            Scene InGameScene = GameClient.Scene;
            Texture2D shadowBladeTexture = InGameScene.Content.Load<Texture2D>("crop_cards/0012_shadow_blade_crop");

            CardUi testCard = new CardUi(new Card() { Name = "imp" }, shadowBladeTexture);
            InGameScene.AddEntity(testCard);

            List<CardUi> openWindowCards = new List<CardUi>();
            NezSpriteFont font2 = new NezSpriteFont(InGameScene.Content.Load<SpriteFont>("Debug"));
            for (int x = 0; x < numberOfCards; x++)
            {
                TextComponent text = new TextComponent(font2, x.ToString() + x.ToString() + x.ToString(), Vector2.Zero, Color.MediumVioletRed);

                CardUi cardUi3 = new CardUi(new Card() { Name = "imp" + x }, shadowBladeTexture);
                cardUi3.Enabled = false;
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
                CardUi testCard = new CardUi(new Card() { Name = "imp" }, shadowBladeTexture);
                InGameScene.AddEntity(testCard);
                InGameScene.EntitiesOfType<PatrolSlotUi>()[x].PatrolCard(testCard);
            }
            
        }
    }
}
