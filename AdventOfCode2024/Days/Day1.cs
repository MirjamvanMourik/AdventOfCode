using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    public class Day1 : IDay
    {
        long IDay.Day => 1;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.Split(Day1LocationIds.Input);

            var totalAmount = 0L;

            var leftList = input[0];
            var rightList = input[1];

            leftList.Sort();
            rightList.Sort();

            for (int i = 0; i < leftList.Count; i++)
            {
                totalAmount += Math.Abs(leftList[i] - rightList[i]);
            }

            return totalAmount;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.Split(Day1LocationIds.Input);

            var totalAmount = 0L;

            var leftList = input[0];
            var rightList = input[1];

            for (int i = 0; i < leftList.Count; i++)
            {
                var occurrences = rightList.Count(x => x == leftList[i]);
                totalAmount += (leftList[i] * occurrences);
            }

            return totalAmount;
        }
    }
}
