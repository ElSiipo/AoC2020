using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_09 : BaseDay
    {
        private readonly long[] _input;
        private long faultyValue = 0;

        public Day_09()
        {
            _input = File
                .ReadAllText(@"..\..\..\09.txt")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => long.Parse(x))
                .ToArray();
        }

        public override string Solve_1()
        {
            int preamble = 25;
            int taken = 0;
            long valueToTry = 0;

            while (taken < _input.Length)
            {
                bool isValid = false;
                int to = taken + preamble;
                var values = _input[taken..to];

                valueToTry = _input.ElementAt(to);

                for (long i = 0; i < values.Length; i++)
                {
                    for (long j = i + 1; j < values.Length; j++)
                    {
                        if (values[i] + values[j] == valueToTry)
                        {
                            isValid = true;
                            break;
                        }
                    }

                    if (isValid)
                        break;
                }

                if (!isValid)
                    break;

                taken++;
            }

            // Answer: 36845998
            faultyValue = 36845998;
            return valueToTry.ToString();
        }
        
        public override string Solve_2()
        {
            for (int preamble = 2; preamble < _input.Length; preamble++)
            {
                for (int i = preamble +1; i < _input.Length; i++)
                {
                    var values = _input[preamble..i];

                    var sum = values.Sum();
                    if (sum == faultyValue)
                    {
                        return (values.Min() + values.Max()).ToString();
                    }
                    if (sum > faultyValue)
                        break;
                }
            }

            // Answer: 4830226
            return 0.ToString();
        }
    }
}
