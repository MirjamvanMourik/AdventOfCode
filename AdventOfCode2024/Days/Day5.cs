using AdventOfCode2024.Extras;
using AdventOfCode2024.Input;
using AdventOfCode2024.Shared;
using System.Data;

namespace AdventOfCode2024.Days
{
    public class Day5 : IDay
    {
        long IDay.Day => 5;
        string IDay.Title => "Prlong Queue";

        private List<(long smaller, long larger)> Rules = new();
        private List<List<long>> Updates = new();

        public Day5()
        {
            var input = InputSplitter.SplitIntoRows(Day5PageOrdering.Input);
            AssignRulesAndUpdates(input);
        }

        public long GetFirstAnswer()
        {
            return Updates.Where(IsUpdateCorrect)
                          .Sum(update => update[update.Count / 2]);
        }

        public long GetSecondAnswer()
        {
            var updates = Updates.Where(update => !IsUpdateCorrect(update));
            var total = 0L;

            foreach (var incorrectUpdate in updates)
            {
                incorrectUpdate.Sort(new Day5Comparer(Rules));

                total += incorrectUpdate[incorrectUpdate.Count / 2];
            }

            return total;
        }

        private void AssignRulesAndUpdates(string[] input)
        {
            Rules = new List<(long smaller, long larger)>();
            Updates = new List<List<long>>();

            foreach (var row in input)
            {
                var splitValues = row.Split(new[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(long.Parse)
                                     .ToList();

                if (row.Contains('|'))
                {
                    Rules.Add((splitValues[0], splitValues[1]));
                }
                else
                {
                    Updates.Add(splitValues);
                }
            }
        }

        private bool IsUpdateCorrect(List<long> update)
        {
            var indexMap = update.Select((value, index) => (value, index))
                                 .ToDictionary(x => x.value, x => x.index);

            return Rules.All(rule =>
            {
                var (first, second) = (rule.smaller, rule.larger);
                return !(indexMap.TryGetValue(first, out var firstIndex) &&
                         indexMap.TryGetValue(second, out var secondIndex) &&
                         firstIndex > secondIndex);
            });
        }
    }
}
