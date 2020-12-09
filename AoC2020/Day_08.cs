using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_08 : BaseDay
    {
        private readonly Instruction[] _input;

        public Day_08()
        {
            _input = File
                .ReadAllText(@"..\..\..\08.txt")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => ParseInput(x))
                .ToArray();
        }

        private static Instruction ParseInput(string s)
        {
            var verb = s[..3];
            var argument = int.Parse(s[4..]);
            var instruction = new Instruction { Verb = Enum.Parse<Instructions>(verb), Argument = argument };
            return instruction;
        }

        public override string Solve_1()
        {
            // Correct: 1553
            (long accumulator, _) = RunBootCode(_input);
            return accumulator.ToString();
        }

        public override string Solve_2()
        {
            long accumulator = 0;

            var modifiableInstructions = _input
                .Select((inst, idx) => new { idx, inst })
                .Where(x => x.inst.Verb == Instructions.jmp || x.inst.Verb == Instructions.nop)
                .Select(x => x.idx);

            foreach(var i in modifiableInstructions) 
            {
                var successful = false;
                _input[i].Verb = (Instructions)((int)_input[i].Verb * -1);

                (accumulator, successful) = RunBootCode(_input);

                if (successful)
                {
                    break;
                }

                _input[i].Verb = (Instructions)((int)_input[i].Verb * -1);
            }

            // Correct: 1877
            return accumulator.ToString();
        }

        private static (long, bool) RunBootCode(Instruction[] instructions)
        {
            Dictionary<string, int> tableOfOperations = new Dictionary<string, int>();
            long accumulator = 0;
            bool successful = true;

            for (int index = 0; index < instructions.Length; index++)
            {
                if (tableOfOperations.ContainsKey($"{index}:{instructions[index].Verb}"))
                {
                    successful = false;
                    break;
                }

                tableOfOperations.Add($"{index}:{instructions[index].Verb}", instructions[index].Argument);

                switch (instructions[index].Verb)
                {
                    case Instructions.acc:
                        accumulator += instructions[index].Argument;
                        continue;
                    case Instructions.jmp:
                        index += instructions[index].Argument - 1;
                        continue;
                    default:
                        continue;
                }
            }

            return (accumulator, successful);
        }

        private class Instruction
        {
            public Instructions Verb { get; set; }
            public int Argument { get; set; }
        }
        private enum Instructions
        {
            nop = -1,
            acc = 0,
            jmp = 1
        }
    }
}
