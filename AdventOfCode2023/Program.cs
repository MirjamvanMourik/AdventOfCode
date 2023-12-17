// See https://aka.ms/new-console-template for more information
using AdventOfCode2023.Days;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("Advent of Code 2023\n");

#region Day1
// Day 1
GetAnswersAndPrintToConsole(new Day1());
#endregion

#region Day2
// Day 2
GetAnswersAndPrintToConsole(new Day2());
#endregion

#region Day3
// Day 3
GetAnswersAndPrintToConsole(new Day3());
#endregion

#region Day4
// Day 4
GetAnswersAndPrintToConsole(new Day4());
#endregion

#region Day6
// Day 6

GetAnswersAndPrintToConsole(new Day6());
#endregion

Console.ReadLine();

static void GetAnswersAndPrintToConsole(IDay day)
{
    Stopwatch stopwatch = new();

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"Day {day.Day}");

    stopwatch.Start();
    var result1 = day.GetFirstAnswer();
    var result2 = day.GetSecondAnswer();
    stopwatch.Stop();

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Result 1: {result1}");
    Console.WriteLine($"Result 2: {result2}");
    Console.WriteLine($"Time elapsed: {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine();
}