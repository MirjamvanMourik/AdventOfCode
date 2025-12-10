using AdventOfCode2025.Extras;
using AdventOfCode2025.Input;
using ConsoleHelper;

namespace AdventOfCode2025.Days
{
    public class Day5 : IDay
    {
        long IDay.Day => 5;
        string IDay.Title => "Cafeteria";

        public long GetFirstAnswer()
        {
            var rows = InputSplitter.SplitIntoRows(Day5Database.Input);

            (var ranges, var ids) = ConvertToIngredientIds(rows);

            ranges = CompareAndSortRanges(ranges);

            var freshIngredienstAmount = 0L;

            foreach (var id in ids.Distinct().Order())
            {
                foreach (var range in ranges)
                {
                    if (id >= range.Start && id <= range.End)
                    {
                        freshIngredienstAmount++;
                        break;
                    }
                }
            }

            return freshIngredienstAmount;
        }

        public long GetSecondAnswer()
        {
            var rows = InputSplitter.SplitIntoRows(Day5Database.Input);

            (var ranges, var _) = ConvertToIngredientIds(rows, true);

            var merged = CompareAndSortRanges(ranges);

            long total = 0;
            foreach (var r in merged)
            {
                total += (r.End - r.Start + 1);
            }

            return total;
        }

        private static (List<Day5Range>, HashSet<long>) ConvertToIngredientIds(string[] rows, bool withoutIds = false)
        {
            var ranges = new List<Day5Range>();
            var ids = new HashSet<long>();

            foreach (var row in rows)
            {
                if (row.Contains('-'))
                {
                    var rowParts = row.Split('-');
                    var range = new Day5Range(long.Parse(rowParts[0]), long.Parse(rowParts[1]));
                    ranges.Add(range);
                    continue;
                }

                if (withoutIds)
                {
                    continue;
                }

                ids.Add(long.Parse(row));
            }

            return (ranges, ids);
        }

        private static List<Day5Range> CompareAndSortRanges(List<Day5Range> ranges)
        {
            ranges = [.. ranges.OrderBy(r => r.Start)];
            var rangesCount = ranges.Count;

            for (var i = 0; i < rangesCount - 1; i++)
            {
                var comparison = ranges[i].CompareTo(ranges[i + 1]);

                switch (comparison)
                {
                    case 1:
                        // overlap
                        ranges[i].Start = Math.Min(ranges[i].Start, ranges[i + 1].Start);
                        ranges[i].End = Math.Max(ranges[i].End, ranges[i + 1].End);
                        ranges.RemoveAt(i + 1);
                        rangesCount--;
                        i--;
                        break;
                    default:
                        // No overlap
                        break;
                }
            }

            return ranges;
        }
    }
}
