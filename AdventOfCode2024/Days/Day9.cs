using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;

namespace AdventOfCode2024.Days
{
    public class Day9 : IDay
    {
        long IDay.Day => 9;
        string IDay.Title => "Disk Fragmenter";

        public long GetFirstAnswer()
        {
            var input = Day9DiskMap.Input.Trim();

            var blocks = AssignBlocks(input);

            var pattern = CreatePatternWithIds(blocks);
            var correctedPattern = CorrectPattern(pattern);

            return CalculateCheckSum(correctedPattern);
        }

        public long GetSecondAnswer()
        {
            var input = Day9DiskMap.Input.Trim();

            var blocks = AssignBlocks(input);

            var pattern = CreatePatternWithIds(blocks);
            var correctedPattern = CorrectSecondPattern(pattern);

            return CalculateCheckSum(correctedPattern);
        }

        private static long CalculateCheckSum(List<string> correctedPattern)
        {
            long total = 0;

            for (var i = 0; i < correctedPattern.Count; i++)
            {
                if (correctedPattern[i] != ".")
                {
                    total += i * int.Parse(correctedPattern[i]);
                }
            }

            return total;
        }

        private List<string> CorrectPattern(List<string> pattern)
        {
            while (true)
            {
                int firstLastNumberId = pattern.IndexOf(".") - 1;
                int lastNumberId = GetLastNumberId(pattern);

                if (firstLastNumberId == lastNumberId)
                {
                    break;
                }

                var value = pattern[lastNumberId];

                pattern.RemoveAt(lastNumberId);
                pattern.RemoveAt(firstLastNumberId + 1);
                pattern.Insert(firstLastNumberId + 1, value);
            }

            return pattern;
        }

        private static List<string> CorrectSecondPattern(List<string> pattern)
        {
            var numberGroupIndices = GetNumberGroupIndices(pattern);
            var originalNumberGroupIndices = numberGroupIndices.ToList();


            for (var n = originalNumberGroupIndices.Count - 1; n >= 0; n--)
            {
                numberGroupIndices = GetNumberGroupIndices(pattern);
                var emptySpaceGroupIndices = GetEmtySpaceGroupIndices(numberGroupIndices, pattern);

                var firstEmptySpaceIndex = pattern.IndexOf(".");

                if (originalNumberGroupIndices[n].EndIndex < firstEmptySpaceIndex)
                {
                    return pattern;
                }

                var numberLength = originalNumberGroupIndices[n].EndIndex - originalNumberGroupIndices[n].StartIndex + 1;
                var e = 0;
                var numbersMoved = false;

                while (!numbersMoved && e < emptySpaceGroupIndices.Count)
                {
                    var emptySpaceLength = emptySpaceGroupIndices[e].EndIndex - emptySpaceGroupIndices[e].StartIndex + 1;

                    if (originalNumberGroupIndices[n].EndIndex < emptySpaceGroupIndices[e].StartIndex)
                    {
                        break;
                    }

                    if (emptySpaceLength >= numberLength)
                    {
                        var index = originalNumberGroupIndices[n].StartIndex;
                        pattern.RemoveRange(index, numberLength);

                        for (var i = 0; i < numberLength; i++)
                        {
                            index = originalNumberGroupIndices[n].StartIndex + i;
                            pattern.Insert(index, ".");

                            numbersMoved = true;
                        }

                        var newIndex = emptySpaceGroupIndices[e].StartIndex;
                        pattern.RemoveRange(newIndex, numberLength);

                        for (var i = 0; i < numberLength; i++)
                        {
                            newIndex = emptySpaceGroupIndices[e].StartIndex + i;
                            pattern.Insert(newIndex, originalNumberGroupIndices[n].Value);

                            numbersMoved = true;
                        }
                    }

                    if (numbersMoved)
                    {
                        break;
                    }

                    e++;
                }
            }

            return pattern;
        }

        private static List<BlockGroup> GetNumberGroupIndices(List<string> pattern)
        {
            var blockGroups = new List<BlockGroup>();
            int startIndex = -1;

            for (int i = 0; i <= pattern.Count; i++)
            {
                if (i == pattern.Count || !int.TryParse(pattern[i], out int _))
                {
                    if (startIndex != -1)
                    {
                        blockGroups.Add(new BlockGroup(
                            pattern[startIndex].ToString(),
                            startIndex,
                            i - 1
                        ));
                        startIndex = -1;
                    }
                }
                else if (startIndex == -1 || pattern[startIndex] != pattern[i])
                {
                    if (startIndex != -1)
                    {
                        blockGroups.Add(new BlockGroup(
                            pattern[startIndex],
                            startIndex,
                            i - 1
                        ));
                    }
                    startIndex = i;
                }
            }

            return blockGroups;
        }

        private static List<BlockGroup> GetEmtySpaceGroupIndices(List<BlockGroup> numberGroups, List<string> pattern)
        {
            var result = new List<BlockGroup>();

            for (var i = 0; i < numberGroups.Count - 1; i++)
            {
                var nextGroupIndex = numberGroups[i].EndIndex + 1;
                var nextGroupLastIndex = numberGroups[i + 1].StartIndex - 1;
                var value = pattern[nextGroupIndex];

                if (value == ".")
                {
                    result.Add(new BlockGroup(value, nextGroupIndex, nextGroupLastIndex));
                }
            }

            return result;
        }

        private static List<string> CreatePatternWithIds(List<IDay9Type> blocks)
        {
            var pattern = new List<string>();
            int currentIdentifier = 0;

            foreach (var block in blocks)
            {
                if (block.Name == "file")
                {
                    for (var i = 0; i < block.AmountOfBlocks; i++)
                    {
                        pattern.Add(currentIdentifier.ToString());
                    }

                    currentIdentifier++;
                }
                else
                {
                    for (var i = 0; i < block.AmountOfBlocks; i++)
                    {
                        pattern.Add(".");
                    }
                }
            }

            return pattern;
        }

        private int GetLastNumberId(List<string> pattern, int currentIndex = 0, bool forSecondPart = false)
        {
            if (!forSecondPart)
            {
                currentIndex = pattern.Count - 1;
            }

            for (int i = currentIndex; i >= 0; i--)
            {
                if (pattern[i] != ".")
                {
                    if (forSecondPart)
                    {
                        return i + 1;
                    }

                    return i;
                }
            }

            return 0;
        }

        private static List<IDay9Type> AssignBlocks(string input)
        {
            var blocks = new List<IDay9Type>();
            bool isFile = true;

            foreach (var number in input)
            {
                blocks.Add(isFile
                    ? new Day9File("file", number)
                    : new Day9FreeSpace("free", number));
                isFile = !isFile;
            }

            return blocks;
        }
    }

    internal class BlockGroup
    {
        public string Value = string.Empty;
        public int StartIndex = 0;
        public int EndIndex = 0;

        public BlockGroup(string value, int startIndex, int endIndex)
        {
            Value = value;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }
    }
}