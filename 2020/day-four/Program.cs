using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

var passports = LoadPassports();

Console.WriteLine($"Total passports: {passports.Count()}");
Console.WriteLine($"Valid Passports: {passports.Count(IsPassportValid)}");

bool IsPassportValid(Dictionary<string, string> passport)
{
    var requiredFields = new List<string>(){
        "byr", "iyr","eyr","hgt","hcl","ecl", "pid"
    };
    return requiredFields.All(passport.ContainsKey) && ValidateFields(passport);
}

bool ValidateFields(Dictionary<string, string> passport)
{
    var validators = new Dictionary<string, Func<string, bool>>()
    {
        {"byr", value => int.Parse(value) is >= 1920 and <= 2002},
        {"iyr", value => int.Parse(value) is >= 2010 and <= 2020},
        {"eyr", value => int.Parse(value) is >= 2020 and <= 2030},
        {"hgt", value => {
            var match = Regex.Match(value, @"(\d*)(.*)");
            var height = int.Parse(match.Groups[1].Value);
            var unit = match.Groups[2].Value;
            if (unit == "cm") return height is >= 150 and <= 193;
            if (unit == "in") return height is >= 59 and <= 76;
            return false;
        }},
        {"hcl", value => Regex.IsMatch(value, @"#[0-9a-f]{6}")},
        {"ecl", value => new List<string> {"amb","blu", "brn","gry","grn","hzl","oth"}.Contains(value)},
        {"pid", value => Regex.IsMatch(value, @"^\d{9}$")},
        {"cid", value => true}
    };
    return passport.All(field => validators[field.Key](field.Value));
}

IEnumerable<Dictionary<string, string>> LoadPassports()
{
    var input = File.ReadAllLines("input.txt");
    var passports = new List<Dictionary<string, string>>();

    var passport = new Dictionary<string, string>();
    foreach (var line in input)
    {
        if (line.Length == 0)
        {
            passports.Add(passport);
            passport = new Dictionary<string, string>();
            continue;
        }

        var keyValues = line.Split(' ');
        foreach (var keyValue in keyValues)
        {
            var match = Regex.Match(keyValue, "(.*):(.*)");
            passport.Add(match.Groups[1].Value, match.Groups[2].Value);
        }
    }
    passports.Add(passport);
    return passports;
}
