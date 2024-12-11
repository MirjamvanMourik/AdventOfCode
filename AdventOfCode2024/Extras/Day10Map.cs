namespace AdventOfCode2024.Extras
{
    public class Day10Map
    {
        public Day10Vertex[,]? Map { get; set; }
        public List<Day10Vertex> TrailHeads { get; set; } = new();

        public int HighestTrailHeight { get; set; }

        private int RowCount = 0;
        private int ColCount = 0;

        public void CreateMap(List<List<string>> input)
        {
            RowCount = input.Count;
            ColCount = input[0].Count;
            Map = new Day10Vertex[RowCount, ColCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var vertex = new Day10Vertex(int.Parse(input[row][col]), row, col);

                    if (vertex.Height > HighestTrailHeight)
                    {
                        HighestTrailHeight = vertex.Height;
                    }

                    Map[row, col] = vertex;
                }
            }

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (row > 0) Map[row, col].Up = Map[row - 1, col];
                    if (row < RowCount - 1) Map[row, col].Down = Map[row + 1, col];
                    if (col > 0) Map[row, col].Left = Map[row, col - 1];
                    if (col < ColCount - 1) Map[row, col].Right = Map[row, col + 1];
                }
            }
        }

        public void FindCompleteTrails(Day10Vertex? currentVertex, int currentNumber, int lastNumber, Day10Vertex? trailStart = null)
        {
            if (currentVertex == null)
            {
                return;
            }

            trailStart ??= currentVertex;

            if (currentVertex.Height == lastNumber && currentNumber == lastNumber)
            {
                var trailEnd = (currentVertex.Row, currentVertex.Col);
                if (!trailStart.EndOfTrailFromTrailhead.Contains(trailEnd))
                {
                    trailStart.EndOfTrailFromTrailhead.Add(trailEnd);
                }
                return;
            }

            foreach (var neighbor in GetNeighbors(currentVertex, currentNumber + 1))
            {
                FindCompleteTrails(neighbor, currentNumber + 1, lastNumber, trailStart);
            }
        }

        public int FindCompleteTrailsPartTwo(Day10Vertex? startingVertex, int currentNumber, int lastNumber)
        {
            if (startingVertex == null)
            {
                return 0;
            }

            if (startingVertex.Height == lastNumber)
            {
                return currentNumber == lastNumber ? 1 : 0;
            }

            int total = 0;

            foreach (var neighbor in GetNeighbors(startingVertex, currentNumber + 1))
            {
                total += FindCompleteTrailsPartTwo(neighbor, currentNumber + 1, lastNumber);
            }

            return total;
        }

        private IEnumerable<Day10Vertex?> GetNeighbors(Day10Vertex vertex, int requiredHeight)
        {
            if (vertex.Up?.Height == requiredHeight) yield return vertex.Up;
            if (vertex.Right?.Height == requiredHeight) yield return vertex.Right;
            if (vertex.Down?.Height == requiredHeight) yield return vertex.Down;
            if (vertex.Left?.Height == requiredHeight) yield return vertex.Left;
        }

        public List<Day10Vertex> GetAllStartingPoints()
        {
            var startingPoints = new List<Day10Vertex>();

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var vertex = Map![row, col];

                    if (vertex.Height == 0)
                    {
                        startingPoints.Add(vertex);
                    }
                }
            }

            return startingPoints;
        }

    }
}