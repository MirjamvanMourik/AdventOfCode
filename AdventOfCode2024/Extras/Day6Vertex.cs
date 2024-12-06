namespace AdventOfCode2024.Extras
{
    public class Day6Vertex
    {
        public bool IsObstacle = false;
        public bool Visited = false;

        public Day6Vertex? Up { get; set; }
        public Day6Vertex? Right { get; set; }
        public Day6Vertex? Left { get; set; }
        public Day6Vertex? Down { get; set; }

        public void MarkAsObstacle()
        {
            IsObstacle = true;
        }

        public void VisitVertex()
        {
            Visited = true;
        }
    }
}
