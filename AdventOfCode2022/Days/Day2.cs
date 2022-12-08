using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day2
    {
        public static int GetTotalScoreWithStrategyGuide(bool differently)
        {
            var totalScore = 0;

            var rounds = Day2StrategyGuide.Input;

            var splitRounds = Splitter.SplitInput(rounds);

            foreach (var round in splitRounds)
            {
                if (differently)
                {
                    totalScore += PlayRoundDifferently(round);
                }
                else
                {
                    totalScore += PlayRound(round);
                }
            }

            return totalScore;
        }

        // [Opponent] A = Rock, B = Paper, C = Scissors - [You] X = Rock, Y = Paper, Z = Scissors
        private static int PlayRound(string round)
        {
            var splitRound = round.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (splitRound[0])
            {
                case "A":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 4;
                        case "Y":
                            return 8;
                        case "Z":
                            return 3;
                    }
                    break;

                case "B":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 1;
                        case "Y":
                            return 5;
                        case "Z":
                            return 9;
                    }
                    break;

                case "C":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 7;
                        case "Y":
                            return 2;
                        case "Z":
                            return 6;
                    }
                    break;
            }

            return 0;
        }

        // [Opponent] A = Rock, B = Paper, C = Scissors - [You] X = Lose, Y = Draw, Z = Win
        private static int PlayRoundDifferently(string round)
        {
            var splitRound = round.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            switch (splitRound[0])
            {
                case "A":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 3;
                        case "Y":
                            return 4;
                        case "Z":
                            return 8;
                    }
                    break;

                case "B":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 1;
                        case "Y":
                            return 5;
                        case "Z":
                            return 9;
                    }
                    break;

                case "C":
                    switch (splitRound[1])
                    {
                        case "X":
                            return 2;
                        case "Y":
                            return 6;
                        case "Z":
                            return 7;
                    }
                    break;
            }

            return 0;
        }
    }
}