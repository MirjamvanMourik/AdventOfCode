namespace AdventOfCode2024.Extras
{
    public class Day7Calculator
    {
        private readonly List<Func<long, long, long>> OperatorFuncs;
        private readonly List<Func<long, long, long>> SecondOperatorFuncs;

        public Day7Calculator()
        {
            static long Add(long x, long y) => x + y;
            static long Multiply(long x, long y) => x * y;
            static long Concatenate(long x, long y) => long.Parse(x.ToString() + y.ToString());

            OperatorFuncs = new List<Func<long, long, long>> { Add, Multiply };
            SecondOperatorFuncs = new List<Func<long, long, long>>(OperatorFuncs) { Concatenate };
        }

        public long CaculateAndDoSomeMagic(List<Day7Equation> testEquations, bool isSecondPart = false)
        {
            var netTotal = 0L;
            var operatorPool = isSecondPart ? SecondOperatorFuncs : OperatorFuncs;

            foreach (var equation in testEquations)
            {
                var operatorsCombinations = GenerateOperatorCombinations(operatorPool, equation.Numbers.Count - 1);

                foreach (var operatorList in operatorsCombinations)
                {
                    var equationTotal = CalculateTotal(equation.Numbers, operatorList);
                    if (equationTotal == equation.Total)
                    {
                        netTotal += equationTotal;
                        break;
                    }
                }
            }

            return netTotal;
        }

        private static long CalculateTotal(IReadOnlyList<long> numbers, IReadOnlyList<Func<long, long, long>> operators)
        {
            var total = numbers[0];

            for (var i = 0; i < operators.Count; i++)
            {
                total = operators[i](total, numbers[i + 1]);
            }

            return total;
        }

        private static List<List<Func<long, long, long>>> GenerateOperatorCombinations(
            List<Func<long, long, long>> operators, int length)
        {
            if (length == 0)
            {
                return new List<List<Func<long, long, long>>> { new() };
            }

            var results = new List<List<Func<long, long, long>>>();

            void Backtrack(List<Func<long, long, long>> current)
            {
                if (current.Count == length)
                {
                    results.Add(new List<Func<long, long, long>>(current));
                    return;
                }

                foreach (var op in operators)
                {
                    current.Add(op);
                    Backtrack(current);
                    current.RemoveAt(current.Count - 1);
                }
            }

            Backtrack(new List<Func<long, long, long>>());

            return results;
        }
    }
}
