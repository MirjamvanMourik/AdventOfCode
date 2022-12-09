using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Diagnostics;

namespace AdventOfCode2022.Days
{
    public class Day8
    {
        private static List<List<int>> forest = new();

        public static int GetAmountOfVisbleTrees()
        {
            var treesInput = Day8Trees.Input;
            var treesInput2 = @"30373
25512
65332
33549
35390";

            var trees = Splitter.SplitInput(treesInput);
            forest = SplitTrees(trees);

            var visibleTreesCount = 0;

            for (var i = 0; i < forest.Count; i++)
            {
                for (var j = 0; j < forest[i].Count; j++)
                {
                    if (IsTreeVisible(forest[i][j], i, j))
                    {
                        visibleTreesCount++;
                    }
                }
            }

            return visibleTreesCount;
        }

        public static int GetMostScenicTreePosition()
        {
            var treesInput = Day8Trees.Input;
            var treesInput2 = @"30373
25512
65332
33549
35390";

            var trees = Splitter.SplitInput(treesInput);
            forest = SplitTrees(trees);

            var mostScenicTreeScore = 0;

            for (var i = 0; i < forest.Count; i++)
            {
                for (var j = 0; j < forest[i].Count; j++)
                {
                    if (GetScenicTreeScore(forest[i][j], i, j) > mostScenicTreeScore)
                    {
                        mostScenicTreeScore = GetScenicTreeScore(forest[i][j], i, j);
                    }
                }
            }

            return mostScenicTreeScore;
        }

        private static int GetScenicTreeScore(int height, int row, int column)
        {
            var scores = new List<int>();
            var hasHigherTree = false;

            // Up
            for (var i = row - 1; i >= 0; i--)
            {
                if (forest[i][column] >= height)
                {
                    scores.Add(row - i);
                    hasHigherTree = true;
                    break;
                }
            }

            if (!hasHigherTree)
            {
                scores.Add(row);
            }
            else
            {
                hasHigherTree = false;
            }

            // Down
            for (var i = row + 1; i < forest.Count; i++)
            {
                if (forest[i][column] >= height)
                {
                    scores.Add(i - row);
                    hasHigherTree = true;
                    break;
                }
            }

            if (!hasHigherTree)
            {
                scores.Add((forest.Count - 1) - row);
            }
            else
            {
                hasHigherTree = false;
            }

            // Left
            for (var i = column - 1; i >= 0; i--)
            {
                if (forest[row][i] >= height)
                {
                    scores.Add(column - i);
                    hasHigherTree = true;
                    break;
                }
            }

            if (!hasHigherTree)
            {
                scores.Add(column);
            }
            else
            {
                hasHigherTree = false;
            }

            // Right
            for (var i = column + 1; i < forest[row].Count; i++)
            {
                if (forest[row][i] >= height)
                {
                    scores.Add(i - column);
                    hasHigherTree = true;
                    break;
                }
            }

            if (!hasHigherTree)
            {
                scores.Add((forest[row].Count - 1) - column);
            }

            // Calculate scenic score
            var calculatedScore = 0;
            for (var i = 0; i < scores.Count; i++)
            {
                if (i == 0)
                {
                    calculatedScore = scores[i];
                }
                else
                {
                    calculatedScore *= scores[i];
                }
            }

            return calculatedScore;
        }

        public static bool IsTreeVisible(int height, int row, int column)
        {
            if (row == 0 || row == forest.Count - 1 || column == 0 || column == forest[row].Count - 1)
            {
                return true;
            }

            if (!HasLargerTreeInLeftRow(height, row, column)
                || !HasLargerTreeInRightRow(height, row, column)
                || !HasLargerTreeInUpperColumn(height, row, column)
                || !HasLargerTreeILowerColumn(height, row, column))
            {
                return true;
            }

            return false;
        }

        private static bool HasLargerTreeInLeftRow(int height, int row, int column)
        {
            for (var i = 0; i < row; i++)
            {
                if (forest[i][column] >= height)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasLargerTreeInRightRow(int height, int row, int column)
        {
            for (var i = forest.Count - 1; i > row; i--)
            {
                if (forest[i][column] >= height)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasLargerTreeInUpperColumn(int height, int row, int column)
        {
            for (var i = 0; i < column; i++)
            {
                if (forest[row][i] >= height)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasLargerTreeILowerColumn(int height, int row, int column)
        {
            for (var i = forest[row].Count - 1; i > column; i--)
            {
                if (forest[row][i] >= height)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<List<int>> SplitTrees(string[] trees)
        {
            var treeList = new List<List<int>>();

            foreach (var row in trees)
            {
                var splitTrees = new List<int>();

                foreach (var tree in row)
                {
                    splitTrees.Add(int.Parse(tree.ToString()));
                }

                treeList.Add(splitTrees);
            }

            return treeList;
        }
    }
}
