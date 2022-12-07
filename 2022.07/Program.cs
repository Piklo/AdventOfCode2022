namespace _2022._07;

internal class Program
{
    static void Main()
    {
        var testData =
            """
            $ cd /
            $ ls
            dir a
            14848514 b.txt
            8504156 c.dat
            dir d
            $ cd a
            $ ls
            dir e
            29116 f
            2557 g
            62596 h.lst
            $ cd e
            $ ls
            584 i
            $ cd ..
            $ cd ..
            $ cd d
            $ ls
            4060174 j
            8033020 d.log
            5626152 d.ext
            7214296 k
            """;
        var test = Solution.Solve1(testData);
        Console.WriteLine(test);

        var res1 = Solution.Solve1(Data.Value);
        Console.WriteLine(res1);

        var res2 = Solution.Solve2(Data.Value);
        Console.WriteLine(res2);
    }
}
