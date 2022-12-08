using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Extras
{
    public class Day7Item
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Day7Type Type { get; set; }
        public Day7Item Parent { get; set; }
        public List<Day7Item> Childeren { get; set; }

        public Day7Item()
        {
            Childeren = new List<Day7Item>();
        }

        public string ToString(string space)
        {
            var size = "";

            if (Type == Day7Type.File)
            {
                size = $" ({Size})";
            }

            var output = $"{space}-{Name}{size}\n";

            foreach (var item in Childeren)
            {
                output += item.ToString($" {space}");
            }

            return output;
        }
    }

    public enum Day7Type
    {
        File,
        Directory
    }
}
