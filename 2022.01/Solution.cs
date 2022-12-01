namespace _2022._01;

public static class Solution
{
    private static List<int> ParseData(string data)
    {
        var lines = data.Split("\r\n");
        var elvesData = new List<int>();

        var currentElf = 0;
        foreach (var line in lines)
        {
            if (line == "")
            {
                elvesData.Add(currentElf);
                currentElf = 0;
                continue;
            }

            var succeeded = int.TryParse(line, out var value);
            if (!succeeded)
            {
                throw new Exception($"failed to get int out of \"{line}\"");
            }

            currentElf += value;
        }

        elvesData.Add(currentElf);

        return elvesData;
    }

    public static int Solve1(string data)
    {
        var list = ParseData(data);

        return list.Max();
    }

    public static int Solve2(string data)
    {
        var list = ParseData(data);
        var sum = list.OrderByDescending(x => x).Take(3).Sum();

        return sum;
    }
}
