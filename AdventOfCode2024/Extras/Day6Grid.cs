namespace AdventOfCode2024.Extras
{
    public class Day6Grid
    {
        public Day6Vertex[,]? Grid { get; set; }
        private int RowCount = 0;
        private int ColCount = 0;
        private Day6Vertex? Guard { get; set; }
        private string InitialDirection = string.Empty;

        public void CreateGrid(List<List<string>> input)
        {
            RowCount = input.Count;
            ColCount = input[0].Count;

            Grid = new Day6Vertex[RowCount, ColCount];

            var guardCol = 0;
            var guardRow = 0;

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var vertex = new Day6Vertex();
                    var location = input[row][col];

                    if (location == "#")
                    {
                        vertex.MarkAsObstacle();
                    }
                    else if (location != ".")
                    {
                        vertex.VisitVertex();
                        guardCol = col;
                        guardRow = row;

                        InitialDirection = location switch
                        {
                            ">" => "r",
                            "<" => "l",
                            "^" => "u",
                            _ => "d",
                        };
                    }

                    Grid[row, col] = vertex;
                }
            }

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (row > 0) Grid[row, col].Up = Grid[row - 1, col];
                    if (row < RowCount - 1) Grid[row, col].Down = Grid[row + 1, col];
                    if (col > 0) Grid[row, col].Left = Grid[row, col - 1];
                    if (col < ColCount - 1) Grid[row, col].Right = Grid[row, col + 1];
                }
            }

            Guard = Grid[guardRow, guardCol];
        }

        public void WalkThePath()
        {
            var direction = InitialDirection;
            var gotOffTheMap = false;
            var currentVertex = Guard;

            while (!gotOffTheMap)
            {
                switch (direction)
                {
                    case "r":
                        if (currentVertex?.Right != null)
                        {
                            if (currentVertex.Right.IsObstacle)
                            {
                                direction = TurnRightAndGetNewDirection(direction);
                                currentVertex = currentVertex.Down;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                            else
                            {
                                currentVertex = currentVertex.Right;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                        }
                        else
                        {
                            gotOffTheMap = true;
                        }
                        break;
                    case "l":
                        if (currentVertex?.Left != null)
                        {
                            if (currentVertex.Left.IsObstacle)
                            {
                                direction = TurnRightAndGetNewDirection(direction);
                                currentVertex = currentVertex.Up;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                            else
                            {
                                currentVertex = currentVertex.Left;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                        }
                        else
                        {
                            gotOffTheMap = true;
                        }
                        break;
                    case "u":
                        if (currentVertex?.Up != null)
                        {
                            if (currentVertex.Up.IsObstacle)
                            {
                                direction = TurnRightAndGetNewDirection(direction);
                                currentVertex = currentVertex.Right;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                            else
                            {
                                currentVertex = currentVertex.Up;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                        }
                        else
                        {
                            gotOffTheMap = true;
                        }
                        break;
                    default:
                        if (currentVertex?.Down != null)
                        {
                            if (currentVertex.Down.IsObstacle)
                            {
                                direction = TurnRightAndGetNewDirection(direction);
                                currentVertex = currentVertex.Left;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                            else
                            {
                                currentVertex = currentVertex.Down;

                                if (currentVertex != null)
                                {
                                    currentVertex.VisitVertex();
                                }
                                else
                                {
                                    gotOffTheMap = true;
                                }
                            }
                        }
                        else
                        {
                            gotOffTheMap = true;
                        }
                        break;
                }
            }
        }

        public int CountVisitedLocations()
        {
            var visitedLocationsAmount = 0;

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (Grid![row, col].Visited)
                    {
                        visitedLocationsAmount++;
                    }
                }
            }

            return visitedLocationsAmount;
        }

        private static string TurnRightAndGetNewDirection(string direction)
        {
            if (direction == "u")
            {
                return "r";
            }
            else if (direction == "r")
            {
                return "d";
            }
            else if (direction == "d")
            {
                return "l";
            }

            return "u";
        }
    }
}
