using System.Text;

namespace AdventOfCode2025.Extras
{
    public class Day4Grid
    {
        public Day4Position[,]? Grid { get; set; }
        private int RowCount = 0;
        private int ColCount = 0;

        public long FindAmountOfAccessiblePapers(List<List<string>> input, bool printGrid = false)
        {
            if (Grid == null)
            {
                CreateGrid(input);
            }

            var toRemove = new List<Day4Position>();

            foreach (var pos in Grid!)
            {
                if (pos.Value == Day4Value.Paper)
                {
                    var amount = pos.FindPaperAroundPosition();
                    if (amount < 4)
                    {
                        toRemove.Add(pos);
                    }
                }
            }

            foreach (var pos in toRemove)
            {
                pos.Value = Day4Value.Empty;
            }

            if (printGrid)
            {
                PrintGrid();
            }

            return toRemove.Count;
        }

        public void CreateGrid(List<List<string>> input)
        {
            RowCount = input.Count;
            ColCount = input[0].Count;

            Grid = new Day4Position[RowCount, ColCount];

            for (int r = 0; r < RowCount; r++)
            {
                for (int c = 0; c < ColCount; c++)
                {
                    Grid[r, c] = new Day4Position(c, r, input[r][c]);
                }
            }

            foreach (var pos in Grid)
            {
                int r = pos.Row;
                int c = pos.Column;

                pos.Up ??= r > 0 ? Grid[r - 1, c] : null;
                pos.Down ??= r < RowCount - 1 ? Grid[r + 1, c] : null;
                pos.Left ??= c > 0 ? Grid[r, c - 1] : null;
                pos.Right ??= c < ColCount - 1 ? Grid[r, c + 1] : null;
            }
        }

        private void PrintGrid()
        {
            if (Grid == null)
                return;

            for (int r = 0; r < RowCount; r++)
            {
                var sb = new StringBuilder();

                for (int c = 0; c < ColCount; c++)
                {
                    var pos = Grid[r, c];

                    if (pos != null)
                    {
                        string valStr = pos.Value.ToString() ?? string.Empty;
                        var consoleColor = pos.Value == Day4Value.Paper && pos.FindPaperAroundPosition() < 4 ? ConsoleColor.Green : ConsoleColor.Red;

                        if (string.Equals(valStr, "Empty", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.Write(". ");
                        }
                        else if (string.Equals(valStr, "Paper", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.Write("@ ");
                        }
                        else if (!string.IsNullOrEmpty(valStr))
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.Write(valStr[0]);
                        }
                        else
                        {
                            Console.Write("  ");
                            Console.ResetColor();
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}