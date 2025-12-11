using AdventOfCode2025.Extras;
using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day6 : IDay
    {
        long IDay.Day => 6;
        string IDay.Title => "Trash Compactor";

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitLinesSeperatedBySpaces(Day6MathWorksheet.Input);
            var numbers = new List<List<long>>(input.Count - 1);
            var operators = input.Last();

            foreach (var line in input.Take(input.Count - 1))
            {
                var lineNumber = 0;
                foreach (var number in line)
                {
                    if (numbers.Count == lineNumber)
                    {
                        numbers.Add([]);
                    }

                    numbers[lineNumber].Add(long.Parse(number));
                    lineNumber++;
                }
            }

            var total = 0L;

            for (var i = 0; i < numbers.Count; i++)
            {
                var count = 0L;

                for (var j = 0; j < numbers[i].Count - 1; j++)
                {
                    if (operators[i] == "+")
                    {
                        if (j == 0)
                        {
                            count = numbers[i][j] + numbers[i][j + 1];
                            continue;
                        }

                        count += numbers[i][j + 1];
                    }
                    else if (operators[i] == "*")
                    {
                        if (j == 0)
                        {
                            count = numbers[i][j] * numbers[i][j + 1];
                            continue;
                        }

                        count *= numbers[i][j + 1];
                    }
                }

                total += count;
            }

            return total;
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitIntoRows(Day6MathWorksheet.Input);
            var operatorsRow = input.Last();
            var operators = new List<Day6Operator>();

            var lastOperatorPosition = 0;
            var lastOperator = '.';

            for (var i = 0; i < operatorsRow.Length; i++)
            {
                if (i == 0)
                {
                    lastOperator = operatorsRow[i];
                    continue;
                }

                if (operatorsRow[i] != ' ')
                {
                    if (i == operatorsRow.Length - 1)
                    {
                        operators.Add(new Day6Operator(lastOperator, lastOperatorPosition, i - 2));
                        lastOperatorPosition = i;
                        lastOperator = operatorsRow[i];

                        operators.Add(new Day6Operator(lastOperator, lastOperatorPosition, i));
                    }

                    operators.Add(new Day6Operator(lastOperator, lastOperatorPosition, i - 2));
                    lastOperatorPosition = i;
                    lastOperator = operatorsRow[i];
                }
                else
                {
                    if (i == operatorsRow.Length - 1)
                    {
                        operators.Add(new Day6Operator(lastOperator, lastOperatorPosition, i));
                    }
                }
            }

            var total = 0L;

            for (var problem = 0; problem < operators.Count; problem++)
            {
                var columnWidth = operators[problem].End - operators[problem].Start + 1;
                var numbersInProblem = new List<long>();

                for (var nrInNumber = 0; nrInNumber < columnWidth; nrInNumber++)
                {
                    var numberAsString = "";

                    for (var numberInProblem = 0; numberInProblem < input.Length - 1; numberInProblem++)
                    {
                        if (input[numberInProblem][operators[problem].Start + nrInNumber] != ' ')
                        {
                            numberAsString += input[numberInProblem][operators[problem].Start + nrInNumber];
                        }
                    }

                    numbersInProblem.Add(long.Parse(numberAsString));
                }

                if (operators[problem].Operator == '+')
                {
                    var result = numbersInProblem.Sum();
                    total += result;

                    //foreach (var n in numbersInProblem)
                    //{
                    //    Console.Write($"{n} {operators[problem].Operator} ");
                    //}
                    //Console.Write($"= {result}");
                    //Console.WriteLine();
                    //Console.WriteLine($"Total: {total}");
                }
                else
                {
                    var result = numbersInProblem.Aggregate(1L, (acc, x) => acc * x);
                    total += result;

                    //foreach (var n in numbersInProblem)
                    //{
                    //    Console.Write($"{n} {operators[problem].Operator} ");
                    //}
                    //Console.Write($"= {result}");
                    //Console.WriteLine();
                    //Console.WriteLine($"Total: {total}");
                }
            }

            return total;
        }
    }
}