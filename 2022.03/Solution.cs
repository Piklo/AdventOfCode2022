namespace _2022._03;

internal static partial class Solution
{
    private static partial int GetPriority(char character);

    public static int Solve1(string data)
    {
        var lines = data.Split("\r\n");

        var sum = 0;
        foreach (var line in lines)
        {
            var firstCompartment = line[..(line.Length / 2)];
            var secondCompartment = line[(line.Length / 2)..];

            var hashSet1 = firstCompartment.ToHashSet();
            var hashSet2 = secondCompartment.ToHashSet();

            var intersection = hashSet1.Intersect(hashSet2);

            foreach (var character in intersection)
            {
                var priority = GetPriority(character);
                sum += priority;
            }
        }

        return sum;
    }

    private static HashSet<char> GetNextSet(ref IEnumerable<string> lines)
    {
        var firstThree = lines.Take(3);
        lines = lines.Skip(3);

        var hashSet = new HashSet<char>();
        foreach (var str in firstThree)
        {
            if (hashSet.Count == 0)
            {
                hashSet = str.ToHashSet();
                continue;
            }

            hashSet.IntersectWith(str);
        }

        return hashSet;
    }

    public static int Solve2(string data)
    {
        var lines = data.Split("\r\n").AsEnumerable();

        var sum = 0;
        while (lines.Any())
        {
            var hashSet1 = GetNextSet(ref lines);

            var hashSet2 = GetNextSet(ref lines);

            foreach (var character in hashSet1)
            {
                sum += GetPriority(character);
            }

            foreach (var character in hashSet2)
            {
                sum += GetPriority(character);
            }
        }

        return sum;
    }
}
