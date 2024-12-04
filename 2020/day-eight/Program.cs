using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var jmp_nops = input
                .Select((cmd, index) => new {Index = index, Cmd = cmd})
                .Where(item => Regex.IsMatch(item.Cmd, "jmp|nop"))
                .Select(item => item.Index)
                .ToList();

foreach(var index in jmp_nops)
{
    var copyProgram = new string[input.Length];
    input.CopyTo(copyProgram, 0);
    var instruction = input[index];
    if (instruction.StartsWith("jmp"))
    {
        copyProgram[index] = instruction.Replace("jmp", "nop");
    }
    else if (instruction.StartsWith("nop"))
    {
        copyProgram[index] = instruction.Replace("nop", "jmp");
    }
    var result = RunProgram(copyProgram);
    if (result > -1)
    {
        Console.WriteLine($"Found bug at {index} - {instruction}");
        Console.WriteLine($"Accumulator value is: {result}");
        break;
    }
}

int RunProgram(string[] program)
{
    var executeHistory = new List<int>();
    var accumulator = 0;
    var instructionPointer = 0;

    while (instructionPointer < program.Length)
    {
        if (executeHistory.Contains(instructionPointer))
        {
            Console.WriteLine($"Instruction repeated: {instructionPointer} '{program[instructionPointer]}' with acc value {accumulator}.");
            return -1;
        }
        executeHistory.Add(instructionPointer);
        var regex = Regex.Match(program[instructionPointer], @"(.{3}) (.+)");
        switch (regex.Groups[1].Value)
        {
            case "jmp":
                instructionPointer += int.Parse(regex.Groups[2].Value);
                break;
            case "nop":
                instructionPointer++;
                break;
            case "acc":
                accumulator += int.Parse(regex.Groups[2].Value);
                instructionPointer++;
                break;
        }
    }
    return accumulator;
}
