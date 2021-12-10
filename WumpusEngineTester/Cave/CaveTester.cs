using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester.Cave
{
    [TestClass]
    public class CaveTester
    {
        [TestMethod]
        public void TestCaveConstructor()
        {
            // todo: loop to test all layouts
            for (int i = 1; i <= 5; i++)
            {
                WumpusEngine.Cave c = new WumpusEngine.Cave(i);
            }
        }
    }
}
