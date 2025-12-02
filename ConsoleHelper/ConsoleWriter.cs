namespace ConsoleHelper
{
    public static class ConsoleWriter
    {
        public static void WriteFestive(string input)
        {
            string text = $"{input}";
            bool useGreen = true;

            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c))
                {
                    Console.Write(c);
                }
                else
                {
                    Console.ForegroundColor = useGreen ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.Write(c);

                    useGreen = !useGreen;
                }
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        public static void AddChristmasTree()
        {
            ConsoleColor treeColor = ConsoleColor.DarkGreen;
            ConsoleColor redOrnament = ConsoleColor.Red;
            ConsoleColor yellowOrnament = ConsoleColor.Yellow;
            ConsoleColor logColor = ConsoleColor.White;

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

                    Console.Write(c);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}
