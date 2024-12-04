using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")
            .Select(line => int.Parse(line))
            .OrderBy(x => x)
            .ToList();

var maxJolt = input.Max() + 3;
input.Add(maxJolt);

Console.WriteLine($"Total {input.Count()}");

var diffs = input.Select((x, i) => i == 0 ? x : x - input[i-1]);
var oneJoltDiffs = diffs.Count(x => x == 1);
var threeJoltDiffs = diffs.Count(x => x == 3);

Console.WriteLine($"1-jolt differences = {oneJoltDiffs}, 3-jolt differences = {threeJoltDiffs}, answer = {oneJoltDiffs * threeJoltDiffs}");


//was stumped with this part, thanks to this comment that pushed me over the line https://www.reddit.com/r/adventofcode/comments/kacdbl/2020_day_10c_part_2_no_clue_how_to_begin/gf9lzhd/?utm_source=reddit&utm_medium=web2x&context=3
// part II

input.Insert(0, 0);
var items = input.ToDictionary(key => key, element => 0L);
items[0] = 1;

foreach(var adaptor in items)
{
    var reachableItems = items.Where(x => x.Key > adaptor.Key && x.Key <= adaptor.Key + 3);
    foreach(var item in reachableItems)
    {
        items[item.Key] = item.Value + adaptor.Value;
    }
}

Console.WriteLine($"Unique combinations : {items.Last().Value}");
