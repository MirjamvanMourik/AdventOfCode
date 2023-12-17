using AdventOfCode2023.Extras;
using AdventOfCode2023.Input;
using AdventOfCode2023.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public interface IDay
    {
        long Day { get; }

        long GetFirstAnswer();

        long GetSecondAnswer();
    }
}
