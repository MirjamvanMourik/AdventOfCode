using AdventOfCode2024.Extras;
using AdventOfCode2024.Shared;

namespace AdventOfCode2024.Tests
{
    [TestClass]
    public class Day6Tests
    {
        [TestMethod]
        public void SecondAnswerTest1()
        {
            var inputMap = @"....
#..#
.^#.";

            var input = InputSplitter.SplitLinesToString(inputMap);

            var grid = new Day6Grid();
            grid.CreateGrid(input);
            var result = grid.LookForPossibleObstacles();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void SecondAnswerTest2()
        {
            var inputMap = @".#................
..#...............
..................
.^................
..................";

            var input = InputSplitter.SplitLinesToString(inputMap);

            var grid = new Day6Grid();
            grid.CreateGrid(input);
            var result = grid.LookForPossibleObstacles();

            Assert.AreEqual(4, result);
        }
    }
}