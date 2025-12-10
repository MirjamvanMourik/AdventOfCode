namespace AdventOfCode2025.Extras
{
    public class Day5Range(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }

    public static class Day5RangeExtensions
    {
        public static int CompareTo(this Day5Range range, Day5Range otherRange)
        {
            // -1: otherRange is fully contained within range - keep range
            if (otherRange.Start >= range.Start && otherRange.End <= range.End)
            {
                return -1;
            }
            // 1: otherRange overlaps the end of range - start range, end otherRange
            else if (otherRange.Start >= range.Start && otherRange.Start <= range.End && otherRange.End > range.End)
            {
                return 1;
            }
            // 2: otherRange overlaps the start of range - start otherRange, end range
            else if (otherRange.Start < range.Start && otherRange.End >= range.Start && otherRange.End <= range.End)
            {
                return 2;
            }
            // 3: range is fully contained within otherRange - keep otherRange
            else if (otherRange.Start < range.Start && otherRange.End > range.End)
            {
                return 3;
            }

            return 0; // No overlap - keep both
        }
    }
}
