namespace AdventOfCode2024.Extras
{
    public class Day12SpaceBetween
    {
        public bool isFence = false;

        public Day12SpaceBetween() { }

        public Day12Region? Up { get; set; }
        public Day12Region? Right { get; set; }
        public Day12Region? Left { get; set; }
        public Day12Region? Down { get; set; }
    }
}
