using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Extras
{
    public class Day6ScoreBoard
    {
        public List<Day6Round> rounds = new();
    }

    public class Day6Round
    {
        public long highScore;
        public long maxMiliseconds;
        public List<Day6Score> scores = new();
    }

    public class Day6Score
    {
        public long buttonHoldTime;
        public long milimetersTraveled;

        public Day6Score(long buttonHoldTime, long milimetersTraveled)
        {
            this.buttonHoldTime = buttonHoldTime;
            this.milimetersTraveled = milimetersTraveled;
        }
    }
}
