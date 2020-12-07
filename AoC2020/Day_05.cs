using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_05 : BaseDay
    {
        private readonly string[] _input;
        public Day_05()
        {
            //_input = File.ReadAllText(@"..\..\..\05.txt").Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            _input = @"BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL".Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string Solve_1()
        {
            foreach (var line in _input)
            {
                int startVal = 127;
                var rows = line[0..^3];
                foreach (var character in rows)
                {
                    if (character == 'F')
                        startVal /= 2;
                }
            }

            return "Not solved yet!";
        }

        public override string Solve_2()
        {
            return "Not Solved Yet!";
        }
    }
}

