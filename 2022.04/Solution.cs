namespace _2022._04;

internal record Assignment(int Start, int End);

internal record Pair(Assignment FirstElf, Assignment SecondElf);

internal static class Solution
{
    private static List<Pair> ParseData(string data)
    {
        var lines = data.Split("\r\n");
        var pairs = new List<Pair>();

        foreach (var line in lines)
        {
            var pairOfAssignments = line.Split(',') switch
            {
                [var firstAssignment, var secondAssignment] => (firstAssignment, secondAssignment),
                _ => throw new InvalidOperationException($"failed to get two pairOfAssignments from {line}")
            };

            var parsedFirstAssignment = pairOfAssignments.firstAssignment.Split("-") switch
            {
                [var start, var end] => new Assignment(int.Parse(start), int.Parse(end)),
                _ => throw new InvalidOperationException($"failed to get two values from {pairOfAssignments.firstAssignment}")
            };

            var parsedSecondAssignment = pairOfAssignments.secondAssignment.Split("-") switch
            {
                [var start, var end] => new Assignment(int.Parse(start), int.Parse(end)),
                _ => throw new InvalidOperationException($"failed to get two values from {pairOfAssignments.secondAssignment}")
            };

            var pair = new Pair(parsedFirstAssignment, parsedSecondAssignment);

            pairs.Add(pair);
        }

        return pairs;
    }

    private static bool IsBContainedInA(Assignment a, Assignment b)
    {
        return a.Start <= b.Start && a.End >= b.End;
    }

    public static int Solve1(string data)
    {
        var result = 0;
        var pairs = ParseData(data);

        foreach (var pair in pairs)
        {
            if (IsBContainedInA(pair.FirstElf, pair.SecondElf) || IsBContainedInA(pair.SecondElf, pair.FirstElf))
            {
                result++;
            }
        }

        return result;
    }

    // for the pair to not be overlapping
    // one assignment has to end before the other one starts
    // or it has to start after the other one ends
    private static bool IsPairOverlapping(Pair pair)
    {
        return !(pair.FirstElf.End < pair.SecondElf.Start || pair.FirstElf.Start > pair.SecondElf.End);
    }

    public static int Solve2(string data)
    {
        var result = 0;
        var pairs = ParseData(data);

        foreach (var pair in pairs)
        {
            if (IsPairOverlapping(pair))
            {
                result++;
            }
        }

        return result;
    }
}
