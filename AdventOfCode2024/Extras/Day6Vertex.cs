namespace AdventOfCode2024.Extras
{
    public class Day6Vertex
    {
        public bool IsObstacle = false;
        public bool Visited = false;
        public int Row = -1;
        public int Col = -1;

        public Day6Vertex? Up { get; set; }
        public Day6Vertex? Right { get; set; }
        public Day6Vertex? Left { get; set; }
        public Day6Vertex? Down { get; set; }

        public void MarkAsObstacle()
        {
            IsObstacle = true;
        }

        public void RemoveAsObstacle()
        {
            IsObstacle = false;
        }

        public void VisitVertex()
        {
            Visited = true;
        }
    }
}
