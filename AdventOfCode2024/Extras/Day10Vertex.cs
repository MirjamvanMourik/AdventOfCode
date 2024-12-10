namespace AdventOfCode2024.Extras
{
    public class Day10Vertex
    {
        public int Height;
        public int AmountOfTrails = 0;
        public bool IsTrailhead => AmountOfTrails > 0;

        public int Row = -1;
        public int Col = -1;

        public Day10Vertex? Up { get; set; }
        public Day10Vertex? Right { get; set; }
        public Day10Vertex? Left { get; set; }
        public Day10Vertex? Down { get; set; }

        public Day10Vertex(int height, int row, int col)
        {
            Height = height;
            Row = row;
            Col = col;
        }
    }
}
