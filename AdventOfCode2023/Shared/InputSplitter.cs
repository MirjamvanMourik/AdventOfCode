using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Shared
{
    public static class InputSplitter
    {
        public static Dictionary<string, string[]> Split(string input)
        {
            var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var dictionaryInput = new Dictionary<string, string[]>();

            foreach (var row in rows)
            {
                var rowData = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var key = rowData[0].Replace(":", "").ToLower();
                var value = rowData.Skip(1).ToArray();

                dictionaryInput.Add(key, value);
            }

            return dictionaryInput;
        }

        public static Dictionary<string, List<string>> SplitForOneRace(string input)
        {
            var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var dictionaryInput = new Dictionary<string, List<string>>();

            foreach (var row in rows)
            {
                var rowData = row.Replace(" ","");
                var seperatedData = rowData.Split(":", StringSplitOptions.RemoveEmptyEntries);
                var key = seperatedData[0].ToLower();
                var value = new List<string>() {
                    seperatedData[1] };

                dictionaryInput.Add(key, value);
            }

            return dictionaryInput;
        }

        public static string[] SplitPerLine(string input)
        {
            var rows = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return rows;
        }
    }
}
