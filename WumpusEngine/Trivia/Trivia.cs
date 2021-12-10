using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WumpusEngine
{
    //public enum TriviaProgress { Unasked, Correct, Incorrect}

    class Trivia
    {
        private int numCorrect;
        private int goal;
        private int chances;
        private int qsAsked;
        private StreamReader reader;
        private Question q;
        private Random rand;
        //private TriviaProgress result;

        /// <summary>
        /// Constructor for Trivia.
        /// Takes a random
        /// </summary>
        /// <param name="rand"></param>
        public Trivia(Random rand)
        {
            this.numCorrect = 0;
            this.goal = 0;
            this.chances = 0;
            this.qsAsked = 0;
            this.reader = new StreamReader("Trivia\\questions.txt");
            this.q = new Question(this.reader, rand);
            this.rand = rand;
            //this.result = TriviaProgress.Unasked;
        }

        /// <summary>
        /// Sets up the new question for trivia match
        /// </summary>
        public void NewQuestion()
        {
            this.qsAsked++;
            this.q.LoadNewQuestion();
        }

        /// <summary>
        /// If the match is over, then return that yes, the match was won (true)
        /// or no, the match was lost (false).
        /// </summary>
        public bool MatchWon()
        {
            return (this.numCorrect == this.goal);
        }

        /// <summary>
        /// Sets the goal and number of chances depending on what
        /// the condition of the trivia match is.
        /// Also marks the start of the match.
        /// </summary>
        /// <param name="wumpus"> whether or not this is a wumpus battle
        /// true - wumpus battle; false - other conflict </param>
        public void Wumpus(bool wumpus)
        {
            qsAsked = 0;

            if (wumpus)
            {
                this.goal = 3;
                this.chances = 5;
            }
            else
            {
                this.goal = 2;
                this.chances = 3;
            }
        }

        /// <summary>
        /// Returns whether the game is finished or in progress.
        /// </summary>
        /// <returns> The game state - true if in progress, false if not </returns>
        public bool MatchInProgress()
        {
            return ((this.numCorrect < this.goal) 
                && ((this.chances - (this.qsAsked - this.numCorrect)) >= this.goal));
        }

        /// <summary>
        /// Gets a question from list of unasked question
        /// and gives a question (that hasn't been asked yet).
        /// </summary>
        /// <returns> question </returns>
        public string GetQuestion()
        {
            return this.q.GetQuestion();
        }

        /// <summary>
        /// Returns the choices of answers as a string
        /// </summary>
        /// <returns> string </returns>
        public string[] GetChoices()
        {
            return this.q.GetChoices();
        }

        /// <summary>
        /// Cheat method - give the index of the correct asnwer
        /// </summary>
        /// <returns> index of the correct answer </returns>
        public int GetCorrectAnswer()
        {
            return this.q.GetAnswer();
        }

        /// <summary>
        /// Returns the hint for a question for when the player enters a new room.
        /// </summary>
        /// <returns></returns>
        public string GetHint()
        {
            return this.q.GetHint();
        }

        /// <summary>
        /// Gives the question and the answer to a question that the player got wrong
        /// </summary>
        /// <returns> Format of: "Question? Answer" </returns>
        public string GetFailedQuestion()
        {
            return this.q.GetFailedQuestion();
        }

        /// <summary>
        /// Trivia will return whether the answer was correct or not 
        /// by comparing the player's answer to the correct answer.
        /// </summary>
        /// <param name="playerAnswer"> Which answer the player chose </param>
        /// <returns> true - player was correct; false - player was incorrect </returns>
        public bool CheckAnswer(int playerAnswer)
        {
            if (playerAnswer == this.q.GetAnswer())
            {
                //this.result = TriviaProgress.Correct;
                this.numCorrect++;
            }
            else
            {
                q.AddFailedQuestion();
                //this.result = TriviaProgress.Incorrect;
            }
            return (playerAnswer == this.q.GetAnswer());
        }
    }
}
