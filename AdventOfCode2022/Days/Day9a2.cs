using AdventOfCode2022.Extras;
using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public static class Day9a2
    {
        private static readonly Day9Grid2 grid = new();
        private static Day9Vertex2 currentHeadVertex = new();
        private static Day9Vertex2 currentTailVertex = new();

        public static int GetAmountOfVisitedPositionsForTail()
        {
            var motionSeriesInput2 = Day9MotionSeries.Input;
            var motionSeriesInput = @"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

            var splitMotionSeriesInput = Splitter.SplitInput(motionSeriesInput);
            var motionSeries = CreateMotionSeries(splitMotionSeriesInput);

            currentHeadVertex = grid.CreateGrid(motionSeries);

            grid.PrintGrid("Starting grid");

            ExecuteMotionSeries(motionSeries);

            return grid.GetAmountOfPositionsVisitedByTail();
        }

        private static void ExecuteMotionSeries(List<Day9Motion> motionSeries)
        {
            foreach (var motion in motionSeries)
            {
                MoveHead(motion.Direction, motion.AmountOfSteps);

                grid.PrintGrid($"Go {motion.Direction} for {motion.AmountOfSteps} steps\n");
            }
        }

        private static void MoveHead(string headDirection, int amountOfSteps)
        {
            var head = currentHeadVertex.GetAndRemoveVisitor(VisitorKind.Head);

            switch (headDirection.ToLower())
            {
                case "u":
                    currentHeadVertex.Up.Visitors.Add(head);

                    currentHeadVertex = currentHeadVertex.Up;
                    break;

                case "r":
                    currentHeadVertex.Right.Visitors.Add(head);

                    currentHeadVertex = currentHeadVertex.Right;
                    break;

                case "d":
                    currentHeadVertex.Down.Visitors.Add(head);

                    currentHeadVertex = currentHeadVertex.Down;
                    break;

                case "l":
                    currentHeadVertex.Left.Visitors.Add(head);

                    currentHeadVertex = currentHeadVertex.Left;
                    break;
            }

            for (var i = 0; i < 9; i++)
            {
                if (i == 0)
                {
                    if (IsTailClose(i + 1, currentHeadVertex) == default)
                    {
                        currentTailVertex = FindTail(currentHeadVertex, i + 1, out string direction);

                        MoveTail(currentHeadVertex, i + 1, direction);
                    }
                    else
                    {
                        currentTailVertex = currentHeadVertex;
                    }
                }
                else
                {
                    if (IsTailClose(i + 1, currentTailVertex) == default)
                    {
                        currentTailVertex = FindTail(currentTailVertex, i + 1, out string direction);

                        MoveTail(currentTailVertex, i + 1, direction);
                    }
                }
            }

            if (amountOfSteps - 1 > 0)
            {
                MoveHead(headDirection, amountOfSteps - 1);
            }
        }

        private static void MoveTail(Day9Vertex2 vertextToStart, int tailNumber, string foundDirection)
        {
            var tail = currentTailVertex.GetAndRemoveVisitor(VisitorKind.Tail, tailNumber);

            // It needs to move into the opposite direction of the found direction. 
            switch (foundDirection)
            {
                case "u":
                    vertextToStart.Down.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Down;
                    break;

                case "r":
                    vertextToStart.Left.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Left;
                    break;

                case "d":
                    vertextToStart.Up.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Up;
                    break;

                case "l":
                    vertextToStart.Right.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Right;
                    break;

                case "ur":
                    vertextToStart.Down.Left.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Down.Left;
                    break;

                case "dr":
                    vertextToStart.Up.Left.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Up.Left;
                    break;

                case "dl":
                    vertextToStart.Up.Right.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Up.Right;
                    break;

                case "ul":
                    vertextToStart.Down.Right.AddVisitor(tail);
                    currentTailVertex = vertextToStart.Down.Right;
                    break;
            }
        }

        private static Day9Vertex2 FindTail(Day9Vertex2 vertextToStart,int tailNumber, out string direction)
        {
            // Check Up
            if (vertextToStart.Up?.Up?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "u";
                return vertextToStart.Up.Up;
            }

            // Check Right
            if (vertextToStart.Right?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "r";
                return vertextToStart.Right.Right;
            }

            // Check Down
            if (vertextToStart.Down?.Down?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "d";
                return vertextToStart.Down.Down;
            }

            // Check Left
            if (vertextToStart.Left?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "l";
                return vertextToStart.Left.Left;
            }

            // Check Diagonal
            if (vertextToStart.Up?.Up?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "ur";
                return vertextToStart.Up.Up.Right;
            }

            if (vertextToStart.Up?.Right?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "ur";
                return vertextToStart.Up.Right.Right;
            }

            if (vertextToStart.Down?.Down?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "dr";
                return vertextToStart.Down.Down.Right;
            }

            if (vertextToStart.Down?.Right?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "dr";
                return vertextToStart.Down.Right.Right;
            }

            if (vertextToStart.Up?.Up?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "ul";
                return vertextToStart.Up.Up.Left;
            }

            if (vertextToStart.Up?.Left?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "ul";
                return vertextToStart.Up.Left.Left;
            }

            if (vertextToStart.Down?.Down?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "dl";
                return vertextToStart.Down.Down.Left;
            }

            if (vertextToStart.Down?.Left?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                direction = "dl";
                return vertextToStart.Down.Left.Left;
            }

            throw new ArgumentException("Couldn't find the tail in the surrounding area of the grid.");
        }

        private static Day9Vertex2 IsTailClose(int tailNumber, Day9Vertex2 closeToVertex)
        {
            if (closeToVertex.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex;
            }
            else if (closeToVertex.Up?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Up;
            }
            else if (closeToVertex.Up?.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Up.Right;
            }
            else if (closeToVertex.Right?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Right;
            }
            else if (closeToVertex.Right?.Down?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Right.Down;
            }
            else if (closeToVertex.Down?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Down;
            }
            else if (closeToVertex.Down?.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Down.Left;
            }
            else if (closeToVertex.Left?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Left;
            }
            else if (closeToVertex.Left?.Up?.Visitors.Where(v => v.TailNumber == tailNumber).FirstOrDefault() != default)
            {
                return closeToVertex.Left.Up;
            }

            return default;
        }

        private static List<Day9Motion> CreateMotionSeries(string[] splitMotionSeries)
        {
            var motionSeries = new List<Day9Motion>();

            foreach (var motion in splitMotionSeries)
            {
                var splitMotion = motion.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                motionSeries.Add(
                    new Day9Motion()
                    {
                        Direction = splitMotion[0],
                        AmountOfSteps = int.Parse(splitMotion[1])
                    });
            }

            return motionSeries;
        }
    }
}
