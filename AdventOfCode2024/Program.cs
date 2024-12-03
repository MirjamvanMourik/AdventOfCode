// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days;
using AdventOfCode2024.Helpers;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
ConsoleHelper.WriteFestive("Advent of Code 2024");
ConsoleHelper.AddChristmasTree();
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("by Mirjam van Mourik\n");

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

Console.ReadLine();

static void GetAnswersAndPrintToConsole(IDay day)
{
    Stopwatch stopwatch = new();

    Console.ForegroundColor = ConsoleColor.DarkGreen;
    ConsoleHelper.WriteFestive($"Day {day.Day} - {day.Title}");

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