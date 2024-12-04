using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")
            .Select(line => long.Parse(line))
            .ToList();

var number = GetInvalidNumber(input);
Console.WriteLine($"{number} is invalid");
var weakness = GetXmasWeakness(number);
Console.WriteLine($"encryption weakness {weakness}");

long GetXmasWeakness(long number)
{
    var skip = 0;
    for (int i = 0; i < input.Count(); i++)
    {
        var total = input.Skip(skip).Take(i).Sum();
        if (total == number)
        {
            var min = input.Skip(skip).Take(i).Min();
            var max = input.Skip(skip).Take(i).Max();
            return min + max;
        }
        else if (total > number)
        {
            skip++;
            i = 0;
        }
    }
    return -1;
}

long GetInvalidNumber(List<long> input)
{
    var pointer = 1;
    while (pointer < input.Count())
    {
        var x = input.Skip(pointer - 1).Take(25).SelectMany(item => input.Skip(pointer - 1).Take(25), (x, y) => x == y ? 0 : x + y)
                .ToList();
        var number = input.Skip(pointer + 24).Take(1).Single();
        if (!x.Contains(number))
        {
            return number;
        }
        pointer++;
    }
    return -1;
}

