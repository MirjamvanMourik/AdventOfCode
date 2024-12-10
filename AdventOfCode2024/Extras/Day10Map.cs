namespace AdventOfCode2024.Extras
{
    public class Day10Map
    {
        public Day10Vertex[,]? Map { get; set; }
        public List<Day10Vertex> TrailHeads { get; set; } = new();

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

        public int FindCompleteTrails(Day10Vertex? startingVertex, int nextNumber, int lastNumber)
        {
            if (startingVertex!.Height == lastNumber)
            {
                return 1;
            }

            if (startingVertex.Height < nextNumber)
            {
                return 0;
            }

            int total = 0;

            if (startingVertex.Up != null)
            {
                total += FindCompleteTrails(startingVertex.Up, nextNumber + 1, lastNumber);
            }

            if (startingVertex.Right != null)
            {
                total += FindCompleteTrails(startingVertex.Right, nextNumber + 1, lastNumber);
            }

            if (startingVertex.Down != null)
            {
                total += FindCompleteTrails(startingVertex.Down, nextNumber + 1, lastNumber);
            }

            if (startingVertex.Left != null)
            {
                total += FindCompleteTrails(startingVertex.Left, nextNumber + 1, lastNumber);
            }

            return total;
        }


        public List<Day10Vertex> GetAllStartingPoints()
        {
            var startingPoints = new List<Day10Vertex>();

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (Map![row, col].Height == 0)
                    {
                        startingPoints.Add(Map[row, col]);
                    }
                }
            }

            return startingPoints;
        }
    }
}
