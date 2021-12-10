using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WumpusEngine
{
    class Cave
    {
        private int roomCount = 30;
        private Room[] rooms;

        /// <summary>
        /// Constructor
        /// </summary>
        public Cave(int layoutNum)
        {
           // which rooms connect 
            rooms = new Room[roomCount];
            ReadFile("Layout" + layoutNum);
        }

   
        /// <summary>
        /// Reads the file
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadFile(string fileName)
        {
            // open our file
            int count = 0;
            string layoutText = (string)CaveFiles.ResourceManager.GetObject(fileName); 
            StringReader sr = new StringReader(layoutText);
            string line; 
            while ((line = sr.ReadLine()) != null)
            {
                // split each line in the file
                string[] tokens = line.Split();
                Direction[] d = new Direction[tokens.Length / 2];
                int[] exits = new int[tokens.Length / 2];
                // direction array
                for (int i = 0; i < tokens.Length / 2; i++)
                {
                    d[i] = (Direction)int.Parse(tokens[i + tokens.Length / 2]);
                    exits[i] = int.Parse(tokens[i]);
                }

                // create a room
                this.rooms[count] = new Room(count, exits, d);
                // add it to the array
                count++;
            }
        }

        /// <summary>
        /// Tell how the rooms of the layout connect
        /// </summary>
        public Room GetRoom(int numRoom) 
        {
            return this.rooms[numRoom - 1]; 
        }

        public Room GetNewRoom(Direction d, int roomNum)
        {
            Room r = rooms[roomNum - 1];
            int newRoom = r.WhereExitLeads(d);
            return rooms[newRoom - 1];
        }
    }


    /// <summary>
    /// Direction assigned to a number
    /// </summary>
    public enum Direction
    {
        N = 0,
        NE,
        SE,
        S,
        SW,
        NW
    }
}