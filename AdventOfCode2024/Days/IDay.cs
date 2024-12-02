using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    public interface IDay
    {
        long Day { get; }
        string Title { get; }

        long GetFirstAnswer();

        long GetSecondAnswer();
    }
}