using AdventOfCode2025.Days;
using ConsoleHelper;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
ConsoleWriter.WriteFestive("Advent of Code 2025");
ConsoleWriter.AddChristmasTree();
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
    new Day6()
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
    ConsoleWriter.WriteFestive($"Day {day.Day} - {day.Title}");

    stopwatch.Start();
    var result1 = day.GetFirstAnswer();
    stopwatch.Stop();
    var time1 = stopwatch.ElapsedMilliseconds;

    stopwatch.Restart();
    var result2 = day.GetSecondAnswer();
    stopwatch.Stop();
    var time2 = stopwatch.ElapsedMilliseconds;

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Result 1: {result1}");
    Console.WriteLine($"Time elapsed 1: {time1} ms");
    Console.WriteLine($"Result 2: {result2}");
    Console.WriteLine($"Time elapsed 2: {time2} ms");
    Console.WriteLine();
}