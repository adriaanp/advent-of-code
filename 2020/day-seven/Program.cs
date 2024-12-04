using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var bags = ParseInput(input);

var result = FindShinyGoldHolderBags(bags, "shiny gold")
.Distinct();

Console.WriteLine(bags.Count);
Console.WriteLine(result.Count());

var bagsForShinyGoldBag = GetBagCount(bags, "shiny gold");
Console.WriteLine(bagsForShinyGoldBag);

int GetBagCount(IEnumerable<Bag> bags, string name)
{
    var total = 0;
    var namedBag = bags.Single(b => b.Name == name);
    foreach (var bag in namedBag.Bags)
    {
        var bagCount = GetBagCount(bags, bag.Name);
        if (bagCount > 0)
        {
            total += bag.Quantity * bagCount;
        }
        total += bag.Quantity;
    }
    return total;
}

List<Bag> FindShinyGoldHolderBags(IEnumerable<Bag> bags, string bagName)
{
    var filtered = bags.Where(b => b.Bags.Exists(bagItem => bagItem.Name == bagName))
               .ToList();
    var result = new List<Bag>();
    result.AddRange(filtered);
    foreach (var filter in filtered)
    {
        result.AddRange(FindShinyGoldHolderBags(bags, filter.Name));
    }
    return result;
}

List<Bag> ParseInput(string[] input)
{
    var list = new List<Bag>();
    var containerBagRegex = new Regex(@"(\w+ \w+) bags contain (.+)");
    var bagQtyRegex = new Regex(@"(\d) (\w+ \w+) bag");

    foreach (var line in input)
    {
        var bagMatch = containerBagRegex.Match(line);
        var bagItems = new List<BagItem>();

        if (bagMatch.Groups[2].Value != "no other bags.")
        {
            foreach (var bagItem in bagMatch.Groups[2].Value.Split(','))
            {
                var bagItemMatch = bagQtyRegex.Match(bagItem);
                bagItems.Add(new BagItem(bagItemMatch.Groups[2].Value, int.Parse(bagItemMatch.Groups[1].Value)));
            }
        }

        list.Add(new Bag(bagMatch.Groups[1].Value, bagItems));
    }

    return list;
}

public record Bag(string Name, List<BagItem> Bags);
public record BagItem(string Name, int Quantity);
