using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Extras
{
    public class Day4Vertex
    {
        public string Letter = string.Empty;
        public int XAmount = 0;
        public Day4Vertex Up { get; set; }
        public Day4Vertex Right { get; set; }
        public Day4Vertex Left { get; set; }
        public Day4Vertex Down { get; set; }

        public Day4Vertex(string letter) => Letter = letter;
    }
}
