using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace WumpusEngine
{
    class High_Score
    {
        private string fileName;

        public High_Score()
        {
            //make fileName equal to whatever file is going to be
            fileName = "ScoreFile.txt";
        }

        /// <summary>
        /// This method will get the new score from the last completed game
        /// </summary>
        /// <param name="turns">The number of turns it took to finish game</param>
        /// <param name="gold">The number of gold the player has at the end</param>
        /// <param name="playerName">The name the player entered</param>
        /// <param name="score">The final score of the game session</param>
        /// <param name="arrow">Number of arrows at the end of session</param>
        /// <param name="caveName">Name of the cave that game session is in</param>
        public void AddNewScore(string playerName, string caveName, int score, int turns, int arrow, int gold)
        {
            //make new score
            Score board = new Score(playerName, caveName, score, turns, arrow, gold);
            //added bool so the StreamWriter will overwrite the current file
            using (StreamWriter line = new StreamWriter(fileName, false))
            {
                // create scores array, add new score if necessary, and then write to file
                Score[] scores = ReturnHighScores();
                for (int i = 0; i < scores.Length; i++)
                {
                    if (board.PlayerScore >= scores[i].PlayerScore)
                    {
                        for (int x = scores.Length - 1; x >= i; x--)
                        {
                            scores[x] = scores[x - 1];

                            if (x == i)
                                scores[x] = board;
                        }
                        break;
                    }
                }
                for (int i = 0; i < scores.Length; i++)
                    line.WriteLine(scores[i]);
            }
        }

        /// <summary>
        /// This method will return an array back to game control for shenanagins 
        /// </summary>
        /// <returns></returns>
        public Score[] ReturnHighScores()
        {
            Score[] scores = new Score[10];

            StreamReader stream = new StreamReader(fileName);

            for (int x = 0; x < 10; x++)
            {
                string s = stream.ReadLine();

                string[] y = s.Split(' ');

                scores[x] = new Score(y[0], y[1],
                    int.Parse(y[2]), int.Parse(y[3]),
                    int.Parse(y[4]), int.Parse(y[5]));
            }
            return scores;
        }
    }
}
