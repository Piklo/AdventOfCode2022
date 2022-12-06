namespace _2022._06;
internal static class Solution
{
    private static int Solve(string data, int marker)
    {
        var queue = new Queue<char>();
        var counter = 0;

        foreach (var character in data)
        {
            while (queue.Contains(character))
            {
                queue.Dequeue();
            }

            counter++;
            queue.Enqueue(character);

            if (queue.Count == marker)
                break;
        }

        return counter;
    }

    public static int Solve1(string data)
    {
        const int StartOfPacketMarker = 4;

        return Solve(data, StartOfPacketMarker);
    }

    public static int Solve2(string data)
    {
        const int StartOfMessageMarker = 14;

        return Solve(data, StartOfMessageMarker);
    }
}
