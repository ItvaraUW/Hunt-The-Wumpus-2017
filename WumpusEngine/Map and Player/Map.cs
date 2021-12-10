using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WumpusEngine
{
    class Map
    {
        private int CaveNum;
        private Cave Cave;
        private int currentRoom;
        private int startRoom;
        private Player Player;
        private int pit1;
        private int pit2;
        private int bat1;
        private int bat2;
        private int wumpus;
        private Random rand;

        /// <summary>
        /// Selects cave and player name
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="caveSelection"></param>
        /// <param name="rand"></param>
        public Map(string playerName, int caveSelection, Random rand)
        {
            this.CaveNum = caveSelection;
            this.Player = new Player(playerName);
            this.rand = rand;
            this.Cave = new Cave(caveSelection);
        }

        /// <summary>
        /// Returns the cave number
        /// </summary>
        /// <returns></returns>
        public int CaveNumber()
        {
            return this.CaveNum;
        }

        /// <summary>
        /// returns player name
        /// </summary>
        /// <returns></returns>
        public string PlayerName()
        {
            return Player.PlayerName();
        }

        /// <summary>
        /// Passes a room the player starts in 
        /// </summary>
        /// <returns></returns>
        public Room StartingRoom()
        {
            this.startRoom = rand.Next(1, 31);
            this.currentRoom = startRoom;
            PlaceHazards();
            PlaceWumpus();
            return Cave.GetRoom(startRoom);
        }

        /// <summary>
        /// Asks cave for exits in a room and returns the exits with a list
        /// </summary>
        /// <param name="exits"></param>
        /// <returns></returns>
        public int[] Exit(int[] exits)
        {
            return exits;
        }

        /// <summary>
        /// Returns a room the player moved to
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public Room ReturnRoom(Direction d)
        {
            Room r = this.Cave.GetNewRoom(d, currentRoom);
            this.currentRoom = r.GetRoomNum();
            Player.UpdateInventory();
            return r;
        }

        /// <summary>
        /// returns player inventory
        /// </summary>
        /// <returns></returns>
        public int[] GetPlayerInfo()
        {
            return Player.PlayerInventory();
        }

        /// <summary>
        /// Randomly places hazards in a cave
        /// </summary>
        /// <returns></returns>
        public int GetRoomNumber()
        {
            return this.currentRoom;
        }

        /// <summary>
        /// calls the method that purchase arrows
        /// </summary>
        public void PurchaseArrows()
        {
            this.Player.PurchaseArrows();
        }

        /// <summary>
        /// calls the method that subtract a gold coin
        /// </summary>
        public void RemoveCoin()
        {
            this.Player.MinusGoldCoin();
        }

        /// <summary>
        /// receives direction, compare room# of wumpus with room# the arrow shot
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool ArrowShoot(Direction d)
        {
            Room r = this.Cave.GetNewRoom(d, currentRoom);
            int nextRoom = r.GetRoomNum();
            Player.ShootArrows();
            return nextRoom == wumpus;      
        }

        /// <summary>
        /// Randomly places hazards in a cave
        /// </summary>
        /// <returns></returns>
        private void PlaceHazards()
        {
            this.pit1 = rand.Next(1, 31);
            while (this.pit1 == this.currentRoom)
                this.pit1 = rand.Next(1, 31);

            this.pit2 = rand.Next(1, 31);
            while (this.pit2 == this.currentRoom || this.pit2 == this.pit1)
                this.pit2 = rand.Next(1, 31);

            this.bat1 = rand.Next(1, 31);
            while (this.bat1 == this.currentRoom || this.bat1 == this.pit1 || this.bat1 == this.pit2)
                this.bat1 = rand.Next(1, 31);

            this.bat2 = rand.Next(1, 31);
            while (this.bat2 == this.currentRoom || this.bat2 == this.pit1 || this.bat2 == this.pit2 || this.bat2 == this.bat1)
                this.bat2 = rand.Next(1, 31);
        }

        /// <summary>
        /// return the room numbers the bats are in
        /// </summary>
        /// <returns></returns>
        public int[] BatRoom()
        {
            int[] Bats = new int[2];
            Bats[0] = this.bat1;
            Bats[1] = this.bat2;
            return Bats;
        }

        /// <summary>
        /// return the room numbers the pits are in
        /// </summary>
        /// <returns></returns>
        public int[] PitRoom()
        {
            int[] Pits = new int[2];
            Pits[0] = this.pit1;
            Pits[1] = this.pit2;
            return Pits;
        }

        /// <summary>
        /// Randomly places the wumpus
        /// </summary>
        private void PlaceWumpus()
        {
            this.wumpus = rand.Next(1, 31);
            while (this.wumpus == this.currentRoom)
            {
                this.wumpus = rand.Next(1, 31);
            }
        }

        /// <summary>
        /// moves the wumpus
        /// </summary>
        public void MoveWumpus()
        {
            Room r = Cave.GetRoom(this.wumpus);
            List<Direction> Exits = new List<Direction>();
            for (Direction d = Direction.N; d <= Direction.NW; d++)
            {
                if (r.IsThereAnExit(d))
                    Exits.Add(d); 
            }
            int name = rand.Next(0, Exits.Count);
            this.wumpus = r.WhereExitLeads(Exits[name]);
        }

        /// <summary>
        /// moves the wumpus 2-4 times after the arrow is shot
        /// </summary>
        public void MoveWumpusAgain()
        {
            int move = rand.Next(2, 5);
            for (int i = 0; i < move; i++)
            {
                MoveWumpus();
            }
        }

        /// <summary>
        /// return a room the wumpus is in
        /// </summary>
        /// <returns></returns>
        public int WumpusRoom()
        {
            return this.wumpus;
        }

        /// <summary>
        ///  Bats carry the player to a random room
        /// </summary>
        /// <returns></returns>
        public Room BatNewRoom()
        {
            if (this.currentRoom == this.bat1)
                this.bat1 = rand.Next(1, 31);
            else
                this.bat2 = rand.Next(1, 31);
            this.currentRoom = rand.Next(1, 31);
            return Cave.GetRoom(currentRoom);
        }
 
        /// <summary>
        /// returns if there is a pit in the current room
        /// </summary>
        /// <returns></returns>
        public bool Pit()
        {
            return currentRoom == this.pit1 || currentRoom == this.pit2;
        }

        /// <summary>
        /// returns if there is a bat in the current room
        /// </summary>
        /// <returns></returns>
        public bool Bats()
        {
            return currentRoom == this.bat1 || currentRoom == this.bat2;
        }

        /// <summary>
        /// returns if there is a wumpus in the current room
        /// </summary>
        /// <returns></returns>
        public bool Wumpus()
        {
            return currentRoom == this.wumpus;
        }

        /// <summary>
        /// returns a list of warnings consisting of true or false
        /// </summary>
        /// <returns></returns>
        public bool[] Warnings()
        {
            Room r = Cave.GetRoom(this.currentRoom);
            List<int> Exits = new List<int>();
            bool[] warnings = new bool[3];
            for (Direction d = Direction.N; d <= Direction.NW; d++)
            {
                if (r.IsThereAnExit(d))
                    Exits.Add(r.WhereExitLeads(d));
            }
            for (int i = 0; i < Exits.Count; i++)
            {
                if (Exits[i] == this.wumpus)
                    warnings[0] = true;
                if (Exits[i] == this.pit1 || Exits[i] == pit2)
                    warnings[1] = true;
                if (Exits[i] == this.bat1 || Exits[i] == this.bat2)
                    warnings[2] = true;
            }
            return warnings;
        }

        /// <summary>
        /// returns true or false if there is a wumpus within 2 connecting rooms
        /// </summary>
        /// <returns></returns>
        public bool WumpusWithin2()
        {
            Room r = Cave.GetRoom(this.currentRoom);
            List<int> Exits = new List<int>();
            bool warning = false;
            for (Direction d = Direction.N; d <= Direction.NW; d++)
            {
                if (r.IsThereAnExit(d))
                    Exits.Add(r.WhereExitLeads(d));
            }
            for (int i = 0; i < Exits.Count; i++)
            {
                if (Exits[i] == this.wumpus)
                    warning = true;
                else
                {
                    Room r2 = Cave.GetRoom(Exits[i]);
                    List<int> Exits2 = new List<int>();
                    for (Direction d = Direction.N; d <= Direction.NW; d++)
                    {
                        if (r2.IsThereAnExit(d))
                            Exits2.Add(r2.WhereExitLeads(d));
                    }
                    for (int j = 0; j < Exits2.Count; j++)
                    {
                        if (Exits2[j] == this.wumpus)
                            warning = true;
                    }
                }
            }
            return warning;
        }

        /// <summary>
        /// returns the end score
        /// </summary>
        /// <returns></returns>
        public int EndScore()
        {
            return Player.EndScore();
        }

        /// <summary>
        /// Moves the player back to the starting room - added by Kyrie
        /// </summary>
        public void PitMove()
        {
            this.currentRoom = this.startRoom;
        }

        public Room Teleport(string type)
        {
            if (type == "bat")
            {
                return Cave.GetRoom(this.bat1);
            }
            else if (type == "pit")
            {
                return Cave.GetRoom(this.pit1);
            }
            else
            {
                return Cave.GetRoom(this.wumpus);
            }
        }
    }
}
