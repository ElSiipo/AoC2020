using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_08 : BaseDay
    {
        private readonly string[] _input;

        public Day_08()
        {
            _input = File
                .ReadAllText(@"..\..\..\08.txt")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string Solve_1()
        {
            (long accumulator, bool ranTwice) = RunBootCode(_input);

            // Correct: 1553
            return accumulator.ToString();
        }

        public override string Solve_2()
        {
            long correctAccumulator = 0;
            for(int i = 0; i < _input.Length; i++)
            {
                string[] tempInput = new string[_input.Length];
                _input.CopyTo(tempInput, 0);

                if (tempInput[i].Contains("jmp"))
                    tempInput[i] = tempInput[i].Replace("jmp", "nop");

                (long accumulator, bool ranTwice) = RunBootCode(tempInput);

                if (!ranTwice)
                {
                    correctAccumulator = accumulator;
                    break;
                }
            }

            for (int i = 0; i < _input.Length; i++)
            {
                string[] tempInput = new string[_input.Length];
                _input.CopyTo(tempInput, 0);

                if (tempInput[i].Contains("nop"))
                    tempInput[i] = tempInput[i].Replace("nop", "jmp");

                (long accumulator, bool ranTwice) = RunBootCode(tempInput);

                if (!ranTwice)
                {
                    correctAccumulator = accumulator;
                    break;
                }
            }

            return correctAccumulator.ToString();
        }

        private static (long, bool) RunBootCode(string[] operations)
        {
            Dictionary<string, int> tableOfOperations = new Dictionary<string, int>();
            long accumulator = 0;
            bool instructionRanTwice = false;

            for (int index = 0; index < operations.Length; index++)
            {
                var instruction = operations[index][..3];
                var value = int.Parse(operations[index][4..]);

                if (tableOfOperations.ContainsKey($"{index}:{operations[index]}"))
                {
                    instructionRanTwice = true;
                    break;
                }

                tableOfOperations.Add($"{index}:{operations[index]}", value);

                switch (instruction)
                {
                    case "acc":
                        accumulator += value;
                        continue;
                    case "jmp":
                        index += --value;
                        continue;
                    default:
                        continue;
                }
            }

            return (accumulator, instructionRanTwice);
        }
    }
}
