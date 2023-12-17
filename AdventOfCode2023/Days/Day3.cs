using AdventOfCode2023.Extras;
using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public class Day3 : IDay
    {
        long IDay.Day => 3;

        public long GetFirstAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day3Schematics.Input);
            var grid = new Day3Grid().CreateGrid(input);

            return GetEngineSchematicNumbersTotal(grid.grid, false);
        }

        public long GetSecondAnswer()
        {
            var input = InputSplitter.SplitPerLine(Day3Schematics.Input);
            var grid = new Day3Grid().CreateGrid(input);

            return GetEngineSchematicNumbersTotal(grid.grid, true);
        }

        private static long GetEngineSchematicNumbersTotal(Dictionary<long, Dictionary<long, string>> grid, bool withGears)
        {
            long schematicsNumbersTotal = 0;
            var gears = new Dictionary<string, List<long>>();

            for (long i = 0; i < grid.Count; i++)
            {
                var completeNumberLocations = new List<NumberLocation>();
                var completeNumber = string.Empty;
                long rowTotal = 0;
                string rowAdjacent = $"Rij {i + 1}:";

                for (long j = 0; j < grid[i].Count; j++)
                {
                    if (int.TryParse(grid[i][j], out int number))
                    {
                        completeNumber += number;
                        completeNumberLocations.Add(new NumberLocation(j, i));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(completeNumber))
                        {
                            if (HasAdjacentSymbol(completeNumberLocations, grid, out Symbol symbol))
                            {
                                if (withGears)
                                {
                                    if (symbol.value == "*")
                                    {
                                        if (!gears.ContainsKey($"{symbol.x}-{symbol.y}"))
                                        {
                                            gears.Add($"{symbol.x}-{symbol.y}", new List<long>());
                                        }

                                        gears[$"{symbol.x}-{symbol.y}"].Add(long.Parse(completeNumber));
                                    }
                                }

                                rowAdjacent += $" [{completeNumber}: {symbol.value}]";
                                rowTotal += long.Parse(completeNumber);
                            }

                            completeNumber = string.Empty;
                            completeNumberLocations = new();
                        }
                    }

                    if (j == grid[i].Count - 1)
                    {
                        if (!string.IsNullOrEmpty(completeNumber))
                        {
                            if (HasAdjacentSymbol(completeNumberLocations, grid, out Symbol symbol))
                            {
                                rowAdjacent += $" [{completeNumber}: {symbol}]";
                                rowTotal += long.Parse(completeNumber);

                                if (withGears)
                                {
                                    if (symbol.value == "*")
                                    {
                                        if (!gears.ContainsKey($"{symbol.x}-{symbol.y}"))
                                        {
                                            gears.Add($"{symbol.x}-{symbol.y}", new List<long>());
                                        }

                                        gears[$"{symbol.x}-{symbol.y}"].Add(long.Parse(completeNumber));
                                    }
                                }
                            }

                            completeNumber = string.Empty;
                            completeNumberLocations = new();
                        }
                    }
                }

                //Console.WriteLine(rowAdjacent);

                schematicsNumbersTotal += rowTotal;
            }

            if (withGears)
            {
                long total = 0;

                foreach (var gear in gears)
                {
                    if (gear.Value.Count == 2)
                    {
                        total += gear.Value[0] * gear.Value[1];
                    }
                }

                return total;
            }

            return schematicsNumbersTotal;
        }

        private static bool HasAdjacentSymbol(List<NumberLocation> completeNumberLocations, Dictionary<long, Dictionary<long, string>> grid, out Symbol symbol)
        {
            foreach (var location in completeNumberLocations)
            {
                if (location.x != 0)
                {
                    if (IsSymbol(grid[location.y][location.x - 1]))
                    {
                        symbol = new Symbol(location.y, location.x - 1, grid[location.y][location.x - 1]);
                        return true;
                    }

                    if (location.y != 0)
                    {
                        if (IsSymbol(grid[location.y - 1][location.x - 1]))
                        {
                            symbol = new Symbol(location.y - 1, location.x - 1, grid[location.y - 1][location.x - 1]);
                            return true;
                        }
                    }

                    if (location.y != grid.Count - 1)
                    {
                        if (IsSymbol(grid[location.y + 1][location.x - 1]))
                        {
                            symbol = new Symbol(location.y + 1, location.x - 1, grid[location.y + 1][location.x - 1]);
                            return true;
                        }
                    }
                }

                if (location.x != grid[location.y].Count - 1)
                {
                    if (IsSymbol(grid[location.y][location.x + 1]))
                    {
                        symbol = new Symbol(location.y, location.x + 1, grid[location.y][location.x + 1]);
                        return true;
                    }

                    if (location.y != 0)
                    {
                        if (IsSymbol(grid[location.y - 1][location.x + 1]))
                        {
                            symbol = new Symbol(location.y - 1, location.x + 1, grid[location.y - 1][location.x + 1]);
                            return true;
                        }
                    }

                    if (location.y != grid.Count - 1)
                    {
                        if (IsSymbol(grid[location.y + 1][location.x + 1]))
                        {
                            symbol = new Symbol(location.y + 1, location.x + 1, grid[location.y + 1][location.x + 1]);
                            return true;
                        }
                    }
                }

                if (location.y != 0)
                {
                    if(IsSymbol(grid[location.y - 1][location.x]))
                    {
                        symbol = new Symbol(location.y - 1, location.x, grid[location.y - 1][location.x]);
                        return true;
                    }
                }

                if (location.y != grid.Count - 1)
                {
                    if (IsSymbol(grid[location.y + 1][location.x]))
                    {
                        symbol = new Symbol(location.y + 1, location.x, grid[location.y + 1][location.x]);
                        return true;
                    }
                }
            }

            symbol = new Symbol(0,0,string.Empty);
            return false;
        }

        private static bool IsSymbol(string character)
        {
            return character != "." && !int.TryParse(character, out int _);
        }
    }

    internal class NumberLocation
    {
        public long x;
        public long y;

        public NumberLocation(long x, long y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class Symbol
    {
        public long x;
        public long y;
        public string value;

        public Symbol(long y, long x, string value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }
}
