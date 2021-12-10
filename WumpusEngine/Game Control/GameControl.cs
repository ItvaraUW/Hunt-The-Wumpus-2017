//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WumpusEngine
{
    /// <summary>
    /// Enumerated type GameState
    /// </summary>
    public enum GameState { MainMenu, Roaming, PurchaseArrow, PurchaseSecret, Bats, Pit, Wumpus, GameWin, GameLoss }

    /// <summary>
    /// Connects all the things
    /// </summary>
    public class GameControl
    {
        private static GameControl maintainedInstance;
        private GameState state;
        private Map m;
        private Trivia t;
        private High_Score hs;
        private int purchaseCount;
        private static Random rand = new Random();

        #region Constructor Methods
        /// <summary>
        /// Constructor method; initializes the High Score object
        /// </summary>
        public GameControl()
        {
            this.hs = new High_Score();
            state = GameState.MainMenu;
        }

        /// <summary>
        /// Static GameControl for UI to use
        /// </summary>
        /// <returns>Maintained instance of GameControl</returns>
        public static GameControl GetMaintainedInstance()
        {
            if (maintainedInstance == null)
                maintainedInstance = new GameControl();
            return maintainedInstance;
        }

        /// <summary>
        /// Returns this object's state of the game
        /// </summary>
        /// <returns>A copy of the game's state</returns>
        public GameState GetState()
        {
            // Returns a copy of the game's state
            return this.state;
        }
        #endregion

        #region Methods for Room Updates
        /// <summary>
        /// Given the direction the player moved, gets a new room from Map
        /// </summary>
        /// <returns>The new room</returns>
        public Room GetRoom(Direction d)
        {
            return m.ReturnRoom(d);
        }

        /// <summary>
        /// Performs a check of "things" that could be in the current room:
        ///   - Wumpus
        ///   - Pit
        ///   - Bats
        /// </summary>
        /// <returns>True if state was changed at all (there is something in the room)
        /// False if all checks failed (there is nothing in the room)</returns>
        public bool CheckHazards()
        {
            // Ask map if the wumpus is in the new current room
            if (m.Wumpus())
            {
                // If true, set game state to Wumpus
                this.state = GameState.Wumpus;
            }
            // Else, ask if there is a pit in the room
            else if (m.Pit())
            {
                // If true, set game state to Pit
                this.state = GameState.Pit;
            }
            // Else, ask if there are super bats in the room
            else if (m.Bats())
            {
                // If true, set game state to Bats
                this.state = GameState.Bats;
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns which hazards are nearby or not
        /// </summary>
        /// <returns>An array of bools: wumpus, pits, bats</returns>
        public bool[] GetWarnings()
        {
            return this.m.Warnings();
        }

        /// <summary>
        /// Asks map for the player's stats such as inventory and turn count
        /// </summary>
        /// <returns>An array of ints containing arrow count, coin count, turn count</returns>
        public int[] GetPlayerStats()
        {
            // Askes map for the player's current statistics for UI to display
            return m.GetPlayerInfo();
        }

        /// <summary>
        /// Askes Trivia for a question and correct answer for the player when they enter
        /// a new room
        /// </summary>
        /// <returns>A string containing the question and the answer</returns>
        public string GetTrivia()
        {
            return t.GetHint();
        }

        /// <summary>
        /// If the player entered a room with bats, this method moves them
        /// </summary>
        /// <returns>A new random Room</returns>
        public Room Bats()
        {
            this.state = GameState.Roaming;
            return this.m.BatNewRoom();
        }
        #endregion

        #region Shooting an arrow
        /// <summary>
        /// Given the direction the user shot the arrow, returns whether
        /// the wumpus was hit or not
        /// </summary>
        /// <param name="d">Direction the arrow was shot</param>
        /// <returns>True if wumpus was hit, false if wumpus not hit</returns>
        public bool ShootArrow(Direction d)
        {
            // Ask map if the wumpus was hit knowing the direction the arrow was shot
            if (this.m.ArrowShoot(d))
            {
                this.state = GameState.GameWin;
            }
            else if (this.m.GetPlayerInfo()[0] <= 0)
            {
                this.state = GameState.GameLoss;
            }
            else
            {
                m.MoveWumpus();
            }
            return this.m.ArrowShoot(d);
        }
        #endregion

        #region Trivia Match

        /// <summary>
        /// Starts a Trivia match and tells if it's a wumpus match or not
        /// </summary>
        public void StartMatch()
        {
            this.t.Wumpus(this.state == GameState.Wumpus);
        }

        /// <summary>
        /// Gets a question and answers from trivia to hand UI
        /// </summary>
        /// <returns>The question and multiple choice answers</returns>
        public string GetQuestion()
        {
            // Ask trivia for a question and four multiple choice answers
            this.t.NewQuestion();
            return this.t.GetQuestion();
        }

        /// <summary>
        /// Gets the answer choices from Trivia
        /// </summary>
        /// <returns>an array of answer choices</returns>
        public string[] GetChoices()
        {
            return this.t.GetChoices();
        }
        /// <summary>
        /// Receives the user's answer and asks trivia if it is correct
        /// </summary>
        /// <param name="answer">The user's answer choice</param>
        /// <returns>Whether the answer was correct or not</returns>
        public bool CheckAnswer(int answer)
        {
            // Ask Trivia if the answer is correct
            m.RemoveCoin();
            if (m.GetPlayerInfo()[1] <= 0)
                state = GameState.GameLoss;
            return this.t.CheckAnswer(answer);
        }

        /// <summary>
        /// Askes Trivia if the trivia match is still happening
        /// </summary>
        /// <returns></returns>
        public bool MatchState()
        {
            return this.t.MatchInProgress();
        }

        /// <summary>
        /// When the match is over, askes trivia if the player won or lost
        /// </summary>
        /// <returns>True if player won, false if player lost</returns>
        public bool MatchWin()
        {
            // Askes trivia if the player won the match
            // If true, player won, pass true to UI
            // If false, player lost, pass false to UI
            if (!this.t.MatchWon() && (this.state == GameState.Wumpus || this.state == GameState.Pit))
                this.state = GameState.GameLoss;
            else if (this.state == GameState.Wumpus)
            {
                // Have Map move the wumpus two to four rooms
                this.m.MoveWumpusAgain();
                this.state = GameState.Roaming;
            }
            else if (this.state == GameState.Pit)
            {
                // Have Map move the player back to the starting room
                this.m.PitMove();
                this.state = GameState.Roaming;
            }
            return this.t.MatchWon();
        }
        #endregion

        #region Beginning and Ending Game Methods
        /// <summary>
        /// Askes HighScore for the list of high scores to display on the start menu
        /// </summary>
        /// <returns>A list of high scores with player's name, score, number
        /// of turns, gold coins left, arrows left, and cave played</returns>
        public Score[] GetHighScores()
        {
            return hs.ReturnHighScores();
        }

        /// <summary>
        /// At the start of the game, receive the player's choices and update the appropriate fields
        /// </summary>
        /// <param name="playerName">The player's input for their name</param>
        /// <param name="caveSelection">The cave number the player wishes to use</param>
        public void GameStart(string playerName, int caveSelection)
        {
            // Initialize Map with player name and cave selection
            if (caveSelection < 0 || caveSelection > 5)
                throw new ArgumentException("The Cave selection must be between 1 and 5.");
            this.m = new Map(playerName, caveSelection, rand);
            this.t = new Trivia(rand);
            // Initialize game state to roaming
            state = GameState.Roaming;   
        }

        /// <summary>
        /// Returns the starting room of the game
        /// </summary>
        /// <returns></returns>
        public Room StartingRoom()
        {
            return this.m.StartingRoom();
        }

        /// <summary>
        /// Updates the high score list and passes the player's score info to UI
        /// </summary>
        /// <returns>Player's score break down</returns>
        public int GameOver()
        {
            // Ask Map for the player's name, player's score, player's gold count,
            // player's turn count, player's arrow count, and the cave played in
            // Pass this information to HighScore to update the HighScore list
            int[] inventory = m.GetPlayerInfo();
            string playerName = this.m.PlayerName();
            string caveName = ""; 
            int score = this.m.EndScore();
            int turns = inventory[2];
            int arrows = inventory[0];
            int gold = inventory[1];
            //hs.AddNewScore(playerName, caveName, score, turns, arrows, gold);
            return score;
        }
        #endregion

        #region Purchasing Methods
        /// <summary>
        /// Handles the purchasing of secrets
        /// </summary>
        public string PurchaseSecret()
        {
            // If player was purchasing a secret, randomly select a secret option:
            //    - Room location of a pit #3
            //    - Room location of a bat #4
            //    - Room location of wumpus #1
            //    - If the wumpus is within two rooms #2
            //    - Current room number #6
            //    - Answer to previously incorrectly answered trivia #5
            this.state = GameState.Roaming;
            int chance = rand.Next(1, 100);
            this.purchaseCount++;
            if(chance > 81 + purchaseCount * 2)
            {
                return "You are currently in room " + this.m.GetRoomNumber().ToString();
            }
            else if (chance > 65 + purchaseCount * 2)
            {
                return this.t.GetFailedQuestion();
            }
            else if (chance > 49 + purchaseCount * 2)
            {
                return "There is a bat in room " + this.m.BatRoom()[rand.Next(0, 2)].ToString();
            }
            else if (chance > 33 + purchaseCount * 2)
            {
                return "There is a pit in room " + this.m.PitRoom()[rand.Next(0, 2)].ToString();
            }
            else if ((chance > 17 + purchaseCount * 2) && (this.m.WumpusWithin2()))
            {
                return "There is a Wumpus within two rooms";
            }
            else
            {
                return "The wumpus is in room " + this.m.WumpusRoom().ToString();
            }
        }

        /// <summary>
        /// Ups the arrow count
        /// </summary>
        public void PurchaseArrow()
        {
            this.state = GameState.Roaming;
            m.PurchaseArrows();
        }


        /// <summary>
        /// Changes the game state to purchase
        /// </summary>
        public void StartPurchaseArrow()
        {
            this.state = GameState.PurchaseArrow;
        }

        /// <summary>
        /// Changes the game state to purchase
        /// </summary>
        public void StartPurchaseSecret()
        {
            this.state = GameState.PurchaseSecret;
        }
        #endregion

        #region Cheat Stuff
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CheatInfo()
        {
            return string.Format("Current room: {0}, Bats: {1}, {2}, Pits: {3}, {4} Wumpus: {5}", 
                   this.m.GetRoomNumber(), this.m.BatRoom()[0], this.m.BatRoom()[1], this.m.PitRoom()[0],
                   this.m.PitRoom()[1], this.m.WumpusRoom());
        }

        /// <summary>
        /// Moves the player to a room with the wumpus, a bat, or a pit to show
        /// functionality
        /// </summary>
        /// <param name="roomType">Wumpus, bat, or pit</param>
        /// <returns></returns>
        public Room Teleport(string roomType)
        {
            return this.m.Teleport(roomType);
        }

        /// <summary>
        /// Returns the number of the correct answer
        /// </summary>
        /// <returns>Answer number</returns>
        public int TriviaAnswer()
        {
            return t.GetCorrectAnswer() + 1;
        }
        #endregion
    }
}
