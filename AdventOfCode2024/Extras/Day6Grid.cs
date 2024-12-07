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
                    var vertex = new Day6Vertex
                    {
                        Row = row,
                        Col = col
                    };

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
            var currentVertex = Guard;
            var gotOffTheMap = false;

            while (!gotOffTheMap)
            {
                var nextVertex = GetNextVertex(currentVertex!, direction);
                if (nextVertex == null)
                {
                    gotOffTheMap = true;
                    continue;
                }

                if (nextVertex.IsObstacle)
                {
                    direction = TurnRightAndGetNewDirection(direction);
                    currentVertex = GetNextVertex(currentVertex!, direction);
                }
                else
                {
                    currentVertex = nextVertex;
                }

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

        public int LookForPossibleObstacles()
        {
            var direction = InitialDirection;
            var currentVertex = Guard;
            var gotOffTheMap = false;
            var states = new List<(Day6Vertex visitingState, string nextDirection)>();
            var amountOfPossibleObstacles = 0;

            while (!gotOffTheMap)
            {
                var nextVertex = GetNextVertex(currentVertex!, direction);
                if (nextVertex == null)
                {
                    gotOffTheMap = true;
                    continue;
                }

                if (nextVertex.IsObstacle)
                {
                    direction = TurnRightAndGetNewDirection(direction);
                    states.Add((currentVertex!, direction));
                    currentVertex = GetNextVertex(currentVertex!, direction);
                }
                else
                {
                    states.Add((currentVertex!, direction));

                    nextVertex.MarkAsObstacle();

                    var copyOfStates = new List<(Day6Vertex visitingState, string nextDirection)>();

                    states.ForEach(state =>
                    {
                        copyOfStates.Add(state);
                    });

                    if (WalkThePathWithNewObstacle(currentVertex!, direction, copyOfStates))
                    {
                        amountOfPossibleObstacles++;
                    }

                    nextVertex.RemoveAsObstacle();

                    currentVertex = nextVertex;
                }

                if (currentVertex != null)
                {
                    currentVertex.VisitVertex();
                }
                else
                {
                    gotOffTheMap = true;
                }
            }

            return amountOfPossibleObstacles;
        }

        private static bool WalkThePathWithNewObstacle(Day6Vertex startingVertex, string startingDirection, List<(Day6Vertex visitingState, string nextDirection)> states)
        {
            var direction = startingDirection;
            var currentVertex = startingVertex;
            var gotOffTheMap = false;

            while (!gotOffTheMap)
            {
                var nextVertex = GetNextVertex(currentVertex!, direction);
                if (nextVertex == null)
                {
                    gotOffTheMap = true;
                    continue;
                }

                if (nextVertex.IsObstacle)
                {
                    direction = TurnRightAndGetNewDirection(direction);

                    if (states.Where(state => state.visitingState.Row == currentVertex!.Row
                        && state.visitingState.Col == currentVertex.Col
                        && state.nextDirection == direction).Any())
                    {
                        return true;
                    }

                    currentVertex = GetNextVertex(currentVertex!, direction);
                }
                else
                {
                    if (states.Where(state => state.visitingState.Row == currentVertex.Row
                        && state.visitingState.Col == currentVertex.Col
                        && state.nextDirection == direction).Any())
                    {
                        return true;
                    }

                    currentVertex = nextVertex;
                }

                if (currentVertex != null)
                {
                    currentVertex.VisitVertex();
                }
                else
                {
                    gotOffTheMap = true;
                }
            }

            return false;
        }

        private static Day6Vertex? GetNextVertex(Day6Vertex current, string direction)
        {
            return direction switch
            {
                "r" => current?.Right,
                "l" => current?.Left,
                "u" => current?.Up,
                "d" => current?.Down,
                _ => null
            };
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
