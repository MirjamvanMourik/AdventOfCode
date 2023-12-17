using AdventOfCode2023.Extras;
using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day2 : IDay
    {
        long IDay.Day => 2;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day2Cubes.Input);
            var rounds = CreateRounds(input);
            return PlayGameAndReturnFirstAnswer(rounds);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day2Cubes.Input);
            var rounds = CreateRounds(input);
            return PlayGameAndReturnSecondAnswer(rounds);
        }

        private static long PlayGameAndReturnFirstAnswer(Dictionary<long, Dictionary<string, long>> rounds)
        {
            long total = 0;

            foreach (var round in rounds)
            {
                if (IsRoundPossible(round.Value))
                {
                    total += round.Key;
                }
            }

            return total;
        }

        private static long PlayGameAndReturnSecondAnswer(Dictionary<long, Dictionary<string, long>> rounds)
        {
            long total = 0;

            foreach (var round in rounds)
            {
                total += GetMaxAmountOfCubes(round.Value);
            }

            return total;
        }

        private static long GetMaxAmountOfCubes(Dictionary<string, long> round)
        {
            long multipliedNumber = 1;

            foreach (var grab in round)
            {
                multipliedNumber *= grab.Value;
            }

            return multipliedNumber;
        }

        private static bool IsRoundPossible(Dictionary<string, long> round)
        {
            foreach (var grab in round)
            {
                if (grab.Value > Cubes[grab.Key])
                {
                    return false;
                }
            }

            return true;
        }

        private static Dictionary<long, Dictionary<string, long>> CreateRounds(string[] input)
        {
            var rounds = new Dictionary<long, Dictionary<string, long>>();

            foreach (var row in input)
            {
                var splitRoundRow = row.Split(":");
                var roundNumber = long.Parse(splitRoundRow[0].ToLower().Replace("game ", ""));
                var round = splitRoundRow[1].Split(";");

                rounds.Add(roundNumber, new Dictionary<string, long>());

                foreach (var grabs in round)
                {
                    var splitGrabs = grabs.Split(",");

                    foreach (var grab in splitGrabs)
                    {
                        var splitGrab = grab.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                        var amount = long.Parse(splitGrab[0]);
                        var color = splitGrab[1];

                        if (rounds[roundNumber].ContainsKey(color))
                        {
                            if (rounds[roundNumber][color] < amount)
                            {
                                rounds[roundNumber][color] = amount;
                            }
                        } else
                        {
                            rounds[roundNumber].Add(color, amount);
                        }
                    }
                }
            }

            //foreach(var round in rounds)
            //{
            //    Console.WriteLine($"Round {round.Key}");

            //    foreach(var color in round.Value)
            //    {
            //        Console.WriteLine($"{color.Key}:{color.Value}");
            //    }
            //    Console.WriteLine();
            //}

            return rounds;
        }

        private static readonly Dictionary<string, long> Cubes = new()
        {
            {"red", 12 },
            {"green", 13 },
            {"blue", 14 }
        };
    }
}
