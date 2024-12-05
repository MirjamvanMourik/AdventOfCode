namespace AdventOfCode2024.Extras
{
    class Day5Comparer : IComparer<long>
    {
        private readonly List<(long smaller, long larger)> _rules;

        public Day5Comparer(List<(long smaller, long larger)> rules)
        {
            _rules = rules;
        }

        public int Compare(long x, long y)
        {
            foreach (var (smaller, larger) in _rules)
            {
                if (smaller == x && larger == y)
                {
                    return -1;
                }

                if (smaller == y && larger == x)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
