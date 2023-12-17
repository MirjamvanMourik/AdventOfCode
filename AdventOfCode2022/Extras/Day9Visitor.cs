using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day9Visitor
    {
        public VisitorKind Kind { get; set; }
        public int TailNumber { get; set; }
    }

    public enum VisitorKind
    {
        Head,
        Tail
    }
}
