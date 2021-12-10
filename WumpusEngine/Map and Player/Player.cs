using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WumpusEngine
{
    class Player
    {
        private string P;
        private int[] inventory = new int[3];

        /// <summary>
        /// give the player a name and its starting inventories
        /// </summary>
        /// <param name="playerName"></param>
        public Player(string playerName)
        {
            this.P = playerName;
            this.inventory[0] = 3;   // Arrows
            this.inventory[1] = 0;   // Gold Coins
            this.inventory[2] = 0;   // Turns
        }

        /// <summary>
        /// returns player name
        /// </summary>
        /// <returns></returns>
        public string PlayerName()
        {
            return this.P;
        }

        /// <summary>
        /// adds a turn and a gold coin every time player moves
        /// </summary>
        /// <returns></returns>
        public void UpdateInventory()
        {
            if (inventory[1] <= 100)
                this.inventory[1]++;
            this.inventory[2]++;
        }

        /// <summary>
        /// adds two arrows to the player inventory
        /// </summary>
        public void PurchaseArrows()
        {
            this.inventory[0] = this.inventory[0] + 2;
        }

        /// <summary>
        /// subtracts an arrow from player inventory
        /// </summary>
        public void ShootArrows()
        {
            this.inventory[0] = this.inventory[0] - 1;
        }

        public void MinusGoldCoin()
        {
            this.inventory[1] = this.inventory[1] - 1;
        }

        /// <summary>
        /// Returns player inventory
        /// </summary>
        /// <returns></returns>
        public int[] PlayerInventory()
        {
            return this.inventory;
        }

        /// <summary>
        /// Calculates and returns the end score
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public int EndScore()
        {
            int score = 100 - this.inventory[2] + this.inventory[1] + (10 * this.inventory[0]);
            return score;
        }
    }
}
