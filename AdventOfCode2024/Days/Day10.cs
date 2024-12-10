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
            var input = InputSplitter.SplitLinesToString(Day10TopographicMap.Example);

            var map = new Day10Map();
            map.CreateMap(input);

            var startingPoints = map.GetAllStartingPoints();

            var total = 0;

            foreach (var point in startingPoints)
            {
                total += map.FindCompleteTrails(point, 1, 9);
            }

            return total;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day10TopographicMap.Example);

            var map = new Day10Map();
            map.CreateMap(input);

            return 0;
        }
    }
}
