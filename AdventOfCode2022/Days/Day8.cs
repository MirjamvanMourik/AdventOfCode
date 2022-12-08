using AdventOfCode2022.Extras;
using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day8
    {
        private static readonly Day8Tree firstTree = new();
        private static Day8Tree currentTree = new();

        public static int GetAmountOfVisbleTrees()
        {
            //var trees = Day8Trees.Input;
            var treesInput = @"30373
25512
65332
33549
35390";

            var trees = Splitter.SplitInput(treesInput);
            var splitTrees = SplitTrees(trees);

            CreateForest(splitTrees);

            currentTree = firstTree;

            for (var i = 0; i < splitTrees.Count; i++)
            {
                var rowOfTrees = string.Empty;

                for (var j = 0; j < splitTrees[i].Count; j++)
                {
                    rowOfTrees += currentTree.Height;

                    if (j == splitTrees[i].Count - 1)
                    {
                        currentTree = firstTree;

                        for (var a = 0; a < i + 1; a++)
                        {
                            currentTree = currentTree.Down;
                        }

                        continue;
                    }

                    Console.WriteLine(rowOfTrees);

                    GoRight(1);
                }
            }

            return 0;
        }

        private static void CreateForest(List<List<int>> splitTrees)
        {
            for (var i = 0; i < splitTrees.Count; i++)
            {
                for (var j = 0; j < splitTrees[i].Count; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        currentTree = firstTree;

                        firstTree.Height = splitTrees[i][j];
                        firstTree.Right = new Day8Tree
                        {
                            Height = splitTrees[i][j+1],
                            Left = firstTree
                        };
                        firstTree.Down = new Day8Tree
                        {
                            Height = splitTrees[i + 1][j],
                            Up = firstTree
                        };

                        currentTree = currentTree.Right;
                        continue;
                    }

                    if (i != splitTrees.Count - 1 && j != splitTrees[i].Count - 1) 
                    {
                        currentTree.Right = new Day8Tree
                        {
                            Height = splitTrees[i][j + 1],
                            Left = currentTree
                        };
                        currentTree.Down = new Day8Tree
                        {
                            Height = splitTrees[i + 1][j],
                            Up = currentTree
                        };

                        currentTree = currentTree.Right;
                        continue;
                    }

                    if (j == splitTrees[i].Count - 1 && i != splitTrees.Count - 1)
                    {
                        currentTree.Down = new Day8Tree
                        {
                            Height = splitTrees[i + 1][j],
                            Up = currentTree
                        };

                        currentTree = firstTree;

                        for (var a = 0; a < i + 1; a++)
                        {
                            currentTree = currentTree.Down;
                        }

                        continue;
                    }
                }
            }
        }

        private static void GoRight(int amountOfSteps)
        {
            if (amountOfSteps != 0)
            {
                if (currentTree.Right == null)
                {
                    throw new ArgumentException("You can't go right here.");
                }
                else
                {
                    currentTree = currentTree.Right;

                    GoRight(amountOfSteps - 1);
                }
            }
        }

        private static void GoLeft(int amountOfSteps)
        {
            if (amountOfSteps != 0)
            {
                if (currentTree.Left == null)
                {
                    throw new ArgumentException("You can't go left here.");
                }
                else
                {
                    currentTree = currentTree.Left;

                    GoLeft(amountOfSteps - 1);
                }
            }
        }

        private static void GoDown(int amountOfSteps)
        {
            if (amountOfSteps != 0)
            {
                if (currentTree.Down == null)
                {
                    throw new ArgumentException("You can't go down here.");
                }
                else
                {
                    currentTree = currentTree.Down;

                    GoDown(amountOfSteps - 1);
                }
            }
        }

        private static void GoUp(int amountOfSteps)
        {
            if (amountOfSteps != 0)
            {
                if (currentTree.Up == null)
                {
                    throw new ArgumentException("You can't go up here.");
                }
                else
                {
                    currentTree = currentTree.Up;

                    GoUp(amountOfSteps - 1);
                }
            }
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
