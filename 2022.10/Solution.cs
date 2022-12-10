namespace _2022._10;
internal enum InstructionsType
{
    Addx,
    Noop
}
internal record struct Instruction(string Type, int Value = 0);

internal class CPU
{
    public int registerX { get; private set; } = 1;
    public int NoopCycles { get; } = 1;
    public int AddxCycles { get; } = 2;
    public int CyclesCounter { get; private set; } = 0;
    public HashSet<(int CyclesCounter, int RegisterX)> HistoricValues { get; } = new();

    private InstructionsType GetInstructionType(string type)
    {
        switch (type)
        {
            case "addx": return InstructionsType.Addx;
            case "noop": return InstructionsType.Noop;
            default: throw new Exception($"unknown instruction type = '{type}'");
        }
    }
    public void ExecuteInstruction(Instruction instruction)
    {
        var type = GetInstructionType(instruction.Type);

        var cycles = type switch
        {
            InstructionsType.Addx => AddxCycles,
            InstructionsType.Noop => NoopCycles,
            _ => throw new Exception($"unknown instruction type = '{type}'")
        };

        for (var i = 0; i < cycles; i++)
        {
            CyclesCounter++;
            HistoricValues.Add((CyclesCounter, registerX));
        }

        registerX += instruction.Value;
        //HistoricValues.Add((CyclesCounter, registerX));
    }


}
internal static class Solution
{
    private static List<Instruction> ParseData(string data)
    {
        var instructions = new List<Instruction>();
        var lines = data.Split("\r\n");

        foreach (var line in lines)
        {
            var split = line.Split(' ');

            var instruction = split switch
            {
                [var type, var value] => new Instruction(type, int.Parse(value)),
                [var type] => new Instruction(type, 0),
                _ => throw new Exception($"failed to get instruction data from {line}")
            };

            instructions.Add(instruction);
        }

        return instructions;
    }

    public static int Solve1(string data)
    {
        var result = 0;
        var instructions = ParseData(data);
        var cpu = new CPU();

        foreach (var instruction in instructions)
        {
            cpu.ExecuteInstruction(instruction);
        }

        for (var i = 20 - 1; i < 220; i += 40)
        {
            var value = cpu.HistoricValues.ElementAt(i);
            var strenght = value.CyclesCounter * value.RegisterX;
            result += strenght;
        }

        return result;
    }
}
