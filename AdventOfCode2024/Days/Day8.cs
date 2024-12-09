using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days
{
    public class Day8 : IDay
    {
        long IDay.Day => 8;
        string IDay.Title => "Resonant Collinearity";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitIntoRows(Day8AntennaMap.Input);
            var frequencies = GetFrequenciesFromInput(input);

            return CreateAndCountAntiNodes(frequencies, input.Length - 1, input[0].Length - 1);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitIntoRows(Day8AntennaMap.Input);
            var frequencies = GetFrequenciesFromInput(input);

            return CreateAndCountAntiNodes(frequencies, input.Length - 1, input[0].Length - 1, true);
        }

        private static List<Day8Frequency> GetFrequenciesFromInput(string[] input)
        {
            return input.SelectMany((line, y) =>
                line.Select((ch, x) => new { ch, x })
                    .Where(item => item.ch != '.')
                    .Select(item => new Day8Frequency(item.x, y, item.ch.ToString())))
                .ToList();
        }

        private static long CreateAndCountAntiNodes(List<Day8Frequency> frequencies, long maxY, long maxX, bool isSecondPart = false, bool isTest = false)
        {
            var frequencyValueList = frequencies.GroupBy(f => f.frequencyValue)
                                                 .ToDictionary(g => g.Key, g => g.ToList());

            var possibleCombinations = CreatePossibleCombinations(frequencyValueList);
            var antiNodeLocations = new HashSet<(long x, long y)>();

            foreach (var combination in possibleCombinations)
            {
                var yDif = combination[0].Y - combination[1].Y;
                var xDif = combination[0].X - combination[1].X;

                AddAndTraverseAntiNodes(antiNodeLocations, combination[0].X + xDif, combination[0].Y + yDif, xDif, yDif, maxX, maxY, isSecondPart);
                AddAndTraverseAntiNodes(antiNodeLocations, combination[1].X - xDif, combination[1].Y - yDif, -xDif, -yDif, maxX, maxY, isSecondPart);
            }

            if (isSecondPart)
            {
                foreach (var frequency in frequencies)
                {
                    AddAntiNode(antiNodeLocations, frequency.X, frequency.Y, maxX, maxY);
                }
            }

            if (isTest)
            {
                PrintMap(frequencies, antiNodeLocations, maxX + 1, maxY + 1);
            }

            return antiNodeLocations.Count;
        }

        private static void AddAndTraverseAntiNodes(
            HashSet<(long x, long y)> antiNodeLocations,
            long startX,
            long startY,
            long xStep,
            long yStep,
            long maxX,
            long maxY,
            bool traverse)
        {
            while (startX >= 0 && startX <= maxX && startY >= 0 && startY <= maxY)
            {
                AddAntiNode(antiNodeLocations, startX, startY, maxX, maxY);
                if (!traverse) break;
                startX += xStep;
                startY += yStep;
            }
        }

        private static void AddAntiNode(HashSet<(long x, long y)> antiNodeLocations, long x, long y, long maxX, long maxY)
        {
            if (x >= 0 && x <= maxX && y >= 0 && y <= maxY)
            {
                if (!antiNodeLocations.Contains((x, y)))
                {
                    antiNodeLocations.Add((x, y));
                }
            }
        }

        private static void PrintMap(List<Day8Frequency> frequencies, HashSet<(long x, long y)> antiNodeLocations, long maxX, long maxY)
        {
            var frequencyLookup = frequencies.ToDictionary(f => (f.X, f.Y), f => f.frequencyValue);

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    var character = ".";

                    if (frequencyLookup.TryGetValue((x, y), out var value))
                    {
                        character = value;
                    }

                    else if (antiNodeLocations.Contains((x, y)))
                    {
                        character = "#";
                    }

                    Console.Write(character);
                }
                Console.WriteLine();
            }
        }

        private static List<List<Day8Frequency>> CreatePossibleCombinations(Dictionary<string, List<Day8Frequency>> frequencyValueList)
        {
            var result = new List<List<Day8Frequency>>();

            foreach (var group in frequencyValueList.Values)
            {
                for (var i = 0; i < group.Count; i++)
                {
                    for (var j = i + 1; j < group.Count; j++)
                    {
                        result.Add(new List<Day8Frequency> { group[i], group[j] });
                    }
                }
            }

            return result;
        }
    }
}
