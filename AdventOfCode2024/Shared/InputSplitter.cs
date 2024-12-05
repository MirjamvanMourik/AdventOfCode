using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Shared
{
    public static class InputSplitter
    {
        public static Dictionary<int, List<long>> SplitIntoDictionary(string input)
        {
            var rows = SplitIntoRows(input);

            var dictionaryInput = new Dictionary<int, List<long>>
            {
                { 0, new List<long>() },
                { 1, new List<long>() }
            };

            foreach (var row in rows)
            {
                var rowData = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var key1 = long.Parse(rowData[0]);
                var key2 = long.Parse(rowData[1]);

                dictionaryInput[0].Add(key1);
                dictionaryInput[1].Add(key2);
            }

            return dictionaryInput;
        }

        public static List<List<long>> SplitLinesToLong(string input)
        {
            var rows = SplitIntoRows(input);

            var list = new List<List<long>>();

            foreach (var row in rows)
            {
                var rowData = row.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                list.Add(rowData);
            }

            return list;
        }

        public static List<List<string>> SplitLinesToString(string input)
        {
            var rows = SplitIntoRows(input);

            var list = new List<List<string>>();

            foreach (var row in rows)
            {
                var letters = new List<string>();

                foreach(var letter in row)
                {
                    letters.Add(letter.ToString());
                }
                
                list.Add(letters);
            }

            return list;
        }

        public static List<string> SplitMemory(string input, bool isFirst)
        {
            string firstPattern = @"mul\(\d{1,3},\d{1,3}\)";
            string secondPattern = @"mul\(\d{1,3},\d{1,3}\)|do(?:n't)?\(\)";

            var pattern = isFirst ? firstPattern : secondPattern;

            return Regex.Matches(input, pattern)
                .OfType<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        public static string[] SplitIntoRows(string input)
        {
            return input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
