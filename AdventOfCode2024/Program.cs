// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days;
using AdventOfCode2024.Helpers;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
ConsoleHelper.WriteFestive("Advent of Code 2024");
ConsoleHelper.AddChristmasTree();
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("by Mirjam van Mourik\n");

// True for testing the current day - false for complete overview
var dayTesting = true;

var correctAnswer = false;

while (!correctAnswer)
{
    Console.WriteLine("Do you only want to see the last day completed? (y/n)");

    var isDayTesting = Console.ReadLine()?.ToLower();

    if (isDayTesting != null && (isDayTesting == "y" || isDayTesting == "n"))
    {
        Console.WriteLine();
        correctAnswer = true;
        dayTesting = isDayTesting == "y";
        continue;
    }

    Console.WriteLine("That is an incorrect answer. Please try again. \n");
}

List<IDay> days = new()
{
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day7(),
    new Day8(),
};

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