using System;
using System.IO;

var input = File.ReadAllLines("input.txt");
var slope1 = GetTrees(input, 1, 1);
var slope2 = GetTrees(input, 3, 1);
var slope3 = GetTrees(input, 5, 1);
var slope4 = GetTrees(input, 7, 1);
var slope5 = GetTrees(input, 1, 2);
Console.WriteLine($"R1,D1 => {slope1}");
Console.WriteLine($"R3,D1 => {slope2}");
Console.WriteLine($"R5,D1 => {slope3}");
Console.WriteLine($"R7,D1 => {slope4}");
Console.WriteLine($"R1,D2 => {slope5}");
double result = slope1 * slope2 * slope3 * slope4 * slope5;
Console.WriteLine($"Part 2: {result}");

int GetTrees(string[] input, int right, int down)
{
    var numberOfTrees = 0;
    var pointer = right;
    var currentLine = down;

    while (currentLine < input.Length)
    {
        var line = input[currentLine];
        //Console.WriteLine($"{currentLine}-{pointer}: {line} --> {pointer} = {line[pointer]}");

        if (line[pointer] == '#')
        {
            numberOfTrees++;
        }

        pointer += right;
        if (pointer > line.Length - 1)
        {
            pointer = pointer - line.Length;
        }
        currentLine += down;
    }
    return numberOfTrees;
}
