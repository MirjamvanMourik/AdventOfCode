using System;

namespace AdventOfCode2024.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteFestive(string input)
        {
            string text = $"{input}";
            bool useGreen = true; // Start with green for the first non-space character

            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c))
                {
                    // Write whitespace without changing the color or toggling
                    Console.Write(c);
                }
                else
                {
                    // Alternate colors
                    Console.ForegroundColor = useGreen ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write(c);

                    // Toggle color for the next non-space character
                    useGreen = !useGreen;
                }
            }

            // Reset console color to default
            Console.ResetColor();

            // Add a newline for better formatting
            Console.WriteLine();
        }

        public static void AddChristmasTree()
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
    }
}
