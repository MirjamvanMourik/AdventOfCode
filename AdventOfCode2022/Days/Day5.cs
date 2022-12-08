using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022.Days
{
    public class Day5
    {
        private static Dictionary<int, List<string>> cratesInStacks = new();

        public static string GetCratesOnTopOfEachStackAfterRearranging(bool withUpdate)
        {
            var cratesAndCommands = Day5Crates.Input;
            
            var splitCratesAndCommands = cratesAndCommands.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.None);

            var crates = splitCratesAndCommands[0];
            var commands = Splitter.SplitInput(splitCratesAndCommands[1]);

            cratesInStacks = PutCratesInStacks(crates);

            PrepareAndExecuteCommandsOnStacks(commands, withUpdate);

            return GetTopCrates();
        }

        private static string GetTopCrates()
        {
            var result = string.Empty;

            foreach (var crate in cratesInStacks.Values)
            {
                result += crate[^1];
            }

            return result;
        }

        private static void PrepareAndExecuteCommandsOnStacks(string[] commands, bool withUpdate)
        {
            foreach (var command in commands)
            {
                var splitCommand = command.Split(" ", StringSplitOptions.None);
                var numbers = new List<int>();

                foreach (var cmd in splitCommand)
                {
                    if (int.TryParse(cmd, out int number))
                    {
                        numbers.Add(number);
                    }
                }

                if (withUpdate)
                {
                    ExecuteUpdateCommand(numbers);
                }
                else
                {
                    ExecuteCommand(numbers);
                }
            }
        }

        private static void ExecuteUpdateCommand(List<int> numbers)
        {
            var crates = new List<string>();

            for (var i = 0; i < numbers[0]; i++)
            {
                crates.Add(cratesInStacks[numbers[1]][^1]);
                cratesInStacks[numbers[1]].RemoveAt(cratesInStacks[numbers[1]].Count - 1);
            }

            crates.Reverse();

            foreach (var crate in crates)
            {
                cratesInStacks[numbers[2]].Add(crate);
            }
        }

        private static void ExecuteCommand(List<int> numbers)
        {
            for (var i = 0; i < numbers[0]; i++)
            {
                var crate = cratesInStacks[numbers[1]][^1];
                cratesInStacks[numbers[1]].RemoveAt(cratesInStacks[numbers[1]].Count - 1);
                cratesInStacks[numbers[2]].Add(crate);
            }
        }

        private static Dictionary<int, List<string>> PutCratesInStacks(string crates)
        {
            var splitCrates = crates.Split(Environment.NewLine, StringSplitOptions.None);

            var numbers = splitCrates[^1];
            var crateNumbers = GetNumbersFromString(numbers);
            splitCrates = splitCrates.SkipLast(1).ToArray();

            var cratesInStacks = new Dictionary<int, List<string>>();

            foreach (var number in crateNumbers)
            {
                cratesInStacks.Add(number, new List<string>());
            }

            for (var i = splitCrates.Length - 1; i >= 0; i--)
            {
                var lastRow = GetRowAsList(splitCrates[i], crateNumbers.Count);

                for (var j = 0; j < lastRow.Count; j++)
                {
                    if (!string.IsNullOrEmpty(lastRow[j]))
                    {
                        cratesInStacks[j + 1].Add(lastRow[j]);
                    }
                }
            }

            return cratesInStacks;
        }

        private static List<int> GetNumbersFromString(string input)
        {
            var charNumbers = input.Where(char.IsDigit).ToArray();
            var numbers = new List<int>();

            foreach (var number in charNumbers)
            {
                numbers.Add((int)char.GetNumericValue(number));
            }

            return numbers;
        }

        private static List<string> GetRowAsList(string input, int amountOfStacks)
        {
            var row = new List<string>();
            var lengthPerStack = (input.Length + 1) / amountOfStacks;
            var index = 0;
            var item = string.Empty;

            for (var i = 0; i < amountOfStacks; i++)
            {
                if (index + 1 + lengthPerStack > input.Length)
                {
                    item = input[index..];
                }
                else 
                {

                    item = input.Substring(index, lengthPerStack);
                }

                if (string.IsNullOrWhiteSpace(item))
                {
                    row.Add(string.Empty);
                    index += lengthPerStack;
                    continue;
                }

                var letter = item.Where(char.IsLetter).ToList()[0].ToString();
                row.Add(letter);
                index += lengthPerStack;
            }

            return row;
        }
    }
}
