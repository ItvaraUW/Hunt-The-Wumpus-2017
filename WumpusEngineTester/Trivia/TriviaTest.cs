using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WumpusEngine;

namespace WumpusEngineTester
{
    [TestClass]
    public class TriviaTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Random rand = new NotRandom(1, 5, 13, 25);
            Trivia test = new Trivia(rand);
            test.NewQuestion();
            Assert.AreEqual("What is a group of crows called?", test.GetQuestion());
            test.NewQuestion();
            Assert.AreEqual("Which country only uses one time zone?", test.GetQuestion());
            test.NewQuestion();
            Assert.AreEqual("Mercury and what other element are liquid at room temperature?", test.GetQuestion());
            test.NewQuestion();
            Assert.AreEqual("Who is the only US President to serve more than two terms?", test.GetQuestion());
        }

        // NOTE:
        //
        // Initial implementation of tests may be limited to simply calling
        // methods to ensure they can be called without causing an error.
        // To improve on this, the code can use the provided NotRandom class
        // that will return predictable values, so that the data returned
        // from Trivia Manager methods can be compared to expected results.

        // NOTE:
        //
        // Expected design for Trivia Manager (and other features that have
        // randomized behavior) is that the constructor will take as one of
        // the parameters a Random instance. This instance passed to the
        // constructor should be saved in a non-static field for the object,
        // so it can be used later to generate random numbers.

        // Use this.manager for each test method
        // TODO: uncomment line below once you've added this file to your project
        private Trivia manager;

        // Common code that will run before each and every test
        [TestInitialize]
        public void Initialize()
        {
            // This Random instance will serve as the Random object provided.
            // See note above, regarding using a custom subclass of Random here
            // to generate predictable random numbers.
            NotRandom random = new NotRandom(0, 1, 2, 3, 5);

            // A new Trivia Manager object is created, with a fresh Random, for each test.
            // TODO: uncomment line below once you've added this file to your project
            this.manager = new Trivia(random);
        }

        [TestMethod]
        public void Hint()
        {
            // Basic: add code here to get a hint from "manager".
            // Extended: verify result matches expected hint based on custom Random object
            Assert.AreEqual("Think critically when it comes to birds, it may help you later", manager.GetHint());
        }

        [TestMethod]
        public void WumpusChallenge()
        {
            // Basic: start a "best 3 out of 5" challenge
            manager.Wumpus(true);

            // NOTE:
            //
            // There are two ways to approach the trivia challenge:
            //     * Require user to answer all five questions
            //     * End challenge early if the user gets three questions
            //       correct before being asked all five
            // Adjust the logic below to account for the design choice you made
            // with respect to the above. I.e. if using the second option, track
            // the number of correct answers, and once three are made, verify that
            // the challenge is correctly terminated with a success value.

            // NOTE:
            //
            // In the below, there are three steps for each question. The second
            // and third step may or may not be combined into a single step,
            // depending on the actual design of your Trivia Manager objects.
            // i.e. if the method to submit an answer returns the correct/incorrect
            // status, those two steps are really just one, a single method call.



            // Basic: get the first question of the challenge
            // Basic: submit an answer to the first question
            // Basic: get answer result (correct/incorrect)

            manager.NewQuestion();
            Assert.AreEqual("What is the average airspeed velocity of an unladen swallow?", manager.GetQuestion());
            Assert.AreEqual(true, manager.CheckAnswer(1));
            Assert.AreEqual(true, manager.MatchInProgress());
            
            // Basic: get the second question of the challenge
            // Basic: submit an answer to the second question
            // Basic: get answer result (correct/incorrect)

            manager.NewQuestion();
            Assert.AreEqual("How many hearts does an octopus have?", manager.GetQuestion());
            Assert.AreEqual(false, manager.CheckAnswer(1));
            Assert.AreEqual(true, manager.MatchInProgress());

            // Basic: get the third question of the challenge
            // Basic: submit an answer to the third question
            // Basic: get answer result (correct/incorrect)

            manager.NewQuestion();
            Assert.AreEqual("What is the name of Aladdin's monkey in the movie \"Aladdin\"?", manager.GetQuestion());
            Assert.AreEqual(true, manager.CheckAnswer(1));
            Assert.AreEqual(true, manager.MatchInProgress());

            // Basic: get the fourth question of the challenge
            // Basic: submit an answer to the fourth question
            // Basic: get answer result (correct/incorrect)

            manager.NewQuestion();
            Assert.AreEqual("Which country only uses one time zone?", manager.GetQuestion());
            Assert.AreEqual(false, manager.CheckAnswer(0));
            Assert.AreEqual(true, manager.MatchInProgress());

            // Basic: get the fifth question of the challenge
            // Basic: submit an answer to the fifth question
            // Basic: get answer result (correct/incorrect)

            manager.NewQuestion();
            Assert.AreEqual("In which country is the Giza Plateau?", manager.GetQuestion());
            Assert.AreEqual(true, manager.CheckAnswer(1));
            Assert.AreEqual(false, manager.MatchInProgress());

            // Basic: get challenge result

            Assert.AreEqual(true, manager.MatchWon());

            // Extended:
            // 
            // In all of the above, verify result matches expected
            // correct/incorrect status based on custom Random object (which
            // affects the order questions are given) and the answer submitted.
            //
            // Likewise, verify challenge result matches expected status,
            // based on number of correct/incorrect submitted answers.
        }

        [TestMethod]
        public void OtherChallenge()
        {
            // Basic: start a "best 2 out of 3" challenge

            // NOTE: the rest of this is just like for the WumpusChallenge() method,
            // except there are only three questions instead of five.

            manager.Wumpus(false);

            manager.NewQuestion();
            Assert.AreEqual("What is the average airspeed velocity of an unladen swallow?", manager.GetQuestion());
            Assert.AreEqual(false, manager.CheckAnswer(0));
            Assert.AreEqual(true, manager.MatchInProgress());

            manager.NewQuestion();
            Assert.AreEqual("How many hearts does an octopus have?", manager.GetQuestion());
            Assert.AreEqual(false, manager.CheckAnswer(0));
            Assert.AreEqual(false, manager.MatchInProgress());

            // Basic: get challenge result

            Assert.AreEqual(false, manager.MatchWon());
        }

        [TestMethod]
        public void TestRemoveQuestion()
        {
            NotRandom rando = new NotRandom(0);
            Trivia test = new Trivia(rando);
            test.NewQuestion();
            Assert.AreEqual("What is the average airspeed velocity of an unladen swallow?", test.GetQuestion());
            test.NewQuestion();
            Assert.AreEqual("What is a group of crows called?", test.GetQuestion());
        }
    }
}
