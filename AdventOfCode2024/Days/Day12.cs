using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days
{
    public class Day12 : IDay
    {
        long IDay.Day => 12;
        string IDay.Title => "Garden Groups";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day12Map.SmallerExample);
            var garden = new Day12Garden();
            garden.CreateGarden(input);
            return garden.CalculatePrice();
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day12Map.SmallerExample);
            var garden = new Day12Garden();
            garden.CreateGarden(input);
            return garden.CalculateDiscountedPrice();
        }
    }
}
