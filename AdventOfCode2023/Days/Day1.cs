using AdventOfCode2023.Extras;
using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day1 : IDay
    {
        long IDay.Day => 1;

        public long GetFirstAnswer()
        {
            return GetAnswer(InputSplitter.SplitPerLine(Day1Calibration.Input), false);
        }

        public long GetSecondAnswer()
        {
            return GetAnswer(InputSplitter.SplitPerLine(Day1Calibration.Input), true);
        }

        private static long GetAnswer(string[] input, bool withWrittenNumbers)
        {
            var spelledOutNumbers = new Dictionary<string, string>() {
                {"one", "1"},
                {"two", "2"},
                {"three", "3"},
                {"four", "4"},
                {"five", "5"},
                {"six", "6"},
                {"seven", "7"},
                {"eight", "8"},
                {"nine", "9"}
            };

            long total = 0;
            var firstNumber = 0;
            var lastNumber = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (withWrittenNumbers)
                {
                    var writtenNumberIndexes = new List<Day1WrittenNumberIndex>();
                    var inputLine = input[i];

                    foreach(var spelledOutNumber in spelledOutNumbers)
                    {
                        long firstIndex = inputLine.IndexOf(spelledOutNumber.Key);

                        if (firstIndex != -1 && !writtenNumberIndexes.Any(n => n.Index == firstIndex))
                        {
                            writtenNumberIndexes.Add(new Day1WrittenNumberIndex(firstIndex, spelledOutNumber.Key));
                        }

                        long lastIndex = inputLine.LastIndexOf(spelledOutNumber.Key);

                        if (firstIndex != -1 && !writtenNumberIndexes.Any(n => n.Index == lastIndex))
                        {
                            writtenNumberIndexes.Add(new Day1WrittenNumberIndex(lastIndex, spelledOutNumber.Key));
                        }
                    }

                    writtenNumberIndexes = writtenNumberIndexes.OrderBy(n => n.Index).ToList();

                    if (writtenNumberIndexes.Count > 0)
                    {
                        var firstIndex = (int)writtenNumberIndexes.First().Index;
                        var firstWrittenNumber = writtenNumberIndexes.First().WrittenNumber;
                        inputLine = inputLine.Remove(firstIndex, firstWrittenNumber.Length).Insert(firstIndex, spelledOutNumbers[firstWrittenNumber]);

                        if (writtenNumberIndexes.Count > 1)
                        {
                            var lastIndex = (int)writtenNumberIndexes.Last().Index - (firstWrittenNumber.Length - 1);
                            var lastWrittenNumber = writtenNumberIndexes.Last().WrittenNumber;

                            var subString = inputLine.Substring(lastIndex, lastWrittenNumber.Length);
                            var actualLastIndexDifference = -1;
                            var actualLength = 0;

                            if (subString != lastWrittenNumber) {
                                for (var k = 0; k < subString.Length; k++)
                                {
                                    if (lastWrittenNumber.Contains(subString[k]))
                                    {
                                        actualLength++;

                                        if (actualLastIndexDifference == -1)
                                        {
                                            actualLastIndexDifference = k;
                                        }
                                    }
                                }
                            }

                            if (actualLastIndexDifference == -1)
                            {
                                actualLength = lastWrittenNumber.Length;
                                actualLastIndexDifference = 0;
                            }

                            inputLine = inputLine.Remove(lastIndex + actualLastIndexDifference, actualLength).Insert(lastIndex + actualLastIndexDifference, spelledOutNumbers[lastWrittenNumber]);
                        }
                    }

                    input[i] = inputLine;
                }

                for (var j = 0; j < input[i].Length; j++)
                {
                    if (char.IsNumber(input[i].ElementAt(j)))
                    {
                        if (firstNumber == 0)
                        {
                            firstNumber = input[i].ElementAt(j);
                        }
                        else
                        {
                            lastNumber = input[i].ElementAt(j);
                        }
                    }
                }

                if (lastNumber == 0)
                {
                    lastNumber = firstNumber;
                }

                var totalString = "" + firstNumber + "" + lastNumber;
                total += long.Parse(totalString);
                firstNumber = 0;
                lastNumber = 0;
            }

            return total;
        }
    }
}
