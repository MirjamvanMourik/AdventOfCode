// See https://aka.ms/new-console-template for more information
using AdventOfCode2024.Days;
using System;
using System.Diagnostics;

Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("Advent of Code 2024");
AddChristmasTree();
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

static void AddChristmasTree()
{
    // Set up colors
    ConsoleColor treeColor = ConsoleColor.DarkGreen;
    ConsoleColor redOrnament = ConsoleColor.Red;
    ConsoleColor yellowOrnament = ConsoleColor.Yellow;
    ConsoleColor logColor = ConsoleColor.White;

    // Define the tree rows
    string[] tree = {
            "        *",
            "       /|\\",
            "      /*|O\\",
            "     /*/|\\*\\",
            "    /X/O|*\\X\\",
            "   /*/X/|\\O\\*\\",
            "  /O/*/X|*\\X\\O\\",
            " /X/O/*/|\\O\\X\\*\\",
            "/X/O/X/|O|X\\*O\\X\\",
            "       |||",
            "       |||",
            "       |||"
        };

    // Output each row with appropriate colors
    foreach (string line in tree)
    {
        if (line.Contains("|||"))
        {
            Console.ForegroundColor = logColor;
            Console.Write(line);
            Console.WriteLine();
            continue;
        }

        foreach (char c in line)
        {
            // Set color based on the character
            if (c == 'X' || c == '/' || c == '|' || c == '\\')
            {
                Console.ForegroundColor = treeColor;
            }
            else if (c == 'O')
            {
                Console.ForegroundColor = redOrnament;
            }
            else if (c == '*')
            {
                Console.ForegroundColor = yellowOrnament;
            }
            else
            {
                Console.ResetColor();
            }

            // Write character
            Console.Write(c);
        }

        // Move to the next line
        Console.WriteLine();
    }

    // Reset color after finishing
    Console.ResetColor();
}