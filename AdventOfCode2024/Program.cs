// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days;
using AdventOfCode2024.Helpers;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
ConsoleHelper.WriteFestive("Advent of Code 2024");
ConsoleHelper.AddChristmasTree();
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("by Mirjam van Mourik\n");

List<IDay> days = new()
{
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day7(),
};

// True for testing the current day - false for complete overview
var dayTesting = true;

if (dayTesting)
{
    // For daytesting
    GetAnswersAndPrintToConsole(days.Last());
}
else
{
    foreach (var day in days)
    {
        GetAnswersAndPrintToConsole(day);
    }
}

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