using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day8Tree
    {
        public int Height { get; set; }
        public Day8Tree? Up { get; set; }
        public Day8Tree? Right { get; set; }
        public Day8Tree? Down { get; set; }
        public Day8Tree? Left { get; set; }

        public Day8Tree()
        {
            Height = -1;
        }
    }
}
