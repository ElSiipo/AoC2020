using AoCHelper;
using System;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_09 : BaseDay
    {
        private readonly long[] _input;
        private long faultyValue = 0;
        private int startIndex = 0;

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
            long valueToTry = 0;

            while (startIndex < _input.Length)
            {
                bool isValid = false;
                int to = startIndex + preamble;
                var values = _input[startIndex..to];

                valueToTry = _input.ElementAt(to);

                for (int i = 0; i < values.Length; i++)
                {
                    for (int j = i + 1; j < values.Length; j++)
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

                startIndex++;
            }

            // Answer: 36845998
            faultyValue = valueToTry;
            return valueToTry.ToString();
        }
        
        public override string Solve_2()
        {
            for (int begin = startIndex; begin >= 0; begin--)
            {
                for (int end = begin + 2; end < _input.Length; end++)
                {
                    var values = _input[begin..end];

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
