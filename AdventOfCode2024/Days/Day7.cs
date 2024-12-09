using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Days
{
    public class Day7 : IDay
    {
        long IDay.Day => 7;
        string IDay.Title => "Bridge Repair";

        public long GetFirstAnswer()
        {
            var testEquations = InputSplitter.SplitLinesToTestEquations(Day7TestEquations.Input);

            var calculator = new Day7Calculator();

            return calculator.CaculateAndDoSomeMagic(testEquations!);
        }

        public long GetSecondAnswer()
        {
            var testEquations = InputSplitter.SplitLinesToTestEquations(Day7TestEquations.Input);

            var calculator = new Day7Calculator();

            return calculator.CaculateAndDoSomeMagic(testEquations!, true);
        }
    }
}