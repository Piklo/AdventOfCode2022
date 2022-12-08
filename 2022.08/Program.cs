namespace _2022._08;

internal class Program
{
    static void Main()
    {
        var testData =
            """
            30373
            25512
            65332
            33549
            35390
            """;
        var test1 = Solution.Solve1(testData);
        Console.WriteLine(test1);

        var test2 = Solution.Solve2(testData);
        Console.WriteLine(test2);

        var res1 = Solution.Solve1(Data.Value);
        Console.WriteLine(res1);

        var res2 = Solution.Solve2(Data.Value);
        Console.WriteLine(res2);
    }
}
