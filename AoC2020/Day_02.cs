using AoCHelper;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;
        public Day_02()
        {
            _input = File.ReadAllText(@"..\..\..\02.txt").Split(Environment.NewLine);
        }

        public override string Solve_1()
        {
            int validPasswords = 0;

            foreach (var line in _input)
            {
                var splittedLine = line.Split(" ");
                var minAndMax = splittedLine[0].Split("-").Select(int.Parse).ToArray();
                var letterToMatch = splittedLine[1].Replace(":", "").ToCharArray()[0];
                var password = splittedLine[2];

                var occurrances = password.Count(x => x == letterToMatch);

                if (occurrances >= minAndMax[0] && occurrances <= minAndMax[1])
                {
                    validPasswords++;
                }
            }

            return validPasswords.ToString();
        }

        public override string Solve_2()
        {
            int validPasswords = 0;

            foreach (var line in _input)
            {
                var splittedLine = line.Split(" ");
                var firstLastIndex = splittedLine[0].Split("-").Select(int.Parse).ToArray();
                var letterToMatch = splittedLine[1].Replace(":", "").ToCharArray()[0];
                var password = splittedLine[2];

                firstLastIndex[0]--;
                firstLastIndex[1]--;

                if (password[firstLastIndex[0]] == letterToMatch ^ password[firstLastIndex[1]] == letterToMatch)
                {
                    validPasswords++;
                }
            }

            return validPasswords.ToString();
        }
    }
}
