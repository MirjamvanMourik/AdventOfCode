using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Shared
{
    public static class Splitter
    {
        public static string[] SplitInput(string rounds)
        {
            return rounds.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
