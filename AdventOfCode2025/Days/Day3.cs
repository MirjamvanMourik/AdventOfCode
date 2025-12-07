using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day3 : IDay
    {
        long IDay.Day => 3;
        string IDay.Title => "Lobby";

        public long GetFirstAnswer()
        {
            var banks = InputSplitter.SplitLinesToInt(Day3JoltageRatings.Input);

            var total = 0L;

            foreach (var bank in banks)
            {
                (var highest, var highestPosition) = GetHighestNumer(bank, inclLast: false);
                (var secondHighest, var _) = GetHighestNumer(bank, highestPosition + 1);

                total += int.Parse($"{highest}{secondHighest}");

                //foreach (var nr in bank)
                //{
                //    Console.Write(nr);
                //}
                //Console.WriteLine();
                //Console.WriteLine($"{highest}{secondHighest}");
            }

            return total;
        }

        public long GetSecondAnswer()
        {
            var banks = InputSplitter.SplitLinesToInt(Day3JoltageRatings.Input);

            var total = 0L;

            foreach (var bank in banks)
            {
                var amountOfNumbersToRemove = bank.Count - 12;

                //foreach (var b in bank)
                //{
                //    Console.Write(b);
                //}
                //Console.WriteLine();

                for (var count = amountOfNumbersToRemove; count > 0; count--)
                {
                    var indexToRemove = GetLowestIndex(bank);
                    //Console.WriteLine($"Removed: {bank[indexToRemove]} at {indexToRemove}");
                    bank.RemoveAt(indexToRemove);
                }

                //foreach (var b in bank)
                //{
                //    Console.Write(b);
                //}
                //Console.WriteLine();

                var number = "";
                foreach (var item in bank)
                {
                    number += item.ToString();
                }

                total += long.Parse(number);
            }

            return total;
        }

        private static (int, int) GetHighestNumer(List<int> bank, int startingPosition = 0, bool inclLast = true)
        {
            var highestNumber = 0;
            var highestNumberPosition = 0;
            var count = inclLast ? bank.Count : bank.Count - 1;

            for (var position = startingPosition; position < count; position++)
            {
                if (bank[position] > highestNumber)
                {
                    highestNumber = bank[position];
                    highestNumberPosition = position;
                }
            }

            return (highestNumber, highestNumberPosition);
        }

        private static int GetLowestIndex(List<int> bank)
        {
            for (var position = 0; position < bank.Count - 1; position++)
            {
                var first = bank[position];
                var second = bank[position + 1];

                if (position < bank.Count - 2)
                {
                    if (first < second)
                    {
                        return position;
                    }
                }
                else
                {
                    if (first < second)
                    {
                        return position;
                    }
                    else
                    {
                        return position + 1;
                    }
                }
            }

            return 0;
        }
    }
}
