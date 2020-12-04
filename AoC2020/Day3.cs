using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC_2020_01
{
    public class Day3
    {
        public static void Run()
        {
            var data = GetMap();

            // warm up
            var stopwatchWarmup = new Stopwatch();
            stopwatchWarmup.Start();
            var resultPartWarmup = Part1(data);
            stopwatchWarmup.Stop();
            Console.WriteLine("Warm up!");
            Console.WriteLine($"Part1: {resultPartWarmup} \t\t{stopwatchWarmup.ElapsedMilliseconds} milliseconds {stopwatchWarmup.ElapsedTicks} ticks");


            Console.WriteLine("----");
            Console.WriteLine();
            Console.WriteLine("Real test:");
            // real
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var resultPart1 = Part1(data);
            stopwatch.Stop();
            Console.WriteLine($"Part1: {resultPart1} \t\t{stopwatch.ElapsedMilliseconds} milliseconds {stopwatch.ElapsedTicks} ticks");

            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var resultPart2 = Part2(data);
            stopwatch2.Stop();
            Console.WriteLine($"Part2: {resultPart2} \t{stopwatch2.ElapsedMilliseconds} milliseconds {stopwatch2.ElapsedTicks} ticks");
        }

        private static long Part1(char[][] data)
        {
            return CountTrees(data, (3, 1));
        }

        private static long Part2(char[][] map)
        {
            long result = 1;

            (int, int)[] array = new (int, int)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            for (int i = 0; i < array.Length; i++)
            {
                (int, int) slope = array[i];
                result *= CountTrees(map, slope);
            }

            return result;
        }

        private static long CountTrees(char[][] map, (int right, int down) slope)
        {
            int col = 0;
            int trees = 0;

            for (int row = 0; row < map.Length; row += slope.down)
            {
                if (map[row][col] == '#')
                    trees++;

                col = (col + slope.right) % map[0].Length;
            }

            return trees;
        }

        private static char[][] GetMap()
        {
            return File.ReadAllLines(@"..\03.txt").Select(line => line.ToCharArray()).ToArray();
        }
    }
}
