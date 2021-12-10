using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace WumpusEngine
{
    public class Score
    {
        // these are the variables needed to create the score board
        private string pN;

        public string PlayerName
        {
            get { return pN; }
            private set { pN = value; }
        }

        private string cN;

        public string CaveName
        {
            get { return cN; }
            private set { cN = value; }
        }

        private int pS;

        public int PlayerScore
        {
            get { return pS; }
            private set { pS = value; }
        }

        private int nT;

        public int NumTurns
        {
            get { return nT; }
            private set { nT = value; }
        }

        private int nA;

        public int NumArrows
        {
            get { return nA; }
            private set { nA = value; }
        }

        private int nG;

        public int NumGold
        {
            get { return nG; }
            private set { nG = value; }
        }

        ///// <summary>
        ///// Accessor method for playerName
        ///// </summary>
        ///// <returns></returns>
        //public string GetPlayerName()
        //{
        //    return this.playerName;
        //}

        ///// <summary>
        ///// Accessor method for caveName
        ///// </summary>
        ///// <returns></returns>
        //public string GetCaveName()
        //{
        //    return this.caveName;
        //}

        ///// <summary>
        ///// Accessor method for score
        ///// </summary>
        ///// <returns></returns>
        //public int GetScore()
        //{
        //    return this.PlayerScore;
        //}

        ///// <summary>
        ///// Accessor method for numArrows
        ///// </summary>
        ///// <returns></returns>
        //public int GetNumArrows()
        //{
        //    return this.numArrows;
        //}

        ///// <summary>
        ///// Accessor method for numTurns
        ///// </summary>
        ///// <returns></returns>
        //public int GetNumTurns()
        //{
        //    return this.numTurns;
        //}

        ///// <summary>
        ///// Accessor method for numGold
        ///// </summary>
        ///// <returns></returns>
        //public int GetNumGold()
        //{
        //    return this.numGold;
        //}

        // unnecessary accessor methods due to the use of accessor methods in field declarations

        /// <summary>
        /// This is the constructor method for Score
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="caveName"></param>
        /// <param name="score"></param>
        /// <param name="numTurns"></param>
        /// <param name="numArrows"></param>
        /// <param name="numGold"></param>
        public Score(string playerName, string caveName, int score, int numTurns, int numArrows, int numGold)
        {
            PlayerName = playerName;

            CaveName = caveName;

            PlayerScore = score;

            NumTurns = numTurns;

            NumArrows = numArrows;

            NumGold = numGold;
        }

        public override string ToString()
        {
            return "Player: " + PlayerName + " Cave: " + CaveName + " Score: " + PlayerScore + " Turns Taken: " + NumTurns + " Remaining Arrows: " + NumArrows + " Coin: " + NumGold;
        }
    }
}

