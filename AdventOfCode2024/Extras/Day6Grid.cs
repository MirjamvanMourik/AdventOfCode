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
                        vertex.IsStartingPosition = true;

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
                var (newDirection, newPossibleVertex) = GetNextNonObstacleVertex(currentVertex!, direction);
                direction = newDirection;
                var nextVertex = newPossibleVertex;

                if (nextVertex == null)
                {
                    gotOffTheMap = true;
                    continue;
                }

                if (nextVertex.IsObstacle)
                {
                    direction = TurnRightAndGetNewDirection(direction);

                    var (newDirection2, newPossibleVertex2) = GetNextNonObstacleVertex(currentVertex!, direction);
                    direction = newDirection2;
                    currentVertex = newPossibleVertex2;
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

        public void PrintGrid(bool withPossibleObstacles = false)
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    Grid![row, col].Print(withPossibleObstacles);
                }

                Console.WriteLine();
            }
        }

        public int LookForPossibleObstacles(bool isTest = false)
        {
            var direction = InitialDirection;
            var currentVertex = Guard;
            var gotOffTheMap = false;
            var states = new List<(Day6Vertex visitingState, string nextDirection)>();
            var amountOfPossibleObstacles = 0;

            if (isTest)
            {
                currentVertex.IsCurrentPosition = true;
                PrintGrid();
                Console.WriteLine();
                currentVertex.IsCurrentPosition = false;
            }

            while (!gotOffTheMap)
            {
                var (newDirection, newPossibleVertex) = GetNextNonObstacleVertex(currentVertex!, direction, states);

                direction = newDirection;
                var nextVertex = newPossibleVertex;

                if (nextVertex == null)
                {
                    if (isTest)
                    {
                        Console.WriteLine("Got off the map");
                        Console.WriteLine();
                    }

                    gotOffTheMap = true;
                    continue;
                }

                if (nextVertex.IsObstacle)
                {
                    direction = TurnRightAndGetNewDirection(direction);
                    states.Add((currentVertex!, direction));


                    var (newDirection2, newPossibleVertex2) = GetNextNonObstacleVertex(currentVertex!, direction, states);
                    direction = newDirection2;
                    currentVertex = newPossibleVertex2;

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

                    if (WalkThePathWithNewObstacle(currentVertex!, direction, copyOfStates, isTest))
                    {
                        if (nextVertex.Row != Guard!.Row || nextVertex.Col != Guard.Col)
                        {
                            nextVertex.IsPossibleObstacle = true;
                            amountOfPossibleObstacles++;
                        }
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
                    if (isTest)
                    {
                        Console.WriteLine("Got off the map");
                        Console.WriteLine();
                    }

                    gotOffTheMap = true;
                    continue;
                }

                if (isTest)
                {
                    currentVertex.IsCurrentPosition = true;
                    PrintGrid();
                    Console.WriteLine();
                    currentVertex.IsCurrentPosition = false;
                }
            }

            if (isTest)
            {
                PrintGrid(true);
                Console.WriteLine();
            }

            return amountOfPossibleObstacles;
        }

        private bool WalkThePathWithNewObstacle(Day6Vertex startingVertex, string startingDirection, List<(Day6Vertex visitingState, string nextDirection)> states, bool isTest)
        {
            var direction = startingDirection;
            var currentVertex = startingVertex;
            var gotOffTheMap = false;

            if (isTest)
            {
                currentVertex.IsCurrentPosition = true;
                PrintGrid();
                Console.WriteLine();
                currentVertex.IsCurrentPosition = false;
            }

            while (!gotOffTheMap)
            {
                var (newDirection, newPossibleVertex) = GetNextNonObstacleVertex(currentVertex!, direction, states);

                if (newDirection == "stuck")
                {
                    if (isTest)
                    {
                        Console.WriteLine("Stuck in loop");
                        Console.WriteLine();
                    }

                    return true;
                }

                direction = newDirection;
                var nextVertex = newPossibleVertex;

                if (nextVertex == null)
                {
                    if (isTest)
                    {
                        Console.WriteLine("Got off the map");
                        Console.WriteLine();
                    }

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
                        if (isTest)
                        {
                            Console.WriteLine("Stuck in loop");
                            Console.WriteLine();
                        }

                        return true;
                    }

                    states.Add((currentVertex!, direction));

                    var (newDirection2, newPossibleVertex2) = GetNextNonObstacleVertex(currentVertex!, direction, states);

                    if (newDirection == "stuck")
                    {
                        if (isTest)
                        {
                            Console.WriteLine("Stuck in loop");
                            Console.WriteLine();
                        }

                        return true;
                    }

                    direction = newDirection2;
                    currentVertex = newPossibleVertex2;
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
                    if (isTest)
                    {
                        Console.WriteLine("Got off the map");
                        Console.WriteLine();
                    }

                    gotOffTheMap = true;
                }

                if (isTest)
                {
                    currentVertex.IsCurrentPosition = true;
                    PrintGrid();
                    Console.WriteLine();
                    currentVertex.IsCurrentPosition = false;
                }
            }

            return false;
        }

        private (string newDirection, Day6Vertex? newNonObstacleVertex) GetNextNonObstacleVertex(Day6Vertex currentVertex, string direction, List<(Day6Vertex visitingState, string nextDirection)>? states = null)
        {
            var newDirection = direction;

            var nextPossibleVertex = direction switch
            {
                "r" => currentVertex?.Right,
                "l" => currentVertex?.Left,
                "u" => currentVertex?.Up,
                "d" => currentVertex?.Down,
                _ => null
            };

            if (nextPossibleVertex != null && nextPossibleVertex.IsObstacle)
            {
                newDirection = TurnRightAndGetNewDirection(direction);

                if (states != null)
                {
                    if (states!.Where(state => state.visitingState.Row == currentVertex!.Row
                            && state.visitingState.Col == currentVertex.Col
                            && state.nextDirection == newDirection).Any())
                    {
                        return ("stuck", null);
                    }

                    states!.Add((currentVertex!, newDirection));
                }

                return GetNextNonObstacleVertex(currentVertex!, newDirection, states);
            }

            return (newDirection, nextPossibleVertex);
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
