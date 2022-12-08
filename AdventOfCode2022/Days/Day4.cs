using AdventOfCode2022.Input;
using AdventOfCode2022.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days
{
    public class Day4
    {
        public static int GetAmountOfCompleteOverlapsInPairs()
        {
            var conpleteOverlaps = 0;

            var pairs = Day4CleaningPairs.Input;

            var splitPairs = Splitter.SplitInput(pairs);

            foreach (var pair in splitPairs)
            {
                var elfRegions = pair.Split(",", StringSplitOptions.TrimEntries);

                var regionsFirstElf = GetRegionList(elfRegions[0]);
                var regionsSecondElf = GetRegionList(elfRegions[1]);

                var hasCompleteOverlap = CheckForCompleteOverlap(regionsFirstElf, regionsSecondElf);

                if (hasCompleteOverlap)
                {
                    conpleteOverlaps++;
                }
            }

            return conpleteOverlaps;
        }

        public static int GetAmountOfPartialOverlapsInPairs()
        {
            var partialOverlaps = 0;

            var pairs = Day4CleaningPairs.Input;

            var splitPairs = Splitter.SplitInput(pairs);

            foreach (var pair in splitPairs)
            {
                var elfRegions = pair.Split(",", StringSplitOptions.TrimEntries);

                var regionsFirstElf = GetRegionList(elfRegions[0]);
                var regionsSecondElf = GetRegionList(elfRegions[1]);

                var hasCompleteOverlap = CheckForPartialOverlap(regionsFirstElf, regionsSecondElf);

                if (hasCompleteOverlap)
                {
                    partialOverlaps++;
                }
            }

            return partialOverlaps;
        }

        private static bool CheckForCompleteOverlap(List<int> regionsFirstElf, List<int> regionsSecondElf)
        {
            if (regionsFirstElf[0] >= regionsSecondElf[0] && regionsFirstElf[1] <= regionsSecondElf[1])
            {
                return true;
            }

            if (regionsSecondElf[0] >= regionsFirstElf[0] && regionsSecondElf[1] <= regionsFirstElf[1])
            {
                return true;
            }

            return false;
        }

        private static bool CheckForPartialOverlap(List<int> regionsFirstElf, List<int> regionsSecondElf)
        {
            if (regionsFirstElf[0] >= regionsSecondElf[0] && regionsFirstElf[0] <= regionsSecondElf[1])
            {
                return true;
            }

            if (regionsSecondElf[0] >= regionsFirstElf[0] && regionsSecondElf[0] <= regionsFirstElf[1])
            {
                return true;
            }

            return false;
        }

        private static List<int> GetRegionList(string strRegion)
        {
            var intRegions = new List<int>();
            var regions = strRegion.Split("-", StringSplitOptions.TrimEntries);

            foreach (var region in regions)
            {
                intRegions.Add(int.Parse(region));
            }

            return intRegions;
        }
    }
}
