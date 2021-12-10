using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WumpusEngine
{
    public class Room
    {
        private int roomNum;
        private Dictionary<Direction, int> exits = new Dictionary<Direction, int>();

        /// <summary>
        /// room info
        /// </summary>
        /// <param name="exits"></param>
        /// <param name="directions"></param>
        public Room(int roomNum, int[] exits, Direction[] directions) // tell what are exits
        {
            for (int i = 0; i < exits.Length; i++)
            {
                this.exits[directions[i]] = exits[i];
            }
            this.roomNum = roomNum + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetRoomNum()
        {
            return this.roomNum;
        }

        /// <summary>
        /// checks current direction and returns if there is an exit
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsThereAnExit(Direction direction)
        {
            return (this.exits.ContainsKey(direction));
        }

        /// <summary>
        /// returns the room that the direction leads to
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public int WhereExitLeads(Direction direction) 
        {
            return this.exits[direction]; // gives you room it leads to
        }
    }
}
