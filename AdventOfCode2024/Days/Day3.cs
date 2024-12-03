using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Days
{
    public class Day3 : IDay
    {
        long IDay.Day => 3;
        string IDay.Title => "Mull It Over";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitMemory(Day3Memory.Input, true);

            var totalAmount = 0;

            foreach (var item in input)
            {
                int start = item.IndexOf("(") + 1;
                int end = item.IndexOf(")");

                string numbers = item[start..end];

                var splitNumbers = numbers.Split(',');

                if (splitNumbers.Length == 2)
                {
                    totalAmount += (int.Parse(splitNumbers[0]) * int.Parse(splitNumbers[1]));
                }
            }

            return totalAmount;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitMemory(Day3Memory.Input, false);

            var useInSum = true;

            var totalAmount = 0;

            foreach (var item in input)
            {
                if (item == "don't()")
                {
                    useInSum = false;
                }
                else if (item == "do()")
                {
                    useInSum = true;
                }

                if (useInSum)
                {
                    int start = item.IndexOf("(") + 1;
                    int end = item.IndexOf(")");

                    string numbers = item[start..end];

                    var splitNumbers = numbers.Split(',');

                    if (splitNumbers.Length == 2)
                    {
                        totalAmount += (int.Parse(splitNumbers[0]) * int.Parse(splitNumbers[1]));
                    }
                }
            }

            return totalAmount;
        }
    }
}
