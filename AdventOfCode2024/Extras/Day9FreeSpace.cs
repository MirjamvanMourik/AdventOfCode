namespace AdventOfCode2024.Extras
{
    public class Day9FreeSpace : IDay9Type
    {
        private readonly string Name = "free";

        public int AmountOfBlocks { get; set; }

        string IDay9Type.Name => Name;

        public Day9FreeSpace(string name, char amountOfBlocks)
        {
            Name = name;
            AmountOfBlocks = int.Parse(amountOfBlocks.ToString());
        }
    }
}
