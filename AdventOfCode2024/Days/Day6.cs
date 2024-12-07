using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days
{
    public class Day6 : IDay
    {
        long IDay.Day => 6;
        string IDay.Title => "Guard Gallivant";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day6GuardMap.Input);

            var grid = new Day6Grid();
            grid.CreateGrid(input);
            grid.WalkThePath();

            return grid.CountVisitedLocations();
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day6GuardMap.Example);

            var grid = new Day6Grid();
            grid.CreateGrid(input);
            return grid.LookForPossibleObstacles();
        }
    }
}
