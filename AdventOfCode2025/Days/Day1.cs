using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day1 : IDay
    {
        long IDay.Day => 1;
        string IDay.Title => "Secret Entrance";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitIntoRows(Day1Rotations.Input);
            var startingPoint = 50;
            var currentPosition = startingPoint;
            var amountOfTimesAtZero = 0;

            foreach (var line in input)
            {
                if (line.StartsWith('L'))
                {
                    currentPosition -= int.Parse(line[1..]);
                }
                else if (line.StartsWith('R'))
                {
                    currentPosition += int.Parse(line[1..]);
                }

                if (currentPosition == 0 || currentPosition % 100 == 0)
                {
                    amountOfTimesAtZero++;
                }
            }

            return amountOfTimesAtZero;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitIntoRows(Day1Rotations.Input);
            var startingPoint = 50;
            var currentPosition = startingPoint;
            var amountOfTimesAtZero = 0;

            foreach (var line in input)
            {
                if (line.StartsWith('L'))
                {
                    for (var i = int.Parse(line[1..]); i > 0; i--)
                    {
                        currentPosition--;

                        if (currentPosition == 0 || currentPosition % 100 == 0)
                        {
                            amountOfTimesAtZero++;
                        }
                    }
                }
                else if (line.StartsWith('R'))
                {
                    for (var i = 0; i < int.Parse(line[1..]); i++)
                    {
                        currentPosition++;
                        if (currentPosition == 0 || currentPosition % 100 == 0)
                        {
                            amountOfTimesAtZero++;
                        }
                    }
                }
            }

            return amountOfTimesAtZero;
        }
    }
}
