using System;
using System.Linq;
using BowlingGame.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingGame
{
    public class Game
    {
        private int currentFrameIndex;
        private Frame[] Frames { get; set; }
        private bool previousWasSpare = false;

        public Game()
        {
            FramesInitialization();
        }

        private void FramesInitialization()
        {
            Frames = new Frame[10];
            for (var i = 0; i < Frames.Length; i++)
            {
                Frames[i] = new Frame();
            }
        }

        public void Roll(int pins)
        {
            if (pins<=10)
            {
                VerifyFrameIsFull();
                Frames[currentFrameIndex].AddRoll(pins);
            }
            else
            {
                throw new ArgumentException("Can't roll more than 10");
            }
        }

        private void VerifyFrameIsFull()
        {
            if (Frames[currentFrameIndex].IsComplete)
            {
                currentFrameIndex++;
            }
        }

        public int GetScore()
        {
            return Frames.Sum(frame => frame.Score);
        }

        public int GetFrameScore(int indexFrame)
        {
            if (indexFrame >= Frames.Length)
                throw new IndexOutOfRangeException("Can't have more than 10 frames");
            return Frames[indexFrame].Score;
        }

        public int GetCurrentFrameIndex()
        {
            return currentFrameIndex;
        }



    }

    [TestFixture]
    public class Game_should : ReportingTest<Game_should>
    {
        private Game game;
        private int score;

        [SetUp]
        public void SetUp()
        {
            game = new Game();
        }

        [Test]
        public void HaveZeroScore_BeforeAnyRolls()
        {
            game
                .GetScore()
                .Should().Be(0);
        }

        [Test]
        public void HaveScore_AfterRoll()
        {
            game.Roll(6);
            game
                .GetScore()
                .Should()
                .Be(6);
        }

        [Test]
        public void FrameHasZeroScore()
        {
 
            game
                .GetFrameScore(0)
                .Should().Be(0);
        }

        [Test]
        public void CantHaveMoreThan10Frames()
        {
            var ex = Assert.Throws<IndexOutOfRangeException>(() => game.GetFrameScore(11));
            Assert.AreEqual(ex.Message, "Can't have more than 10 frames");
        }

        [Test]
        public void CantRollMoreThan10()
        {
            var ex = Assert.Throws<ArgumentException>(() => game.Roll(11));
            Assert.AreEqual(ex.Message, "Can't roll more than 10");
        }

        [Test]
        public void FrameScore10()
        {
            game.Roll(10);
            game
                .GetFrameScore(0)
                .Should().Be(10);
        }

        [Test]
        public void ChangesCurrentFrameIndex()
        {
            game.Roll(3);
            game.Roll(5);

            game.Roll(4);

            game.GetCurrentFrameIndex().Should().Be(1);
        }

        [Test]
        public void SpareBonus()
        {
            game.Roll(5);
            game.Roll(5);
            game.Roll(3);

            game.GetFrameScore(0)
                .Should()
                .Be(13);
        }

        
    }
}
