using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    public class Day4 : IDay
    {
        long IDay.Day => 4;
        string IDay.Title => "Ceres Search";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day4WordGrid.Input);

            var grid = new Day4Grid();
            grid.CreateGrid(input);

            return grid.CalculateOccurences("XMAS");
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitLinesToString(Day4WordGrid.Input);

            var grid = new Day4Grid();
            grid.CreateGrid(input);

            return grid.CalculateXOccurences("MAS");
        }
    }
}
