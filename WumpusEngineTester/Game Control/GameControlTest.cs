using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.Game_Control
{
    [TestClass]
    public class GameControlTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            GameControl gc = GameControl.GetMaintainedInstance();
            Assert.AreEqual(GameState.MainMenu, gc.GetState());
        }

        [TestMethod]
        [Ignore]
        public void TestGameStart()
        {
            GameControl gc = GameControl.GetMaintainedInstance();
            gc.GameStart("Dora", 1);
            Assert.AreEqual(GameState.Roaming, gc.GetState());
        }

        [TestMethod]
        public void TestMatchWin()
        {
            GameControl gc = GameControl.GetMaintainedInstance();

        }

        [TestMethod]
        public void TestCheckAnswers()
        {

        }

        [TestMethod]
        [Ignore]
        public void TestCheckHazards()
        {

        }
    }
}
