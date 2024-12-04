using System.Text.RegularExpressions;

public static class Day3
{
    public static void Execute()
    {
        var memory = Memory.LoadMemoryFromFile("Day3/input.txt");
        var cpu = new Cpu();
        cpu.LoadMemory(memory);

        Console.WriteLine($"Sum of mul instructions: {cpu.AddAllMulInstructions()}");
        Console.WriteLine($"Sum of do mul instructions: {cpu.AddAllDoMulInstructions()}");
    }
}

public record MulInstruction(int X, int Y)
{
    public int Execute()
    {
        return X * Y;
    }
}

public class Memory
{
    private string _memory = string.Empty;

    public Memory(string memory)
    {
        _memory = memory;
    }

    public static Memory LoadMemoryFromFile(string fileName)
    {
        var content = File.ReadAllText(fileName);
        return new Memory(content);
    }

    public IEnumerable<MulInstruction> GetMulInstructions()
    {
        return ParseMulInstructions(_memory);
    }

    private IEnumerable<MulInstruction> ParseMulInstructions(string memory)
    {
        var mulPattern = @"(mul\((\d{1,3}),(\d{1,3})\))";
        return Regex.Matches(memory, mulPattern)
            .Select(match => new MulInstruction(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value)));
    }

    public IEnumerable<MulInstruction> GetGoMulInstructions()
    {
        var instructionMemory = string.Join("", _memory.Split("do()")
            .Select(s => s.Split("don't()").First())
            );

        return ParseMulInstructions(instructionMemory);
    }
}

public class Cpu
{
    private Memory _memory = new(string.Empty);

    public void LoadMemory(Memory memory)
    {
        _memory = memory;
    }

    public int AddAllMulInstructions()
    {
        return _memory.GetMulInstructions().Sum(i => i.Execute());
    }

    public int AddAllDoMulInstructions()
    {
        return _memory.GetGoMulInstructions().Sum(i => i.Execute());
    }
}
