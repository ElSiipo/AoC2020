using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC_2020_01
{
    public class Day4
    {
        public static void Run()
        {
            Part2(GetPassports(GetData()));

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int resultPart2 = Part2(GetPassports(GetData()));
            stopwatch.Stop();

            Console.WriteLine($"Part1: {resultPart2} \t{stopwatch.ElapsedMilliseconds} milliseconds {stopwatch.ElapsedTicks} ticks");
        }

        private static int Part2(List<Passport> passports)
        {
            var result = 0;

            foreach (var pass in passports)
            {
                if (string.IsNullOrWhiteSpace(pass.byr) ||
                    string.IsNullOrWhiteSpace(pass.iyr) ||
                    string.IsNullOrWhiteSpace(pass.eyr) ||
                    string.IsNullOrWhiteSpace(pass.hgt) ||
                    string.IsNullOrWhiteSpace(pass.hgtUnit) ||
                    string.IsNullOrWhiteSpace(pass.hcl) ||
                    string.IsNullOrWhiteSpace(pass.ecl) ||
                    string.IsNullOrWhiteSpace(pass.pid) ||
                    !pass.hcl.StartsWith('#') ||
                    pass.hcl.Length != 7)
                {
                    // cid optional
                    continue;
                }

                if (HasValidBirthYear(pass) &&
                      HasValidIssueYear(pass) &&
                      HasValidExpiryYear(pass) &&
                      HasValidHeigt(pass) &&
                      HasValidHairColour(pass) &&
                      HasValidEyeColor(pass.ecl) &&
                      HasValidPassportId(pass))
                {
                    result++;
                }
            }

            return result;
        }

        private static bool HasValidBirthYear(Passport p) => int.Parse(p.byr) >= 1920 && int.Parse(p.byr) <= 2002;
        private static bool HasValidIssueYear(Passport p) => int.Parse(p.iyr) >= 2010 && int.Parse(p.iyr) <= 2020;
        private static bool HasValidExpiryYear(Passport pass) => pass.eyr.Length == 4 && (int.Parse(pass.eyr) >= 2020 && int.Parse(pass.eyr) <= 2030);
        private static bool HasValidHeigt(Passport pass) => (pass.hgtUnit == "cm" && int.Parse(pass.hgt) >= 150 && int.Parse(pass.hgt) <= 193) ||
                (pass.hgtUnit == "in" && int.Parse(pass.hgt) >= 59 && int.Parse(pass.hgt) <= 76);
        private static bool HasValidHairColour(Passport p) => new Regex("^[0-9a-f]{6}$").IsMatch(p.hcl.Replace("#", string.Empty));
        private static bool HasValidEyeColor(string ecl) => new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(ecl);
        private static bool HasValidPassportId(Passport pass) => pass.pid.Length == 9 && long.TryParse(pass.pid, out var _);

        private static List<Passport> GetPassports(string[] data)
        {
            List<Passport> passports = new List<Passport>();
            foreach (var line in data)
            {
                var pass = new Passport();
                var splitted = line.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var entry in splitted)
                {
                    if (entry.StartsWith("byr"))
                        pass.byr = entry.Replace("byr:", "");
                    if (entry.StartsWith("iyr"))
                        pass.iyr = entry.Replace("iyr:", "");
                    if (entry.StartsWith("eyr"))
                        pass.eyr = entry.Replace("eyr:", "");
                    if (entry.StartsWith("hgt"))
                    {
                        var tempHgt = entry.Replace("hgt:", "");
                        var heightUnit = tempHgt.Substring(tempHgt.Length - 2, 2);
                        pass.hgt = tempHgt[0..^2];
                        pass.hgtUnit = heightUnit;
                    }
                    if (entry.StartsWith("hcl"))
                        pass.hcl = entry.Replace("hcl:", "");
                    if (entry.StartsWith("ecl"))
                        pass.ecl = entry.Replace("ecl:", "");
                    if (entry.StartsWith("pid"))
                        pass.pid = entry.Replace("pid:", "");
                    if (entry.StartsWith("cid"))
                        pass.cid = entry.Replace("cid:", "");
                }
                passports.Add(pass);
            }

            return passports;
        }

        private static string[] GetData() => File.ReadAllText(@"..\..\..\04.txt").Split("\r\n\r\n");
    }

    class Passport
    {
        public string byr { get; set; }
        public string iyr { get; set; }
        public string eyr { get; set; }
        public string hgt { get; set; }
        public string hgtUnit { get; set; }
        public string hcl { get; set; }
        public string ecl { get; set; }
        public string pid { get; set; }
        public string cid { get; set; }
    }
}

