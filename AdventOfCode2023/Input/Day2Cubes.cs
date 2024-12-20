﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Input
{
    public static class Day2Cubes
    {
        public static readonly string Example = @"
    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        public static readonly string Input = @"
Game 1: 3 blue, 2 green, 6 red; 17 green, 4 red, 8 blue; 2 red, 1 green, 10 blue; 1 blue, 5 green
Game 2: 9 red, 2 green; 5 red, 1 blue, 6 green; 3 green, 13 red, 1 blue; 3 red, 6 green; 1 blue, 14 red, 6 green
Game 3: 6 red, 3 blue, 8 green; 6 blue, 12 green, 15 red; 3 blue, 18 green, 4 red
Game 4: 1 blue, 4 red; 2 blue, 6 red; 13 blue; 11 blue, 1 green, 8 red; 10 blue, 3 green, 2 red; 3 green, 7 blue
Game 5: 2 red, 1 blue, 8 green; 2 blue, 7 green, 3 red; 1 blue, 7 green, 4 red; 2 blue, 1 green, 1 red; 13 green, 1 blue
Game 6: 7 green, 1 red, 3 blue; 1 red, 4 blue; 6 green, 6 blue; 8 green, 1 red; 6 green, 1 red, 5 blue
Game 7: 10 blue, 1 green; 5 red, 8 blue, 3 green; 11 blue, 5 red, 8 green; 2 blue, 8 red, 5 green; 7 blue, 9 green; 6 blue, 2 green, 7 red
Game 8: 15 green, 8 blue, 3 red; 6 blue, 7 green, 5 red; 2 green, 1 red, 5 blue; 9 blue, 9 green, 5 red
Game 9: 16 red; 5 blue, 6 red, 9 green; 7 blue, 6 green, 2 red; 15 red, 5 blue, 3 green; 1 red, 6 green, 6 blue; 3 blue, 7 red, 5 green
Game 10: 17 green, 5 blue, 6 red; 18 green, 9 red; 4 red, 4 blue, 4 green; 10 red, 6 green, 5 blue; 8 red, 4 blue, 12 green
Game 11: 4 blue, 2 green, 5 red; 1 blue, 1 red; 9 blue, 1 green, 2 red; 4 red, 10 blue; 3 green, 4 blue, 3 red
Game 12: 4 green, 2 blue, 7 red; 4 blue, 2 green, 1 red; 7 green, 5 blue, 9 red
Game 13: 1 green, 3 red, 3 blue; 1 blue, 10 green; 2 green, 3 blue
Game 14: 7 red, 3 green, 12 blue; 5 red, 4 green, 6 blue; 13 blue, 1 red; 4 blue, 6 red, 2 green; 4 red, 3 blue; 9 red, 13 blue
Game 15: 4 blue, 5 red, 2 green; 7 red, 2 blue, 1 green; 17 red, 3 blue; 2 blue; 4 blue, 8 red
Game 16: 5 blue; 9 red, 14 green, 5 blue; 5 blue, 9 green
Game 17: 3 blue, 5 red; 6 blue, 1 green, 4 red; 7 green, 6 blue, 7 red; 1 red, 6 blue, 4 green; 6 green; 1 blue, 6 green
Game 18: 9 blue, 4 green, 2 red; 1 green, 9 red, 10 blue; 14 red, 10 green, 17 blue; 12 red, 1 green, 15 blue; 3 blue, 8 red, 2 green; 3 green, 11 red, 13 blue
Game 19: 1 blue, 3 red, 9 green; 14 green, 3 red, 2 blue; 1 blue, 8 red, 11 green; 4 blue, 3 red; 14 red, 4 green; 5 red, 8 green
Game 20: 2 blue, 3 red, 2 green; 3 blue, 2 green; 1 red, 4 green, 5 blue; 9 blue, 9 green, 3 red; 3 green, 1 blue, 4 red; 1 red, 9 green, 2 blue
Game 21: 11 blue, 6 red; 8 red; 7 red, 6 green, 11 blue; 7 green, 7 red, 11 blue; 6 red, 12 blue
Game 22: 7 green, 8 blue, 5 red; 12 green, 4 red, 2 blue; 12 green, 7 red, 11 blue
Game 23: 5 green, 2 blue, 7 red; 6 blue, 8 green, 3 red; 10 red, 5 blue; 6 green, 3 blue; 1 green, 8 red
Game 24: 17 blue, 1 green, 2 red; 2 red, 11 green, 9 blue; 6 red, 8 blue
Game 25: 3 red, 1 blue, 19 green; 1 blue, 1 green, 6 red; 6 green, 5 blue; 4 green, 2 red, 19 blue; 6 red, 19 blue, 18 green; 1 red, 4 blue, 1 green
Game 26: 3 red, 4 blue, 2 green; 2 red, 1 green, 3 blue; 14 blue, 1 green, 3 red; 5 green, 2 red, 10 blue; 9 blue, 2 red, 7 green; 15 blue, 4 green, 3 red
Game 27: 3 blue, 5 red, 2 green; 6 red, 7 blue, 9 green; 14 green, 11 red, 6 blue; 3 blue, 20 green, 3 red; 6 red, 15 green, 7 blue; 13 red, 1 blue, 14 green
Game 28: 4 blue, 7 green, 4 red; 2 red, 4 blue, 7 green; 6 blue, 11 green, 4 red; 6 blue, 6 green, 3 red; 6 green, 12 red
Game 29: 3 red, 9 blue; 5 red, 5 blue; 2 green, 3 red, 3 blue
Game 30: 1 green, 1 red, 3 blue; 1 blue, 1 red, 3 green; 1 blue; 1 blue, 3 green, 1 red; 2 blue, 2 green
Game 31: 2 blue, 1 red; 1 blue, 1 green, 1 red; 4 blue, 3 green; 1 red, 3 green, 2 blue; 2 green
Game 32: 1 blue, 6 green; 9 red, 6 green; 1 blue, 15 red, 3 green
Game 33: 18 green, 1 blue, 10 red; 10 red, 1 blue, 7 green; 11 green; 6 red, 13 green
Game 34: 10 red, 14 blue, 6 green; 2 green, 13 blue, 1 red; 8 green, 7 blue, 1 red; 9 blue, 7 green, 4 red
Game 35: 5 blue, 9 green, 2 red; 7 green, 9 blue, 5 red; 1 green, 5 red
Game 36: 10 red; 5 red, 1 green, 1 blue; 2 green, 8 red; 9 red, 2 green; 1 blue, 10 red; 6 red, 1 green, 1 blue
Game 37: 13 red, 1 blue, 7 green; 1 green, 9 red, 3 blue; 4 red, 1 blue, 11 green; 1 red; 1 red, 1 blue; 6 red, 3 blue, 2 green
Game 38: 3 blue, 12 red, 7 green; 1 green; 12 red, 1 blue
Game 39: 7 green, 12 blue, 2 red; 3 red, 10 blue, 7 green; 2 red, 8 green, 3 blue; 3 red, 12 blue, 5 green
Game 40: 7 green, 5 red; 1 green, 2 blue; 2 red, 1 green, 7 blue
Game 41: 1 red, 7 green, 2 blue; 2 green, 2 blue; 4 blue, 7 green, 1 red; 1 blue, 1 red, 7 green; 6 blue, 2 red, 3 green
Game 42: 6 blue; 4 green, 18 blue, 1 red; 10 green, 14 blue, 2 red; 6 blue, 4 green; 2 red, 13 blue, 6 green; 6 green, 1 red, 5 blue
Game 43: 5 blue, 12 red; 5 blue, 2 green, 7 red; 9 red, 4 blue; 1 green, 11 red, 2 blue; 5 red, 1 green; 2 blue, 3 red, 1 green
Game 44: 4 blue, 9 red, 4 green; 4 blue, 10 red; 4 green, 5 red; 1 green, 2 red, 3 blue
Game 45: 7 green, 2 blue, 18 red; 19 red, 7 green; 8 green, 1 blue, 19 red; 2 green, 12 red; 6 red, 5 green; 7 green, 10 red
Game 46: 1 blue, 15 red, 11 green; 7 red, 1 green, 5 blue; 13 red, 2 blue, 2 green; 7 green, 5 blue, 10 red; 12 green, 3 red, 1 blue
Game 47: 2 blue, 2 red, 5 green; 7 green, 2 red, 7 blue; 10 blue, 2 red, 8 green
Game 48: 8 green, 10 red; 6 green, 5 red; 12 green, 2 blue; 17 green, 5 red, 1 blue; 14 green, 3 blue, 16 red; 1 blue, 5 red
Game 49: 5 blue, 6 red, 12 green; 8 blue, 15 green; 4 blue, 3 green, 3 red; 6 red, 11 green, 10 blue; 9 green, 2 red, 10 blue
Game 50: 10 red, 11 green, 14 blue; 6 green, 8 blue, 17 red; 2 blue, 4 red; 6 blue, 8 green, 17 red; 17 red, 9 blue, 2 green; 13 blue, 16 red, 12 green
Game 51: 12 red, 2 green, 7 blue; 5 blue, 10 red; 1 blue, 7 red, 1 green; 14 blue, 2 red, 1 green
Game 52: 5 blue, 5 red, 8 green; 1 blue, 9 green, 7 red; 4 blue, 5 red, 3 green; 7 green, 2 blue, 2 red; 5 red, 3 blue, 17 green; 19 green, 1 red
Game 53: 4 red, 1 blue, 2 green; 1 green; 2 red; 1 blue, 2 green; 2 green, 4 red
Game 54: 7 red; 9 red, 2 blue, 14 green; 1 blue, 5 green; 7 green, 3 blue
Game 55: 11 blue, 2 green; 11 blue, 9 green, 12 red; 8 green, 6 blue, 12 red
Game 56: 2 green, 1 red, 2 blue; 4 red, 5 green, 5 blue; 5 red, 5 blue, 10 green; 8 red, 3 green, 3 blue
Game 57: 7 red, 3 green; 1 blue, 6 red, 1 green; 1 blue; 7 red, 1 green, 1 blue; 2 red, 1 blue, 1 green; 3 green, 1 blue
Game 58: 9 blue, 2 red; 2 green, 9 blue, 2 red; 5 blue, 4 green
Game 59: 8 red; 5 green, 1 blue, 3 red; 1 green, 8 red
Game 60: 9 green, 8 blue, 3 red; 10 green, 4 red; 8 blue, 2 green, 4 red; 1 red, 5 green, 7 blue; 1 green, 4 blue, 1 red; 4 blue
Game 61: 5 blue, 9 red, 4 green; 5 green, 7 blue, 6 red; 7 green, 8 red; 7 blue, 4 red, 2 green; 8 red, 4 blue, 5 green; 3 green, 9 red, 7 blue
Game 62: 9 red, 10 blue; 1 green, 7 red, 13 blue; 1 green, 11 blue; 6 red, 16 blue, 5 green; 20 red, 1 green, 3 blue
Game 63: 9 red, 8 green, 1 blue; 13 green, 12 red, 1 blue; 7 green, 5 red, 3 blue
Game 64: 3 red, 2 blue, 10 green; 3 green; 1 blue, 8 green, 2 red; 7 red, 1 blue, 4 green; 9 red, 1 blue, 4 green
Game 65: 7 red, 6 green; 1 blue, 4 green, 7 red; 6 red; 6 red, 4 green, 1 blue
Game 66: 4 blue, 4 green; 1 red, 7 green, 1 blue; 7 green, 3 red, 3 blue; 1 blue, 1 red, 6 green; 3 red, 7 green
Game 67: 5 green, 16 blue, 5 red; 4 red, 7 green, 3 blue; 4 red, 4 green, 9 blue; 12 green, 5 red; 15 green, 3 red; 10 blue, 6 red, 1 green
Game 68: 3 green, 3 blue, 5 red; 2 green, 6 blue; 2 green, 3 blue, 1 red; 1 blue, 11 red
Game 69: 5 green, 1 blue; 16 green, 9 red; 10 red, 18 green
Game 70: 1 blue, 1 green; 1 red; 1 red, 2 blue, 1 green; 1 green, 2 red; 2 blue, 2 red; 1 red
Game 71: 11 red; 2 green, 3 blue, 13 red; 1 green, 3 blue; 15 red, 1 green, 3 blue; 4 red
Game 72: 2 blue, 6 red, 18 green; 6 red, 8 green, 7 blue; 5 blue, 3 red, 12 green; 3 red, 2 blue, 4 green
Game 73: 12 blue, 7 green, 4 red; 5 green, 2 red, 4 blue; 3 green, 3 red, 10 blue; 1 green, 12 blue, 6 red; 3 blue, 6 green, 14 red
Game 74: 3 red; 1 blue, 8 green, 11 red; 3 green, 2 red
Game 75: 5 green, 2 red, 1 blue; 8 green, 2 red; 11 green, 2 red; 2 red, 17 green; 3 blue, 3 green, 2 red
Game 76: 1 blue, 5 green, 4 red; 8 green, 11 blue, 5 red; 8 blue, 2 red, 11 green
Game 77: 1 red, 11 blue, 7 green; 8 green, 4 blue; 1 blue, 8 green
Game 78: 1 green, 1 red, 1 blue; 3 green, 1 blue, 3 red; 10 green, 1 blue; 12 green
Game 79: 1 red, 11 blue, 6 green; 3 green, 3 red, 5 blue; 16 blue, 1 red, 5 green; 11 blue, 3 green, 2 red; 8 blue, 6 green, 4 red
Game 80: 5 green; 6 green, 7 red, 4 blue; 7 green, 5 blue; 6 blue, 6 green; 7 blue, 7 green; 6 green, 7 blue, 5 red
Game 81: 1 green, 14 blue; 11 blue, 1 red; 1 red, 16 green, 2 blue; 9 green, 1 red, 13 blue; 10 green, 8 blue
Game 82: 7 green, 7 red, 3 blue; 4 blue, 1 green, 4 red; 2 green, 14 blue, 3 red
Game 83: 15 blue; 2 blue, 1 green, 4 red; 8 green, 4 red, 6 blue
Game 84: 12 blue, 17 green; 6 green, 1 red, 16 blue; 1 blue, 1 red; 5 blue, 11 green
Game 85: 5 blue, 15 green, 3 red; 4 blue, 1 green, 11 red; 8 red, 2 blue, 4 green
Game 86: 11 blue, 16 green, 16 red; 11 blue, 17 red, 10 green; 8 green, 7 red
Game 87: 2 red, 4 green, 2 blue; 2 blue, 6 green; 2 red, 3 blue, 3 green; 1 red, 4 green; 1 green, 2 blue, 2 red; 4 blue, 4 green
Game 88: 10 red, 7 green; 2 blue, 6 red, 1 green; 8 blue, 8 red, 7 green; 2 green, 5 blue, 2 red; 3 blue, 3 red, 6 green
Game 89: 9 blue, 16 green; 2 red, 5 blue, 6 green; 12 blue, 15 green; 8 green, 2 red, 3 blue
Game 90: 18 red, 1 blue; 3 red, 5 blue, 4 green; 1 blue, 2 green, 6 red; 2 green, 16 red, 3 blue; 5 blue, 13 red, 5 green
Game 91: 4 red, 7 green, 1 blue; 3 green, 16 blue, 2 red; 4 green, 8 blue
Game 92: 4 red, 3 green; 5 red, 11 green, 1 blue; 16 green, 13 red; 15 green, 14 red, 3 blue; 3 red, 5 green, 2 blue
Game 93: 2 blue, 1 red, 3 green; 10 blue, 1 red, 10 green; 11 blue, 16 green, 4 red; 2 green, 20 blue, 7 red; 11 green, 8 red, 15 blue; 9 green, 10 blue, 1 red
Game 94: 2 blue, 12 red, 10 green; 16 red, 9 blue, 6 green; 5 green, 9 blue, 11 red; 4 red, 2 blue
Game 95: 2 green, 9 red, 1 blue; 2 blue, 1 red; 2 green, 5 blue, 3 red
Game 96: 1 green, 5 red, 13 blue; 1 green, 2 red, 13 blue; 2 green, 2 red, 17 blue; 3 red, 1 green; 6 red, 2 green; 1 green, 7 blue, 4 red
Game 97: 1 green, 1 red, 1 blue; 2 blue, 11 green; 1 blue, 13 green; 9 blue, 6 green, 1 red; 10 green, 8 blue
Game 98: 12 green, 9 red; 12 green, 10 blue, 3 red; 3 red, 13 green, 7 blue
Game 99: 8 green, 10 blue, 1 red; 10 green, 2 red, 6 blue; 3 green, 1 blue, 1 red; 10 blue, 1 red
Game 100: 8 blue, 6 green, 8 red; 7 red, 2 blue; 2 red, 10 green, 10 blue; 9 green, 7 red; 3 red, 7 green, 1 blue";
    }
}
