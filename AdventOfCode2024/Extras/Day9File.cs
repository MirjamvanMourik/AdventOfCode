namespace AdventOfCode2024.Extras
{
    public class Day9File : IDay9Type
    {
        private readonly string Name = "file";

        public int AmountOfBlocks { get; set; }

        string IDay9Type.Name => Name;

        public Day9File(string name, char amountOfBlocks)
        {
            Name = name;
            AmountOfBlocks = int.Parse(amountOfBlocks.ToString());
        }
    }
}
