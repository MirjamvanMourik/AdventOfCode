
namespace AdventOfCode2024.Extras
{
    public class Day12Garden
    {
        public Day12Region[,]? Garden { get; set; }
        private int RowCount = 0;
        private int ColCount = 0;
        private List<List<Day12Region>> PlotGroups = [];
        private HashSet<Day12Region> VisitedRegions = [];

        public void CreateGarden(List<List<string>> input)
        {
            RowCount = input.Count;
            ColCount = input[0].Count;

            Garden = new Day12Region[RowCount, ColCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    // Create the region
                    var region = new Day12Region(input[row][col], row, col);
                    Garden[row, col] = region;

                    // Link vertical neighbors
                    if (row > 0)
                    {
                        region.Up = new Day12SpaceBetween { Up = Garden[row - 1, col] };
                        Garden[row - 1, col].Down = new Day12SpaceBetween { Down = region };
                    }
                    else
                    {
                        region.Up = new Day12SpaceBetween();
                    }

                    // Link horizontal neighbors
                    if (col > 0)
                    {
                        region.Left = new Day12SpaceBetween { Left = Garden[row, col - 1] };
                        Garden[row, col - 1].Right = new Day12SpaceBetween { Right = region };
                    }
                    else
                    {
                        region.Left = new Day12SpaceBetween();
                    }

                    // Handle edges
                    if (row == RowCount - 1)
                    {
                        region.Down = new Day12SpaceBetween();
                    }
                    if (col == ColCount - 1)
                    {
                        region.Right = new Day12SpaceBetween();
                    }
                }
            }
        }

        public long CalculatePrice()
        {
            FindPlotGroups();
            PlaceFences();

            return PlotGroups.Sum(plotGroup => plotGroup.Count * plotGroup.Sum(plot => plot.Perimeter));
        }

        public long CalculateDiscountedPrice()
        {
            FindPlotGroups();
            var price = 0;

            foreach (var group in PlotGroups)
            {
                int sides = CountSides(group);
                price += sides * group.Count;
            }

            return price;
        }

        private static int CountSides(List<Day12Region> group)
        {
            var rows = group.Select(region => region.Row).Distinct().ToList();
            var cols = group.Select(region => region.Col).Distinct().ToList();

            var totalCount = 0;

            // Check vertical sides
            foreach (var row in rows)
            {
                var regionsInRow = group.Where(region => region.Row == row).ToList();
                var topFences = 0;

                for (var i = 0; i < regionsInRow.Count; i++)
                {
                    if (i == 0)
                    {
                        if (regionsInRow[i].Up!.isFence)
                        {

                        }
                        count++;
                        continue;
                    }

                    if (regionsInRow[i].Col - regionsInRow[i - 1].Col > 1)
                    {
                        if
                    }
                }

                totalCount += count;
            }

            // Check horizontal sides
            foreach (var col in cols)
            {
                var regionsInCol = group.Where(region => region.Col == col).ToList();
                var count = 0;

                for (var i = 0; i < regionsInCol.Count; i++)
                {
                    if (i == 0)
                    {
                        count++;
                        continue;
                    }

                    if (regionsInCol[i].Col - regionsInCol[i - 1].Col > 1)
                    {
                        count++;
                    }
                }

                totalCount += count;
            }

            return totalCount;
        }

        private void PlaceFences()
        {
            foreach (var plotGroup in PlotGroups)
            {
                foreach (var currentPlot in plotGroup)
                {
                    currentPlot.Perimeter = CalculatePerimeter(currentPlot);
                }
            }
        }

        private static int CalculatePerimeter(Day12Region plot)
        {
            int perimeter = 0;

            if (plot.Up!.Up == null || plot.Up.Up.Kind != plot.Kind)
            {
                perimeter++;
                plot.Up.isFence = true;
            }

            if (plot.Right!.Right == null || plot.Right.Right.Kind != plot.Kind)
            {
                perimeter++;
                plot.Up.isFence = true;
            }

            if (plot.Down!.Down == null || plot.Down.Down.Kind != plot.Kind)
            {
                perimeter++;
                plot.Up.isFence = true;
            }

            if (plot.Left!.Left == null || plot.Left.Left.Kind != plot.Kind)
            {
                perimeter++;
                plot.Up.isFence = true;
            }

            return perimeter;
        }

        private void FindPlotGroups()
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var region = Garden![row, col];

                    if (!VisitedRegions.Contains(region))
                    {
                        var group = new List<Day12Region>();
                        CreateGroup(region, group);
                        PlotGroups.Add(group);
                    }
                }
            }
        }

        private void CreateGroup(Day12Region start, List<Day12Region> group)
        {
            var stack = new Stack<Day12Region>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (!VisitedRegions.Add(current))
                {
                    continue;
                }

                group.Add(current);

                PushIfValid(stack, current.Up!.Up, current.Kind);
                PushIfValid(stack, current.Down!.Down, current.Kind);
                PushIfValid(stack, current.Left!.Left, current.Kind);
                PushIfValid(stack, current.Right!.Right, current.Kind);
            }
        }

        private void PushIfValid(Stack<Day12Region> stack, Day12Region? neighbor, string kind)
        {
            if (neighbor != null && neighbor.Kind == kind && !VisitedRegions.Contains(neighbor))
            {
                stack.Push(neighbor);
            }
        }

    }
}
