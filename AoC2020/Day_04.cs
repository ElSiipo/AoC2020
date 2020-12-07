using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC_2020_01
{
    public class Day_04 : BaseDay 
    {
        private readonly string[] _input;

        public Day_04()
        {
            _input = File.ReadAllText(@"..\..\..\04.txt").Split("\r\n\r\n");
        }

        private static bool HasValidBirthYear(Passport p) => int.Parse(p.Byr) >= 1920 && int.Parse(p.Byr) <= 2002;
        private static bool HasValidIssueYear(Passport p) => int.Parse(p.Iyr) >= 2010 && int.Parse(p.Iyr) <= 2020;
        private static bool HasValidExpiryYear(Passport pass) => pass.Eyr.Length == 4 && (int.Parse(pass.Eyr) >= 2020 && int.Parse(pass.Eyr) <= 2030);
        private static bool HasValidHeigt(Passport pass) => (pass.HgtUnit == "cm" && int.Parse(pass.Hgt) >= 150 && int.Parse(pass.Hgt) <= 193) ||
                (pass.HgtUnit == "in" && int.Parse(pass.Hgt) >= 59 && int.Parse(pass.Hgt) <= 76);
        private static bool HasValidHairColour(Passport p) => new Regex("^[0-9a-f]{6}$").IsMatch(p.Hcl.Replace("#", string.Empty));
        private static bool HasValidEyeColor(string ecl) => new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(ecl);
        private static bool HasValidPassportId(Passport pass) => pass.Pid.Length == 9 && long.TryParse(pass.Pid, out var _);

        private static List<Passport> GetPassports(string[] data)
        {
            List<Passport> passports = new List<Passport>();
            foreach (var line in data)
            {
                var pass = new Passport();
                var splitted = line.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (splitted.Length < 7)
                    continue;

                foreach (var entry in splitted)
                {
                    if (entry.StartsWith("byr"))
                        pass.Byr = entry.Replace("byr:", "");
                    if (entry.StartsWith("iyr"))
                        pass.Iyr = entry.Replace("iyr:", "");
                    if (entry.StartsWith("eyr"))
                        pass.Eyr = entry.Replace("eyr:", "");
                    if (entry.StartsWith("hgt"))
                    {
                        var tempHgt = entry.Replace("hgt:", "");
                        var heightUnit = tempHgt.Substring(tempHgt.Length - 2, 2);
                        pass.Hgt = tempHgt[0..^2];
                        pass.HgtUnit = heightUnit;
                    }
                    if (entry.StartsWith("hcl"))
                        pass.Hcl = entry.Replace("hcl:", "");
                    if (entry.StartsWith("ecl"))
                        pass.Ecl = entry.Replace("ecl:", "");
                    if (entry.StartsWith("pid"))
                        pass.Pid = entry.Replace("pid:", "");
                    if (entry.StartsWith("cid"))
                        pass.Cid = entry.Replace("cid:", "");
                }
                passports.Add(pass);
            }

            return passports;
        }

        private static bool HasRequiredFields(Passport pass)
        {
            if (string.IsNullOrWhiteSpace(pass.Byr) ||
                    string.IsNullOrWhiteSpace(pass.Iyr) ||
                    string.IsNullOrWhiteSpace(pass.Eyr) ||
                    string.IsNullOrWhiteSpace(pass.Hgt) ||
                    string.IsNullOrWhiteSpace(pass.HgtUnit) ||
                    string.IsNullOrWhiteSpace(pass.Hcl) ||
                    string.IsNullOrWhiteSpace(pass.Ecl) ||
                    string.IsNullOrWhiteSpace(pass.Pid) ||
                    !pass.Hcl.StartsWith('#') ||
                    pass.Hcl.Length != 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string Solve_1()
        {
            var result = 0;
            var passports = GetPassports(_input);
            result = passports.Count(p => HasRequiredFields(p));

            return result.ToString();
        }

        public override string Solve_2()
        {
            var result = 0;
            var passports = GetPassports(_input);

            foreach (var pass in passports)
            {
                if (string.IsNullOrWhiteSpace(pass.Byr) ||
                    string.IsNullOrWhiteSpace(pass.Iyr) ||
                    string.IsNullOrWhiteSpace(pass.Eyr) ||
                    string.IsNullOrWhiteSpace(pass.Hgt) ||
                    string.IsNullOrWhiteSpace(pass.HgtUnit) ||
                    string.IsNullOrWhiteSpace(pass.Hcl) ||
                    string.IsNullOrWhiteSpace(pass.Ecl) ||
                    string.IsNullOrWhiteSpace(pass.Pid) ||
                    !pass.Hcl.StartsWith('#') ||
                    pass.Hcl.Length != 7)
                {
                    // cid optional
                    continue;
                }

                if (HasValidBirthYear(pass) &&
                      HasValidIssueYear(pass) &&
                      HasValidExpiryYear(pass) &&
                      HasValidHeigt(pass) &&
                      HasValidHairColour(pass) &&
                      HasValidEyeColor(pass.Ecl) &&
                      HasValidPassportId(pass))
                {
                    result++;
                }
            }

            return result.ToString();
        }
    }

    class Passport
    {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string HgtUnit { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }
    }
}

