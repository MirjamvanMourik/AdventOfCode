namespace AdventOfCode2024.Extras
{
    public class Day12Region
    {
        public string Kind { get; set; } = string.Empty;
        public int Perimeter { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Day12Region(string kind, int row, int col)
        {
            Kind = kind;
            Row = row;
            Col = col;
        }

        public Day12SpaceBetween? Up { get; set; }
        public Day12SpaceBetween? Right { get; set; }
        public Day12SpaceBetween? Left { get; set; }
        public Day12SpaceBetween? Down { get; set; }
    }
}
