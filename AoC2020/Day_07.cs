using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2020_01
{
    public class Day_07 : BaseDay
    {
        private readonly HashSet<Bag> _input;
        //private readonly string[] _input;

        public Day_07()
        {
            _input = File
                .ReadAllText(@"..\..\..\07.txt")
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new string[] { ", ", " contain " }, StringSplitOptions.RemoveEmptyEntries))
                .ToArray()
                .Select(line => new Bag
                {
                    Color = line[0]
                    .Replace("bags", "bag")
                    .Replace(".", ""),
                    CarriesBagTypes = line.Skip(1)
                        .Select(x => GenerateInner(x))
                        .Where(x => x.HasValue)
                        .Select(x => x.Value)
                        .ToList()
                }).ToHashSet();

            //_input = File
            //    .ReadAllText(@"..\..\..\07.txt")
            //    .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string Solve_1()
        {
            var carriesShinyGolden = _input.Where(b => b.CarriesBagTypes.Any(x => x.Item1.Color.Contains("shiny gold"))).ToHashSet();
            var hashset = new HashSet<Bag>(carriesShinyGolden);
            foreach (var bag in carriesShinyGolden)
            {
                CanCarryBag(bag, _input, hashset);
            }
            // Correct: 296
            return hashset.Count.ToString();
        }

        //public string New_Solve_1()
        //{
        //    var hashSet = _input
        //        .Select(x => x.Split(new string[] { " contain ", ", ", " bags", " bag", "." }, StringSplitOptions.RemoveEmptyEntries))
        //        .ToHashSet();

        //    var carriesShinyGolden = new HashSet<string[]>();
        //    foreach(var hash in hashSet)
        //    {
        //        for (int i = 1; i < hash.Length; i++)
        //        {
        //            if (hash[i].Contains("shiny gold"))
        //                carriesShinyGolden.Add(hash);
        //        }
        //    }

        //    var hashset = new HashSet<string[]>(carriesShinyGolden);
        //    foreach (var bag in hashSet)
        //    {
        //        CanCarryBag(bag[0], carriesShinyGolden);
        //    }

        //    // Correct: 296
            
        //    return hashset.Count.ToString();
        //}

        private int CanCarryBag(string color, HashSet<string[]> allBags)
        {
            var foundBags = allBags.Where(b => b[0].Contains(color));

            foreach(var bag in foundBags)
            {
                CanCarryBag(bag[0], allBags);
            }

            return 0;
        }

        public override string Solve_2()
        {
            var bag = _input.First(b => b.Color.Contains("shiny gold"));
            var result = CalculateBagage(bag, _input);
            return result.ToString();
            //Correct: 9339
        }

        //public override string New_Solve_2()
        //{
        //    var bag = _input.First(b => b.Contains("shiny gold"));
        //    //var result = CalculateBagage(bag, _input);
        //    return "Not Finished!".ToString();
        //}

        private void CanCarryBag(Bag bag, HashSet<Bag> allBags, HashSet<Bag> canCarry)
        {
            if (bag.CarriesBagTypes == null)
                return;

            var foundBags = allBags.Where(b => b.CarriesBagTypes.Any(x => x.Item1.Color.Contains(bag.Color))).ToList();

            canCarry.UnionWith(foundBags);
            foreach (var b in foundBags)
            {
                CanCarryBag(b, allBags, canCarry);
            }
        }

        private static int CalculateBagage(Bag bag, HashSet<Bag> allBags)
        {
            var total = 0;
            var getBag = allBags.First(b => b.Color == bag.Color);

            foreach(var tempBag in getBag.CarriesBagTypes)
            {
                total += tempBag.Item2 + tempBag.Item2 * CalculateBagage(tempBag.Item1, allBags);
            }

            return total;
        }

        private static (Bag, int)? GenerateInner(string line)
        {
            return line.Contains("no other bags") 
                ? null 
                : ((Bag, int)?)(new Bag { Color = line[1..].Trim().Replace("bags", "bag").Replace(".","") }, int.Parse(line[0..1]));
        }

        class Bag
        {
            public string Color { get; set; }
            public List<(Bag, int)> CarriesBagTypes { get; set; }
        }
    }
}

