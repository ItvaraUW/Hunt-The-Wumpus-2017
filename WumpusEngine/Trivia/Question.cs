using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WumpusEngine
{
    class Question
    {
        private List<string> questions;
        private List<string> failedQs;
        private string question;
        private string failedQAns;
        private string[] choices;
        private string answer;
        private int ansNum;
        private Random rand;

        /// <summary>
        /// Contructor for the question.
        /// Takes a reader with the questions.txt file attached and an random
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="rand"></param>
        public Question(StreamReader reader, Random rand)
        {
            // Create a list of all the questions and choices.
            this.questions = new List<string>();
            while (!reader.EndOfStream)
                this.questions.Add(reader.ReadLine());
            reader.Close();

            this.failedQs = new List<string>();
            this.question = "";
            this.choices = new string[3];
            this.answer = "";
            this.ansNum = 0;
            this.rand = rand;
        }

        /// <summary>
        /// Loads a new question, reinitializing all fields (question, choices, answer, etc.) for the new question.
        /// </summary>
        internal void LoadNewQuestion()
        {
            int qNum = this.rand.Next((this.questions.Count - 1) / 5) * 5;
            this.question = questions[qNum]; // First line is the question. Looks like: "What is the thing?"

            for (int i = 0; i < 3; i++) // Next three lines are the choices.
            {
                this.choices[i] = this.questions[(qNum) + i + 1];
                if (this.choices[i].StartsWith("*")) // Correct answer is marked by *
                {
                    this.ansNum = i; // Mark this index in the array as the correct answer
                    this.choices[i] = this.choices[i].Replace("*", "");
                    this.answer = this.choices[i];
                }
            }
            // Delete the question and choices from the list.
            for (int i = 0; i < 5; i++)
                this.questions.RemoveAt(qNum);
        }

        /// <summary>
        /// Gets the question and returns it as a string.
        /// </summary>
        /// <returns> question </returns>
        internal string GetQuestion()
        {
            return this.question;
        }

        /// <summary>
        /// Returns and array of strings of the answer choices.
        /// </summary>
        /// <returns> array of string </returns>
        internal string[] GetChoices()
        {
            return this.choices;
        }

        /// <summary>
        /// Gets the int corresponding to the correct answer.
        /// </summary>
        /// <returns> number of the correct answer (a, b, c, etc.) --> (0, 1, 2, etc.) </returns>
        internal int GetAnswer()
        {
            return this.ansNum;
        }

        /// <summary>
        /// Adds the current question and answer to a list of
        /// questions that were answered incorrectly
        /// (Only called if the answer was incorrect)
        /// </summary>
        internal void AddFailedQuestion()
        {
            this.failedQs.Add(this.question + " " + this.answer);
        }

        /// <summary>
        /// Lets Game control make sure there is at least one failed question
        /// so that they can factor that into the selection of the secret.
        /// </summary>
        /// <returns></returns>
        internal bool FailedQuestion()
        {
            return (!(failedQs.Count == 0));
        }

        /// <summary>
        /// Returns a random question + answer from the list of
        /// failed questions
        /// </summary>
        /// <returns></returns>
        internal string GetFailedQuestion()
        {
            int qNum = this.rand.Next(this.failedQs.Count - 1);
            this.failedQAns = failedQs[qNum];
            failedQs.RemoveAt(qNum);
            return this.failedQAns;
        }

        /// <summary>
        /// Gives a hint for a random trivia question
        /// for when the player enters a new room.
        /// </summary>
        /// <returns></returns>
        internal string GetHint()
        {
            return this.questions[(this.rand.Next((this.questions.Count - 1) / 5) * 5) + 4];
        }
    }
}
