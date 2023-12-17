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
    public static class Day9
    {
        private static readonly Day9Grid grid = new();
        private static Day9Vertex currentHeadVertex = new();

        public static int GetAmountOfVisitedPositionsForTail()
        {
            var motionSeriesInput = Day9MotionSeries.Input;
            var motionSeriesInput2 = @"R 4
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

           //grid.PrintPositions("Starting positions");
           //grid.PrintGrid("Starting grid");

            ExecuteMotionSeries(motionSeries);

            return grid.GetAmountOfPositionsVisitedByTail();
        }

        private static void ExecuteMotionSeries(List<Day9Motion> motionSeries)
        {
            foreach (var motion in motionSeries)
            {
                MoveHead(motion.Direction, motion.AmountOfSteps);

                //grid.PrintPositions($"Go {motion.Direction} for {motion.AmountOfSteps} steps");
                //grid.PrintGrid($"Go {motion.Direction} for {motion.AmountOfSteps} steps\n");
            }
        }

        private static void MoveHead(string headDirection, int amountOfSteps)
        {
            currentHeadVertex.VisitingState = 
                (currentHeadVertex.VisitingState == VisitingState.Both) ? VisitingState.Tail : VisitingState.Empty;

            switch (headDirection.ToLower())
            {
                case "u":
                    currentHeadVertex.Up.VisitingState =
                        (currentHeadVertex.Up.VisitingState == VisitingState.Tail) ? VisitingState.Both : VisitingState.Head;

                    currentHeadVertex = currentHeadVertex.Up;
                    break;

                case "r":
                    currentHeadVertex.Right.VisitingState =
                        (currentHeadVertex.Right.VisitingState == VisitingState.Tail) ? VisitingState.Both : VisitingState.Head;

                    currentHeadVertex = currentHeadVertex.Right;
                    break;

                case "d":
                    currentHeadVertex.Down.VisitingState =
                        (currentHeadVertex.Down.VisitingState == VisitingState.Tail) ? VisitingState.Both : VisitingState.Head;

                    currentHeadVertex = currentHeadVertex.Down;
                    break;

                case "l":
                    currentHeadVertex.Left.VisitingState =
                        (currentHeadVertex.Left.VisitingState == VisitingState.Tail) ? VisitingState.Both : VisitingState.Head;

                    currentHeadVertex = currentHeadVertex.Left;
                    break;
            }
            
            
            if (!IsTailClose())
            {
                var tail = FindTail(out string direction);

                MoveTail(tail, direction);
            }

            if (amountOfSteps - 1 > 0)
            {
                MoveHead(headDirection, amountOfSteps - 1);
            }
        }

        private static void MoveTail(Day9Vertex tail, string foundDirection)
        {
            tail.VisitingState = VisitingState.Empty;

            // It needs to move into the opposite direction of the found direction. 
            switch (foundDirection)
            {
                case "d":
                    tail.Up.VisitingState = VisitingState.Tail;
                    tail.Up.VisitedByTail = true;
                    break;

                case "l":
                    tail.Right.VisitingState = VisitingState.Tail;
                    tail.Right.VisitedByTail = true;
                    break;

                case "u":
                    tail.Down.VisitingState = VisitingState.Tail;
                    tail.Down.VisitedByTail = true;
                    break;

                case "r":
                    tail.Left.VisitingState = VisitingState.Tail;
                    tail.Left.VisitedByTail = true;
                    break;

                case "dl":
                    tail.Up.Right.VisitingState = VisitingState.Tail;
                    tail.Up.Right.VisitedByTail = true;
                    break;

                case "ul":
                    tail.Down.Right.VisitingState = VisitingState.Tail;
                    tail.Down.Right.VisitedByTail = true;
                    break;

                case "ur":
                    tail.Down.Left.VisitingState = VisitingState.Tail;
                    tail.Down.Left.VisitedByTail = true;
                    break;

                case "dr":
                    tail.Up.Left.VisitingState = VisitingState.Tail;
                    tail.Up.Left.VisitedByTail = true;
                    break;
            }
        }

        private static Day9Vertex FindTail(out string direction)
        {
            // Check Up
            if (currentHeadVertex.Up?.Up?.VisitingState == VisitingState.Tail)
            {
                direction = "u";
                return currentHeadVertex.Up.Up;
            }

            // Check Right
            if (currentHeadVertex.Right?.Right?.VisitingState == VisitingState.Tail)
            {
                direction = "r";
                return currentHeadVertex.Right.Right;
            }

            // Check Down
            if (currentHeadVertex.Down?.Down?.VisitingState == VisitingState.Tail)
            {
                direction = "d";
                return currentHeadVertex.Down.Down;
            }

            // Check Left
            if (currentHeadVertex.Left?.Left?.VisitingState == VisitingState.Tail)
            {
                direction = "l";
                return currentHeadVertex.Left.Left;
            }

            // Check Diagonal
            if (currentHeadVertex.Up?.Up?.Right?.VisitingState == VisitingState.Tail)
            {
                direction = "ur";
                return currentHeadVertex.Up.Up.Right;
            }

            if (currentHeadVertex.Up?.Right?.Right?.VisitingState == VisitingState.Tail)
            {
                direction = "ur";
                return currentHeadVertex.Up.Right.Right;
            }

            if (currentHeadVertex.Down?.Down?.Right?.VisitingState == VisitingState.Tail)
            {
                direction = "dr";
                return currentHeadVertex.Down.Down.Right;
            }

            if (currentHeadVertex.Down?.Right?.Right?.VisitingState == VisitingState.Tail)
            {
                direction = "dr";
                return currentHeadVertex.Down.Right.Right;
            }

            if (currentHeadVertex.Up?.Up?.Left?.VisitingState == VisitingState.Tail)
            {
                direction = "ul";
                return currentHeadVertex.Up.Up.Left;
            }

            if (currentHeadVertex.Up?.Left?.Left?.VisitingState == VisitingState.Tail)
            {
                direction = "ul";
                return currentHeadVertex.Up.Left.Left;
            }

            if (currentHeadVertex.Down?.Down?.Left?.VisitingState == VisitingState.Tail)
            {
                direction = "dl";
                return currentHeadVertex.Down.Down.Left;
            }

            if (currentHeadVertex.Down?.Left?.Left?.VisitingState == VisitingState.Tail)
            {
                direction = "dl";
                return currentHeadVertex.Down.Left.Left;
            }

            throw new ArgumentException("Couldn't find the tail in the surrounding area of the grid.");
        }

        private static bool IsTailClose()
        {
            if (currentHeadVertex.VisitingState == VisitingState.Both
                || currentHeadVertex.Up?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Up?.Right?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Right?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Right?.Down?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Down?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Down?.Left?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Left?.VisitingState == VisitingState.Tail
                || currentHeadVertex.Left?.Up?.VisitingState == VisitingState.Tail)
            {
                return true;
            }

            return false;
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
