using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_06 : BaseDay
    {
        private readonly List<string[]> _input;

        public Day_06()
        {
            _input = File
                .ReadAllText(@"..\..\..\06.txt")
                .Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                .ToList();
        }

        // Correct answer
        // 3437

        public override string Solve_1()
        {
            int result = 0;
            foreach (var group in _input)
            {
                var hashset = new HashSet<char>(group[0]);

                foreach (var person in group.Skip(1))
                    hashset.UnionWith(person);

                result += hashset.Count;
            }

            return result.ToString();
        }

        public override string Solve_2()
        {
            int result = 0;
            foreach (var group in _input)
            {
                var hashset = new HashSet<char>(group[0]);
                foreach (var person in group.Skip(1))
                {
                    hashset.IntersectWith(person);
                }
                result += hashset.Count;
            }
            return result.ToString();
        }
    }
}

