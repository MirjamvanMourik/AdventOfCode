using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day9Vertex
    {
        public VisitingState VisitingState { get; set; }
        public Day9Vertex Up { get; set; }
        public Day9Vertex Right { get; set; }
        public Day9Vertex Left { get; set; }
        public Day9Vertex Down { get; set; }
        public bool VisitedByTail = false;

        public Day9Vertex()
        {
            VisitingState = VisitingState.Empty;
        }
    }

    public enum VisitingState
    {
        Empty,
        Head,
        Tail,
        Both
    }
}
