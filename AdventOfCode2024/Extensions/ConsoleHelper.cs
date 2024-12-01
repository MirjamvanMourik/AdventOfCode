using System;

namespace AdventOfCode2024.Extensions
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
    }
}
