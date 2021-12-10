using System;
using WumpusEngine;
using WumpusEngineTester;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WumpusEngineTester
{
    [TestClass]
    public class UnitTest1
    {
        const string playerName = "Joe";
        const string caveName = "Cave1";
        const int score = 100;
        const int numTurns = 15;
        const int numArrows = 2;
        const int numGold = 40;
        [TestMethod]
        public void TestMethod1()
        {
            Score testScore = new Score("Joe", "Cave1", 100, 15, 2, 40);
            Assert.AreEqual(testScore.PlayerName, "Joe");
            Assert.AreEqual(testScore.CaveName, "Cave1");
            Assert.AreEqual(testScore.PlayerScore, 100);
            Assert.AreEqual(testScore.NumTurns, 15);
            Assert.AreEqual(testScore.NumArrows, 2);
            Assert.AreEqual(testScore.NumGold, 40);
        }
        [TestMethod]
        public void TestMethod2()
        {
            High_Score testHighScore = new High_Score();
            


        }
    }
}
