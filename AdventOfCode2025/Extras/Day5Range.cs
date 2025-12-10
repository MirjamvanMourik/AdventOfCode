namespace AdventOfCode2025.Extras
{
    public class Day5Range(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
    }

    public static class Day5RangeExtensions
    {
        /// <summary>
        /// Return 1 for any overlap (inclusive endpoints), -1 for no overlap.
        /// </summary>
        public static int CompareTo(this Day5Range range, Day5Range otherRange)
        {
            long s1 = range.Start, e1 = range.End;
            long s2 = otherRange.Start, e2 = otherRange.End;

            return (e1 < s2 || e2 < s1) ? -1 : 1;
        }
    }
}
