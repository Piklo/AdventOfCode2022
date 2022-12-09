namespace _2022._09;

internal class Program
{
    static void Main()
    {
        var testData =
            """
            R 4
            U 4
            L 3
            D 1
            R 4
            D 1
            L 5
            R 2
            """;
        var test1 = Solution.Solve1(testData);
        Console.WriteLine(test1);

        var res1 = Solution.Solve1(Data.Value);
        Console.WriteLine(res1);
    }
}
