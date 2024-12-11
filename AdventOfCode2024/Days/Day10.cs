using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days
{
    public class Day10 : IDay
    {
        long IDay.Day => 10;
        string IDay.Title => "Hoof It";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day10TopographicMap.Input);

            var map = new Day10Map();
            map.CreateMap(input);
            var totalScore = 0;

            foreach (var point in map.StartingPoints)
            {
                map.FindCompleteTrails(point, 0, map.HighestTrailHeight);
                totalScore += point.EndOfTrailFromTrailhead.Count;
            }

            return totalScore;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day10TopographicMap.Input);

            var map = new Day10Map();
            map.CreateMap(input);

            var totalScore = 0;

            foreach (var point in map.StartingPoints)
            {
                totalScore += map.FindCompleteTrailsPartTwo(point, 0, map.HighestTrailHeight);
            }

            return totalScore;
        }
    }
}
