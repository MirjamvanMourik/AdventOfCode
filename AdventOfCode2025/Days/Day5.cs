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

            foreach (var id in ids.Distinct())
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
            var rows = InputSplitter.SplitIntoRows(Day5Database.Example);

            (var ranges, var ids) = ConvertToIngredientIds(rows);

            ranges = CompareAndSortRanges(ranges);

            var freshRangeIds = new List<long>();

            foreach (var id in ids.Distinct())
            {
                foreach (var range in ranges)
                {
                    if (id >= range.Start && id <= range.End)
                    {
                        for (var rangeId = range.Start; rangeId <= range.End; rangeId++)
                        {
                            if (!freshRangeIds.Contains(rangeId))
                            {
                                freshRangeIds.Add(rangeId);
                            }
                        }
                        break;
                    }
                }
            }

            return freshRangeIds.Count;
        }

        private static (List<Day5Range>, HashSet<long>) ConvertToIngredientIds(string[] rows)
        {
            var ranges = new List<Day5Range>();
            var ids = new HashSet<long>();
            var areIds = false;

            foreach (var row in rows)
            {
                if (!areIds && !row.Contains('-'))
                {
                    areIds = true;
                }

                if (areIds)
                {
                    ids.Add(long.Parse(row));
                    continue;
                }

                var rowParts = row.Split('-');

                var range = new Day5Range(long.Parse(rowParts[0]), long.Parse(rowParts[1]));

                ranges.Add(range);
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
                    case -1:
                        // otherRange is fully contained within range - keep range
                        ranges.RemoveAt(i + 1);
                        rangesCount--;
                        i--;
                        break;
                    case 1:
                        // otherRange overlaps the end of range - start range, end otherRange
                        ranges[i].End = ranges[i + 1].End;
                        ranges.RemoveAt(i + 1);
                        rangesCount--;
                        i--;
                        break;
                    case 2:
                        // otherRange overlaps the start of range - start otherRange, end range
                        ranges[i].Start = ranges[i + 1].Start;
                        ranges.RemoveAt(i + 1);
                        rangesCount--;
                        i--;
                        break;
                    case 3:
                        // range is fully contained within otherRange - keep otherRange
                        rangesCount--;
                        ranges.RemoveAt(i);
                        i--;
                        break;
                    default:
                        // No overlap - keep both
                        break;
                }
            }

            return ranges;
        }
    }
}
