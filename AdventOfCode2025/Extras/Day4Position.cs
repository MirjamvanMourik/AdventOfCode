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
            var amount = 0;

            if (Up != null && Up.Value == Day4Value.Paper) amount++;
            if (Up?.Right != null && Up.Right.Value == Day4Value.Paper) amount++;
            if (Right != null && Right.Value == Day4Value.Paper) amount++;
            if (Right?.Down != null && Right.Down.Value == Day4Value.Paper) amount++;
            if (Down != null && Down.Value == Day4Value.Paper) amount++;
            if (Down?.Left != null && Down.Left.Value == Day4Value.Paper) amount++;
            if (Left != null && Left.Value == Day4Value.Paper) amount++;
            if (Left?.Up != null && Left.Up.Value == Day4Value.Paper) amount++;

            return amount;
        }
    }

    public enum Day4Value
    {
        Empty,
        Paper
    }
}
