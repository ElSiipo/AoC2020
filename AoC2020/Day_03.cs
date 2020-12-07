using AoCHelper;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_03 : BaseDay
    {
        private readonly char[][] _input;

        public Day_03()
        {
            _input = File.ReadAllLines(@"..\03.txt").Select(line => line.ToCharArray()).ToArray();
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

        public override string Solve_1()
        {
            return CountTrees(_input, (3, 1)).ToString();
        }

        public override string Solve_2()
        {
            long result = 1;

            (int, int)[] array = new (int, int)[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            for (int i = 0; i < array.Length; i++)
            {
                (int, int) slope = array[i];
                result *= CountTrees(_input, slope);
            }

            return result.ToString();
        }
    }
}
