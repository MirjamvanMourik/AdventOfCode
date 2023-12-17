using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Extras
{
    public class Day3Grid
    {
        public Dictionary<long, Dictionary<long,string>> grid;

        public Day3Grid CreateGrid(string[] input)
        {
            grid = new Dictionary<long, Dictionary<long,string>>();

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (!grid.ContainsKey(i))
                    {
                        grid.Add(i, new Dictionary<long, string>());
                    }

                    grid[i].Add(j, input[i][j].ToString());
                }
            }

            return this;
        }
    }
}
