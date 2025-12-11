namespace AdventOfCode2025.Extras
{
    public class Day6Number(long number, long length)
    {
        public long Number { get; } = number;
        public long Length { get; } = length;
    }

    public class Day6Operator(char op, int start, int end)
    {
        public char Operator { get; } = op;
        public int Start { get; } = start;
        public int End { get; } = end;
    }
}
