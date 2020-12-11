using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_10 : BaseDay
    {
        private readonly long[] _input;

        public Day_10()
        {
            _input = File
                .ReadAllText(@"..\..\..\10.txt")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .OrderBy(x => x)
                .ToArray();
        }

        public override string Solve_1()
        {
            int oneValues = 1;
            int twoValues = 0;
            int threeValues = 1;

            for (int i = 1; i < _input.Length; i++)
            {
                if (_input[i] - _input[i - 1] == 1)
                    oneValues++;
                else if (_input[i] - _input[i - 1] == 2)
                    twoValues++;
                else if (_input[i] - _input[i - 1] == 3)
                    threeValues++;
            }

            // Correct value: 1998
            return (oneValues * threeValues).ToString();
        }
        
        public override string Solve_2()
        {
            var combinations = new Dictionary<long, long> { { 0, 1 } };
            var max = _input.Max() + 3;

            for (int i = 0; i <= max; i++)
            {
                if(_input.Contains(i) || i == max)
                {
                    long temp = 0;
                    if (combinations.Keys.Contains(i - 3))
                        temp += combinations[i - 3];
                    if (combinations.Keys.Contains(i - 2))
                        temp += combinations[i - 2];
                    if (combinations.Keys.Contains(i - 1))
                        temp += combinations[i - 1];
                    combinations.Add(i, temp);
                }
            }

            // Correct value: 347250213298688
            return combinations[max].ToString();
        }
    }
}
