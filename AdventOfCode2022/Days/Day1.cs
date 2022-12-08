using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day1
    {
        public static int GetHighestAmountOfCaloriesForOneElf()
        {
            var calories = Day1Calories.Input;

            var splitCalories = Splitter.SplitInput(calories);

            var amountPerElf = GetAmountPerElf(splitCalories);

            return amountPerElf.Max();
        }

        public static int GetTotalAmountOfCaloriesForTopThree()
        {
            var calories = Day1Calories.Input;

            var splitCalories = Splitter.SplitInput(calories);

            var amountPerElf = GetAmountPerElf(splitCalories);

            var orderedCalories = amountPerElf.OrderByDescending(x => x).ToList();

            return orderedCalories[0] + orderedCalories[1] + orderedCalories[2];
        }

        private static List<int> GetAmountPerElf(string[] splitCalories)
        {
            var amountPerElf = new List<int>();

            foreach (var input in splitCalories)
            {
                var totalForElf = GetTotal(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
                amountPerElf.Add(totalForElf);
            }

            return amountPerElf;
        }

        private static int GetTotal(string[] caloriesForElf)
        {
            var total = 0;

            foreach (var calories in caloriesForElf)
            {
                var numberOfCalories = int.Parse(calories);
                total += numberOfCalories;
            }

            return total;
        }
    }
}
