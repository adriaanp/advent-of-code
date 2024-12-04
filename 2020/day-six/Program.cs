using System;
using System.IO;
using System.Linq;

var input = File.ReadAllText("input.txt");

var yesAnswers = input.Split("\n\n")
                .Select(s => s.Replace("\n", "")
                              .Distinct()
                              .Count())
                .Sum();

Console.WriteLine($"Yes answers across groups: {yesAnswers}");

var part2Answers = input.Split("\n\n")
                .Select(s => s.Split("\n").Where(s => s.Length != 0).ToArray())
                .Select(i => i.Aggregate((a,b) => new string(a.Intersect(b).ToArray())).Length)
                .Sum();

Console.WriteLine($"Part 2: {part2Answers}");