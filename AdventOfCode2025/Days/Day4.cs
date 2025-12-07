using AdventOfCode2025.Extras;
using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day4 : IDay
    {
        long IDay.Day => 4;
        string IDay.Title => "Printing Department";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day4Diagram.Input);

            var grid = new Day4Grid();
            return grid.FindAmountOfAccessiblePapers(input);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day4Diagram.Input);
            var total = 0L;

            var grid = new Day4Grid();
            grid.CreateGrid(input);

            long currentAmountFound;

            while ((currentAmountFound = grid.FindAmountOfAccessiblePapers(input)) > 0)
            {
                total += currentAmountFound;
            }

            return total;
        }
    }
}
