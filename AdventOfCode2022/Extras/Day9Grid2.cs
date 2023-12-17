using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day9Grid2
    {
        public Day9Vertex2? FirstGridElement { get; set; }

        private int xSpan = 0;
        private int ySpan = 0;

        public Day9Vertex2 CreateGrid(List<Day9Motion> motionSeries)
        {
            var upAndDown = motionSeries.Where(m => m.Direction == "U" || m.Direction == "D").ToList();
            var yMax = GetMax(upAndDown);
            var yMin = GetMin(upAndDown);

            var rightAndLeft = motionSeries.Where(m => m.Direction == "R" || m.Direction == "L").ToList();
            var xMax = GetMax(rightAndLeft);
            var xMin = GetMin(rightAndLeft);

            var startingVertex = new Day9Vertex2();

            FirstGridElement = new Day9Vertex2()
            {
                VisitedByTail = false
            };

            var currentVertex = FirstGridElement;

            xSpan = xMax + (-xMin);
            ySpan = yMax + (-yMin);

            for (var i = 0; i <= xSpan; i++)
            {
                for (var j = 0; j <= ySpan; j++)
                {
                    if (i == -xMin && j == -yMin)
                    {
                        for (var k = 9; k > 0; k--)
                        {
                            currentVertex.AddVisitor(new Day9Visitor { Kind = VisitorKind.Tail, TailNumber = k });
                        }

                        currentVertex.AddVisitor((new Day9Visitor { Kind = VisitorKind.Head }));

                        startingVertex = currentVertex;
                    }

                    currentVertex.Up = new()
                    {
                        Down = currentVertex
                    };

                    currentVertex = currentVertex.Up;

                    if (i != 0)
                    {
                        currentVertex.Left = currentVertex.Down.Left.Up;
                        currentVertex.Down.Left.Up.Right = currentVertex;
                    }
                }

                currentVertex = FirstGridElement;

                for (var k = 0; k <= i; k++)
                {
                    currentVertex.Right ??= new()
                    {
                        Left = currentVertex
                    };

                    currentVertex = currentVertex.Right;
                }
            }

            return startingVertex;
        }

        private static int GetMin(List<Day9Motion> rightAndLeft)
        {
            var lowest = 0;
            var current = 0;

            foreach (var motion in rightAndLeft)
            {
                if (motion.Direction == "U" || motion.Direction == "R")
                {
                    current += motion.AmountOfSteps;
                }

                if (motion.Direction == "D" || motion.Direction == "L")
                {
                    current -= motion.AmountOfSteps;

                    if (current < lowest)
                    {
                        lowest = current;
                    }
                }
            }

            return lowest;
        }

        private static int GetMax(List<Day9Motion> upAndDown)
        {
            var highest = 0;
            var current = 0;

            foreach (var motion in upAndDown)
            {
                if (motion.Direction == "U" || motion.Direction == "R")
                {
                    current += motion.AmountOfSteps;

                    if (current > highest)
                    {
                        highest = current;
                    }
                }

                if (motion.Direction == "D" || motion.Direction == "L")
                {
                    current -= motion.AmountOfSteps;
                }
            }

            return highest;
        }

        public int GetAmountOfPositionsVisitedByTail()
        {
            var amountOfPositionsVisitedByTail = 0;
            var currentVertex = FirstGridElement;

            for (var i = 0; i <= xSpan; i++)
            {
                for (var j = 0; j <= ySpan; j++)
                {
                    if (currentVertex.VisitedByTail)
                    {
                        amountOfPositionsVisitedByTail++;
                    }

                    currentVertex = currentVertex.Up;
                }

                currentVertex = FirstGridElement;

                for (var k = 0; k <= i; k++)
                {
                    currentVertex = currentVertex.Right;
                }
            }

            return amountOfPositionsVisitedByTail;
        }

        public void PrintGrid(string header)
        {
            var output = new List<string>();
            var currentVertex = FirstGridElement;

            for (var i = 0; i <= ySpan; i++)
            {
                var outputLine = string.Empty;

                for (var j = 0; j <= ySpan; j++)
                {
                    var visitor = string.Empty;

                    if (currentVertex.Visitors.Count == 0)
                    {
                        visitor = "E";
                    }
                    else
                    {
                        visitor = currentVertex.Visitors[^1].Kind switch
                        {
                            VisitorKind.Tail => currentVertex.Visitors[^1].TailNumber.ToString(),
                            VisitorKind.Head => "H",
                            _ => "E",
                        };
                    }

                    outputLine += $" {visitor} ";

                    currentVertex = currentVertex.Right;
                }

                output.Add(outputLine);

                currentVertex = FirstGridElement;

                for (var k = 0; k <= i; k++)
                {
                    currentVertex = currentVertex.Up;
                }
            }

            output.Reverse();

            Console.WriteLine($"\n{header}");

            foreach (var outputLine in output)
            {
                Console.WriteLine(outputLine);
            }
        }
    }
}
