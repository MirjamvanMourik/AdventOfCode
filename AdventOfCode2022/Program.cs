// See https://aka.ms/new-console-template for more information
using AdventOfCode2022.Days;

Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("Advent of Code 2022\n");

#region Day1
// Day 1
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 1");

var resultd1e1 = Day1.GetHighestAmountOfCaloriesForOneElf();
var resultd1e2 = Day1.GetTotalAmountOfCaloriesForTopThree();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd1e1}");
Console.WriteLine($"Result 2: {resultd1e2}\n");
#endregion

#region Day2
// Day 2
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 2");

var resultd2e1 = Day2.GetTotalScoreWithStrategyGuide(false);
var resultd2e2 = Day2.GetTotalScoreWithStrategyGuide(true);

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd2e1}");
Console.WriteLine($"Result 2: {resultd2e2}\n");
#endregion

#region Day3
// Day 3
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 3");

var resultd3e1 = Day3.GetPrioSumOfWronglyPackedItems();
var resultd3e2 = Day3.GetPrioSumOfAllBadges();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd3e1}");
Console.WriteLine($"Result 2: {resultd3e2}\n");
#endregion

#region Day4
// Day 4
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 4");

var resultd4e1 = Day4.GetAmountOfCompleteOverlapsInPairs();
var resultd4e2 = Day4.GetAmountOfPartialOverlapsInPairs();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd4e1}");
Console.WriteLine($"Result 2: {resultd4e2}\n");
#endregion

#region Day5
// Day 5
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 5");

var resultd5e1 = Day5.GetCratesOnTopOfEachStackAfterRearranging(false);
var resultd5e2 = Day5.GetCratesOnTopOfEachStackAfterRearranging(true);

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd5e1}");
Console.WriteLine($"Result 2: {resultd5e2}\n");
#endregion

#region Day6
// Day 6
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 6");

var resultd6e1 = Day6.GetAmountOfCharactersToBeProcessedBeforeMarkerDetected();
var resultd6e2 = Day6.GetAmountOfCharactersToBeProcessedBeforeMessageDetected();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd6e1}");
Console.WriteLine($"Result 2: {resultd6e2}\n");
#endregion

#region Day7
// Day 7
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 7");

var resultd7e1 = Day7.GetSumOfTotalDirectorySizesWithMax(100000);
var resultd7e2 = Day7.GetSizeOfDirectoryToDelete(30000000);

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd7e1}");
Console.WriteLine($"Result 2: {resultd7e2}\n");
#endregion

#region Day8
// Day 8
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("Day 8");

var resultd8e1 = Day8.GetAmountOfVisbleTrees();
var resultd8e2 = Day8.GetMostScenicTreePosition();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine($"Result 1: {resultd8e1}");
Console.WriteLine($"Result 2: {resultd8e2}\n");
#endregion

Console.ReadLine();
