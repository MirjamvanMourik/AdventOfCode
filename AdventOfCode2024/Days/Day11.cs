using AdventOfCode2024.Input;

namespace AdventOfCode2024.Days
{
    public class Day11 : IDay
    {
        long IDay.Day => 11;
        string IDay.Title => "Plutonian Pebbles";

        public long GetFirstAnswer()
        {
            var input = Day11StoneArrangement.Input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return BlinkAndCount(25, input);
        }

        public long GetSecondAnswer()
        {
            var input = Day11StoneArrangement.Input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return BlinkAndCount(75, input);
        }

        private static long BlinkAndCount(long amountOfBlinking, string[] input)
        {
            // Use a single buffer to avoid excessive allocations.
            var currentInput = new Queue<string>(input);
            var resultBuffer = new Queue<string>(input.Length * 2); // Optimistic size for fewer resizes.

            for (var i = 0; i < amountOfBlinking; i++)
            {
                resultBuffer.Clear();

                while (currentInput.Count > 0)
                {
                    var stone = currentInput.Dequeue();
                    ProcessStone(stone, resultBuffer);
                }

                // Swap the buffers for the next iteration
                (currentInput, resultBuffer) = (resultBuffer, currentInput);
            }

            return currentInput.Count;
        }

        private static void ProcessStone(string currentStone, Queue<string> resultBuffer)
        {
            var currentNumber = long.Parse(currentStone);

            if (currentNumber == 0)
            {
                resultBuffer.Enqueue("1");
            }
            else if (currentStone.Length % 2 == 0)
            {
                var span = currentStone.AsSpan();
                long firstHalf = long.Parse(span[..(span.Length / 2)]);
                long secondHalf = long.Parse(span[(span.Length / 2)..]);

                resultBuffer.Enqueue(firstHalf.ToString());
                resultBuffer.Enqueue(secondHalf.ToString());
            }
            else
            {
                resultBuffer.Enqueue((currentNumber * 2024).ToString());
            }
        }

    }
}
