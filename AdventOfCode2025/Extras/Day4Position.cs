namespace AdventOfCode2025.Extras
{
    public class Day4Position(int c, int r, string value)
    {
        public int Column { get; set; } = c;
        public int Row { get; set; } = r;
        public Day4Value Value { get; set; } = value == "." ? Day4Value.Empty : Day4Value.Paper;
        public Day4Position? Up { get; set; }
        public Day4Position? Down { get; set; }
        public Day4Position? Left { get; set; }
        public Day4Position? Right { get; set; }

        public int FindPaperAroundPosition()
        {
            var amount = new[]
            {
                Up,
                Up?.Right,
                Right,
                Right?.Down,
                Down,
                Down?.Left,
                Left,
                Left?.Up
            }.Count(p => p?.Value == Day4Value.Paper);

            if (amount < 4)
            {
                Value = Day4Value.Empty;
            }

            return amount;
        }
    }

    public enum Day4Value
    {
        Empty,
        Paper
    }
}
