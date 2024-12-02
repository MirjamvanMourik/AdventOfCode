using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Days
{
    public class Day2 : IDay
    {
        long IDay.Day => 2;
        string IDay.Title => "Red-Nosed Reports";

        private readonly long MaxDifference = 3;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLines(Day2Levels.Input);
            return input.Count(CheckLevelSafety);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLines(Day2Levels.Input);
            long amountOfSafeReports = 0;

            foreach (var levels in input)
            {
                for (int i = 0; i <= levels.Count; i++)
                {
                    if (CheckLevelSafety(RemoveAtIndex(levels, i)))
                    {
                        amountOfSafeReports++;
                        break;
                    }
                }
            }

            return amountOfSafeReports;
        }

        private bool CheckLevelSafety(List<long> levels)
        {
            if (levels.Count < 2) {
                return false;
            }

            bool isIncreasing = true;
            bool isDecreasing = true;

            for (int i = 0; i < levels.Count - 1; i++)
            {
                if (levels[i] < levels[i + 1])
                {
                    isDecreasing = false;
                }

                if (levels[i] > levels[i + 1])
                {
                    isIncreasing = false;
                }

                if (!isIncreasing && !isDecreasing)
                {
                    return false;
                }
            }

            return levels.Zip(levels.Skip(1), (a, b) => Math.Abs(a - b))
                         .All(diff => diff > 0 && diff <= MaxDifference);
        }

        private static List<long> RemoveAtIndex(List<long> levels, int index)
        {
            if (index >= levels.Count) {
                return levels;
            }

            var result = new List<long>(levels);
            result.RemoveAt(index);

            return result;
        }
    }
}
