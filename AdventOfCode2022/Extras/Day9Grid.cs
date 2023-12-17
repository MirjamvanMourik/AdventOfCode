using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day9Grid
    {
        public Day9Vertex? FirstGridElement { get; set; }

        private int xSpan = 0;
        private int ySpan = 0;

        public Day9Vertex CreateGrid(List<Day9Motion> motionSeries)
        {
            var upAndDown = motionSeries.Where(m => m.Direction == "U" || m.Direction == "D").ToList();
            var yMax = GetMax(upAndDown);
            var yMin = GetMin(upAndDown);

            var rightAndLeft = motionSeries.Where(m => m.Direction == "R" || m.Direction == "L").ToList();
            var xMax = GetMax(rightAndLeft);
            var xMin = GetMin(rightAndLeft);

            var startingVertex = new Day9Vertex();

            FirstGridElement = new Day9Vertex()
            {
                VisitingState = VisitingState.Empty,
                VisitedByTail = false
            };

            var currentVertex = FirstGridElement;

            xSpan = xMax + (-xMin);
            ySpan = yMax + (-yMin);

            for (var i = 0; i <= xSpan; i++)
            {
                for (var j = 0; j <= ySpan; j++)
                {

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

                    if (i == -xMin && j == -yMin)
                    {
                        currentVertex.VisitingState = VisitingState.Both;
                        currentVertex.VisitedByTail = true;
                        startingVertex = currentVertex;
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

                    switch (currentVertex.VisitingState)
                    {
                        case VisitingState.Tail:
                            visitor = "T";
                            break;
                        case VisitingState.Head:
                            visitor = "H";
                            break;
                        case VisitingState.Both:
                            visitor = "B";
                            break;
                        case VisitingState.Empty:
                            visitor = "E";
                            break;
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

        public void PrintPositions(string header)
        {
            var headPos = string.Empty;
            var tailPos = string.Empty;
            var currentVertex = FirstGridElement;

            for (var i = 0; i <= ySpan; i++)
            {
                if (headPos != string.Empty && tailPos != string.Empty)
                {
                    continue;
                }

                for (var j = 0; j <= xSpan; j++)
                {
                    if (headPos != string.Empty && tailPos != string.Empty)
                    {
                        continue;
                    }

                    switch (currentVertex.VisitingState)
                    {
                        case VisitingState.Tail:
                            tailPos = $"x: {j}, y: {i}";
                            break;
                        case VisitingState.Head:
                            headPos = $"x: {j}, y: {i}";
                            break;
                        case VisitingState.Both:
                            tailPos = $"x: {j}, y: {i}";
                            headPos = $"x: {j}, y: {i}";
                            break;
                    }

                    currentVertex = currentVertex.Right;
                }

                currentVertex = FirstGridElement;

                for (var k = 0; k <= i; k++)
                {
                    currentVertex = currentVertex.Up;
                }
            }

            Console.WriteLine($"\n{header}");

            Console.WriteLine($"Head: {headPos}");
            Console.WriteLine($"Tail: {tailPos}");
        }
    }
}
