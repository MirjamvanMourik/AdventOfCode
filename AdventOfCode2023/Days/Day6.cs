using AdventOfCode2023.Extras;
using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day6: IDay
    {
        long IDay.Day => 6;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.Split(Day6BoatRace.Input);
            var scoreBoard = new Day6ScoreBoard();

            for (var i = 0; i < input["time"].Length; i++)
            {
                var round = new Day6Round
                {
                    highScore = long.Parse(input["distance"][i]),
                    maxMiliseconds = long.Parse(input["time"][i])
                };

                PlayRound(round);

                scoreBoard.rounds.Add(round);
            }

            return CalculateResult(scoreBoard);
        }
        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitForOneRace(Day6BoatRace.Input);
            var scoreBoard = new Day6ScoreBoard();

            for (var i = 0; i < input["time"].Count; i++)
            {
                var round = new Day6Round
                {
                    highScore = long.Parse(input["distance"][i]),
                    maxMiliseconds = long.Parse(input["time"][i])
                };

                PlayRound(round);

                scoreBoard.rounds.Add(round);
            }

            return CalculateResult(scoreBoard);
        }

        private static long CalculateResult(Day6ScoreBoard scoreBoard)
        {
            var result = 1;

            for (var i = 0; i < scoreBoard.rounds.Count; i++)
            {
                var amountOfPossibilitiesToWin = 0;

                foreach (var score in scoreBoard.rounds[i].scores)
                {
                    if (score.milimetersTraveled > scoreBoard.rounds[i].highScore)
                    {
                        amountOfPossibilitiesToWin++;
                    }
                }
                result *= amountOfPossibilitiesToWin;
            }

            return result;
        }

        private static void PlayRound(Day6Round round)
        {
            for(var i = 0; i < round.maxMiliseconds; i++)
            {
                var timeButtonPushed = i;
                var timeToTravel = round.maxMiliseconds - timeButtonPushed;
                var distanceTravaled = timeButtonPushed * timeToTravel;

                round.scores.Add(new Day6Score(timeButtonPushed, distanceTravaled));
            }
        }
    }
}
