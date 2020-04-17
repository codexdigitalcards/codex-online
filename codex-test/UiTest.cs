using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using codex_online;
using System.IO;
using System;

namespace codex_test
{
    [TestClass]
    public class UiTest
    {
        [TestInitialize()]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            Mock<Hero>  hero = new Mock<Hero>(MockBehavior.Strict);
            hero.SetupProperty(h => h.Name);
            hero.SetupProperty(h => h.Cost);
            new GameClient().Run();
        }
    }
}
