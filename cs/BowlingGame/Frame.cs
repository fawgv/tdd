using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGame
{
    public class Frame
    {
        public bool IsComplete { get; private set; }
        public int Score { get; private set; }

        private int rollCount;

        public Frame()
        {

        }

        public void AddRoll(int pins, bool endFrameGame = false)
        {
            Score += pins;
            rollCount++;
            if (!endFrameGame)
            {
                IsComplete = rollCount == 2 ? true : false;
            }
            else
            {
                IsComplete = rollCount == 3 ? true : false;
            }
        }
    }
}
