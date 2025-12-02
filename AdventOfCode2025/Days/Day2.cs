using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day2 : IDay
    {
        long IDay.Day => 2;
        string IDay.Title => "Gift Shop";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitBasedOnCharacter(Day2ProductIds.Input, ',');

            var invalidIdsSum = 0L;

            foreach (var id in input)
            {
                var range = id.Split('-');

                if (range[0].StartsWith('0') || range[1].StartsWith('0'))
                {
                    continue;
                }

                var first = long.Parse(range[0]);
                var last = long.Parse(range[1]);

                for (var i = first; i <= last; i++)
                {
                    var valueAsString = i.ToString();
                    if (valueAsString.Length % 2 == 0)
                    {
                        var firstHalf = valueAsString.Substring(0, valueAsString.Length / 2);
                        var secondHalf = valueAsString.Substring(valueAsString.Length / 2);
                        if (firstHalf == secondHalf)
                        {
                            //Console.WriteLine($"Invalid ID found: {valueAsString}");
                            invalidIdsSum += long.Parse(valueAsString);
                        }
                    }
                }
            }

            return invalidIdsSum;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitBasedOnCharacter(Day2ProductIds.Example, ',');

            var invalidIdsSum = 0L;

            foreach (var id in input)
            {
                var range = id.Split('-');

                if (range[0].StartsWith('0') || range[1].StartsWith('0'))
                {
                    continue;
                }

                var first = long.Parse(range[0]);
                var last = long.Parse(range[1]);

                for (var i = first; i <= last; i++)
                {
                    var valueAsString = i.ToString();
                    var length = valueAsString.Length;

                    for (var j = 1; j <= length; j++)
                    {
                        if (length % j == 0)
                        {
                            var partLength = length / j;
                            var allPartsEqual = true;
                            var firstPart = valueAsString.Substring(0, partLength);
                            for (var k = 1; k < j; k++)
                            {
                                var nextPart = valueAsString.Substring(k * partLength, partLength);
                                if (nextPart != firstPart)
                                {
                                    allPartsEqual = false;
                                    break;
                                }
                            }
                            if (allPartsEqual)
                            {
                                //Console.WriteLine($"Invalid ID found: {valueAsString}");
                                invalidIdsSum += long.Parse(valueAsString);
                                break;
                            }
                        }
                    }
                }
            }

            return invalidIdsSum;
        }
    }
}
