using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Shared
{
    public static class InputSplitter
    {
        public static Dictionary<int, List<long>> Split(string input)
        {
            var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var dictionaryInput = new Dictionary<int, List<long>>
            {
                { 0, new List<long>() },
                { 1, new List<long>() }
            };

            foreach (var row in rows)
            {
                var rowData = row.Split("   ", StringSplitOptions.RemoveEmptyEntries);
                var key1 = long.Parse(rowData[0]);
                var key2 = long.Parse(rowData[1]);

                dictionaryInput[0].Add(key1);
                dictionaryInput[1].Add(key2);
            }

            return dictionaryInput;
        }
    }
}
