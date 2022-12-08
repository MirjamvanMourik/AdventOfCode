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
    public class Day7
    {
        private static readonly Day7Item fileTree = new();
        private static Day7Item currentDirectory = fileTree;
        private static readonly Dictionary<string, int> directorySizes = new();

        public static int GetSumOfTotalDirectorySizesWithMax(int maxSize)
        {
            var terminalOutput = Day7TerminalOutput.Input;

            var commands = Splitter.SplitInput(terminalOutput);

            CreateDirectoryAndFileTree(commands);

            GetDirectorySize(fileTree, fileTree.Name);

            var listWithMaxSizes = directorySizes.Where(item => item.Value <= maxSize).ToList();

            var total = 0;

            foreach (var item in listWithMaxSizes)
            {
                total += item.Value;
            }

            return total;
        }

        public static int GetSizeOfDirectoryToDelete(int updateSize)
        {
            var totalSize = 70000000;
            var sizeNeededForUpdate = updateSize;
            var usedSize = directorySizes["/"];

            var amountLeft = totalSize - usedSize;

            if (amountLeft < sizeNeededForUpdate)
            {
                var sizeToDelete = sizeNeededForUpdate - amountLeft;
                var possibleToDelete = new List<int>();

                foreach (var item in directorySizes)
                {
                    if (item.Value >= sizeToDelete)
                    {
                        possibleToDelete.Add(item.Value);
                    }
                }

                possibleToDelete.Sort();

                return possibleToDelete[0];
            }

            return 0;
        }

        private static int GetDirectorySize(Day7Item currentDirectory, string name)
        {
            var count = 0;

            foreach (var child in currentDirectory.Childeren)
            {
                if (child.Type == Day7Type.File)
                {
                    count += child.Size;
                }
                else
                {
                    count += GetDirectorySize(child, $"{name}/{child.Name}");
                }
            }

            directorySizes.Add(name, count);

            return count;
        }

        private static void CreateDirectoryAndFileTree(string[] commands)
        {
            foreach (var command in commands)
            {
                currentDirectory = ExecuteCommand(command, currentDirectory);
            }
        }

        private static Day7Item ExecuteCommand(string command, Day7Item currentDirectory)
        {
            if (string.IsNullOrEmpty(currentDirectory.Name))
            {
                return CreateDirectory(command, null);
            }
            else
            {
                var firstChar = command[0];

                if (firstChar == '$')
                {
                    if (command.Split(" ")[1] == "cd")
                    {
                        return GoToDirectory(command.Split(" ")[2], currentDirectory);
                    }
                    if (command.Split(" ")[1] == "ls")
                    {
                        return currentDirectory;
                    }
                }
                else if (firstChar == 'd')
                {
                   return CreateDirectory(command, currentDirectory);
                }
                else if (char.IsNumber(firstChar))
                {
                    return CreateFile(command, currentDirectory);
                }
            }

            throw new ArgumentException($"Command is not correct: {command}");
        }

        private static Day7Item GoToDirectory(string v, Day7Item currentDirectory)
        {
            if (v == "..")
            {
                return currentDirectory.Parent;
            }

            var directory = currentDirectory.Childeren.Where(i => i.Name == v && i.Type == Day7Type.Directory).FirstOrDefault();

            if (directory == default)
            {
                throw new ArgumentException($"Can't find directory {v} in {currentDirectory.Name}");
            }

            return directory;
        }

        private static Day7Item CreateDirectory(string command, Day7Item? currentDirectory)
        {
            if (currentDirectory == null)
            {
                fileTree.Name = command.Split(" ")[2];
                fileTree.Type = Day7Type.Directory;
                return fileTree;
            }

            var item = new Day7Item
            {
                Name = command.Split(" ")[1],
                Type = Day7Type.Directory,
                Parent = currentDirectory
            };

            currentDirectory.Childeren.Add(item);

            return currentDirectory;
        }

        private static Day7Item CreateFile(string command, Day7Item currentDirectory)
        {
            var item = new Day7Item
            {
                Name = command.Split(" ")[1],
                Size = int.Parse(command.Split(" ")[0]),
                Type = Day7Type.File,
                Parent = currentDirectory
            };

            currentDirectory.Childeren.Add(item);

            return currentDirectory;
        }
    }

    
}
