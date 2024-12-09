namespace AdventOfCode2024.Extras
{
    public class Day8Frequency
    {
        public long X { get; set; }
        public long Y { get; set; }
        public string frequencyValue = string.Empty;

        public Day8Frequency(long x, long y, string frequencyValue)
        {
            X = x;
            Y = y;
            this.frequencyValue = frequencyValue;
        }
    }
}
