using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Extras
{
    public class Day4Grid
    {
        public Day4Vertex[,]? Grid { get; set; }
        private int RowCount = 0;
        private int ColCount = 0;

        public void CreateGrid(List<List<string>> input)
        {
            RowCount = input.Count;
            ColCount = input[0].Count;

            Grid = new Day4Vertex[RowCount, ColCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    Grid[row, col] = new Day4Vertex(input[row][col]);
                }
            }

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    if (row > 0) Grid[row, col].Up = Grid[row - 1, col];
                    if (row < RowCount - 1) Grid[row, col].Down = Grid[row + 1, col];
                    if (col > 0) Grid[row, col].Left = Grid[row, col - 1];
                    if (col < ColCount - 1) Grid[row, col].Right = Grid[row, col + 1];
                }
            }
        }

        public int CalculateOccurences(string word)
        {
            if (Grid == null || string.IsNullOrEmpty(word))
            {
                return 0;
            }

            int foundWordAmount = 0;

            var directions = new[] { "u", "ur", "r", "rd", "d", "dl", "l", "lu" };

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var currentVertex = Grid[row, col];

                    foreach (var direction in directions)
                    {
                        foundWordAmount += FindWordOccurences(currentVertex, word, direction);
                    }
                }
            }

            return foundWordAmount;
        }

        public int CalculateXOccurences(string word)
        {
            if (Grid == null || string.IsNullOrEmpty(word))
            {
                return 0;
            }

            int foundWordAmount = 0;

            var directions = new[] { "ur", "rd", "dl", "lu" };

            for (int row = 0; row < RowCount; row++)
            {
                for (int col = 0; col < ColCount; col++)
                {
                    var currentVertex = Grid[row, col];

                    if (currentVertex.Letter == word[1].ToString())
                    {
                        foundWordAmount += FindXWordOccurences(currentVertex, word);
                    }
                }
            }

            return foundWordAmount;
        }

        private static int FindXWordOccurences(Day4Vertex? currentVertex, string word)
        {
            if (currentVertex == null || word.Length < 3)
                return 0;

            var upperLeft = currentVertex.Up?.Left?.Letter;
            var upperRight = currentVertex.Up?.Right?.Letter;
            var lowerLeft = currentVertex.Down?.Left?.Letter;
            var lowerRight = currentVertex.Down?.Right?.Letter;

            bool MatchesPattern(string? ul, string? ur, string? ll, string? lr)
            {
                return (ul == word[0].ToString() && lr == word[2].ToString() &&
                        ur == word[0].ToString() && ll == word[2].ToString()) ||
                       (ul == word[2].ToString() && lr == word[0].ToString() &&
                        ur == word[0].ToString() && ll == word[2].ToString()) ||
                       (ul == word[0].ToString() && lr == word[2].ToString() &&
                        ur == word[2].ToString() && ll == word[0].ToString()) ||
                       (ul == word[2].ToString() && lr == word[0].ToString() &&
                        ur == word[2].ToString() && ll == word[0].ToString());
            }

            return MatchesPattern(upperLeft, upperRight, lowerLeft, lowerRight) ? 1 : 0;
        }

        private int FindWordOccurences(Day4Vertex? currentVertex, string word, string direction, int index = 0)
        {
            if (currentVertex == null || index >= word.Length)
            {
                return 0;
            }

            if (currentVertex.Letter != word[index].ToString())
            {
                return 0;
            }

            if (index == word.Length - 1)
            {
                return 1;
            }

            Day4Vertex? nextVertex = direction switch
            {
                "u" => currentVertex.Up,
                "ur" => currentVertex.Up?.Right,
                "r" => currentVertex.Right,
                "rd" => currentVertex.Right?.Down,
                "d" => currentVertex.Down,
                "dl" => currentVertex.Down?.Left,
                "l" => currentVertex.Left,
                "lu" => currentVertex.Left?.Up,
                _ => null
            };
            
            int result = FindWordOccurences(nextVertex, word, direction, index + 1);

            return result;
        }
    }
}
