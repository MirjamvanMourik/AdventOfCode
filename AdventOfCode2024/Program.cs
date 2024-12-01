// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days;
using System;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("Advent of Code 2024");
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("        *\r");
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine("       /|\\\r\n      /*|O\\\r\n     /*/|\\*\\\r\n    /X/O|*\\X\\\r\n   /*/X/|\\O\\*\\\r\n  /O/*/X|*\\XO\\\r\n /X/O/*/|\\O\\X*\\\r\n/__|__|_|__|__\\\r\n      |||\r\n      |||\r\n      |||");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("by Mirjam van Mourik\n");

#region Day1
// Day 1
GetAnswersAndPrintToConsole(new Day1());
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