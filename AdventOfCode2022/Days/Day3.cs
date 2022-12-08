using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day3
    {
        public static int GetPrioSumOfWronglyPackedItems()
        {
            var prioSum = 0;

            var rucksacks = Day3Rucksacks.Input;

            var splitRucksacks = Splitter.SplitInput(rucksacks);

            foreach (var rucksack in splitRucksacks)
            {
                var wronglyPackedLetters = GetWronglyPackedLetters(rucksack);

                foreach (var letter in wronglyPackedLetters)
                {
                    prioSum += GetPriority(letter);
                }
            }

            return prioSum;
        }

        public static int GetPrioSumOfAllBadges()
        {
            var prioSum = 0;
            var index = 0;

            var rucksacks = Day3Rucksacks.Input;

            var splitRucksacks = Splitter.SplitInput(rucksacks);

            for (var i = 0; i < (splitRucksacks.Length / 3); i++)
            {
                var groups = new List<string>();

                groups.Add(splitRucksacks[index]);
                groups.Add(splitRucksacks[index + 1]);
                groups.Add(splitRucksacks[index + 2]);

                var badgeLetter = GetBadgeLetter(groups);

                prioSum += GetPriority(badgeLetter);

                index = index + 3;
            }

            return prioSum;
        }

        private static char GetBadgeLetter(List<string> groups)
        {
            foreach (var letter in groups[0])
            {
                if (groups[1].Contains(letter, StringComparison.InvariantCulture))
                {
                    if (groups[2].Contains(letter, StringComparison.InvariantCulture))
                    {
                        return letter;
                    }
                }
            }

            throw new InvalidOperationException("No badge found in group of backpacks.");
        }

        private static List<char> GetWronglyPackedLetters(string rucksack)
        {
            var foundWronglyPackedLetters = new List<char>();

            var leftCompartment = rucksack[..(rucksack.Length / 2)];
            var rightCompartment = rucksack.Substring((rucksack.Length / 2), (rucksack.Length / 2));

            foreach (var letter in leftCompartment)
            {
                if (rightCompartment.Contains(letter, StringComparison.InvariantCulture))
                {
                    if (!foundWronglyPackedLetters.Contains(letter))
                    {
                        foundWronglyPackedLetters.Add(letter);
                    }
                }
            }

            return foundWronglyPackedLetters;
        }

        private static int GetPriority(char letter)
        {
            var upper = char.IsUpper(letter);

            int index = char.ToUpper(letter) - 64;

            if (upper)
            {
                return index + 26;
            }

            return index;
        }
    }
}
