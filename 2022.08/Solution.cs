using System.Diagnostics;

namespace _2022._08;

[DebuggerDisplay("Height = {Height}, Visible = {Visible}")]
internal class Tree
{
    public required int Height { get; init; }
    public bool Visible { get; set; } = false;
}

internal static class Solution
{
    private static List<List<Tree>> ParseData(string data)
    {
        var lists = new List<List<Tree>>();

        var lines = data.Split("\r\n");

        foreach (var line in lines)
        {
            var list = new List<Tree>();
            lists.Add(list);

            foreach (var character in line)
            {
                var value = int.Parse(character.ToString());
                var tree = new Tree() { Height = value };

                list.Add(tree);
            }
        }

        return lists;
    }

    private static void SetVisibleFromTheLeft(List<List<Tree>> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var currentTallest = -1;

            for (var j = 0; j < data[i].Count; j++)
            {
                var currentTree = data[i][j];

                if (currentTree.Height > currentTallest)
                {
                    currentTree.Visible = true;
                    currentTallest = currentTree.Height;
                }
            }
        }
    }
    private static void SetVisibleFromTheRight(List<List<Tree>> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            var currentTallest = -1;

            for (var j = data[i].Count - 1; j >= 0; j--)
            {
                var currentTree = data[i][j];

                if (currentTree.Height > currentTallest)
                {
                    currentTree.Visible = true;
                    currentTallest = currentTree.Height;
                }
            }
        }
    }
    private static void SetVisibleFromTheTop(List<List<Tree>> data)
    {
        for (var j = 0; j < data[0].Count; j++)
        {
            var currentTallest = -1;

            for (var i = 0; i < data.Count; i++)
            {
                var currentTree = data[i][j];

                if (currentTree.Height > currentTallest)
                {
                    currentTree.Visible = true;
                    currentTallest = currentTree.Height;
                }
            }
        }
    }
    private static void SetVisibleFromTheBottom(List<List<Tree>> data)
    {
        for (var j = 0; j < data[0].Count; j++)
        {
            var currentTallest = -1;

            for (var i = data.Count - 1; i >= 0; i--)
            {
                var currentTree = data[i][j];

                if (currentTree.Height > currentTallest)
                {
                    currentTree.Visible = true;
                    currentTallest = currentTree.Height;
                }
            }
        }
    }

    private static int CountVisible(List<List<Tree>> trees)
    {
        var count = 0;

        foreach (var row in trees)
        {
            foreach (var tree in row)
            {
                if (tree.Visible)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static int Solve1(string data)
    {
        var trees = ParseData(data);

        SetVisibleFromTheLeft(trees);
        SetVisibleFromTheRight(trees);
        SetVisibleFromTheTop(trees);
        SetVisibleFromTheBottom(trees);

        var result = CountVisible(trees);

        return result;
    }

    public static int Solve2(string data)
    {
        var trees = ParseData(data);
        var maxScore = 0;

        for (var i = 0; i < trees.Count; i++)
        {
            for (var j = 0; j < trees[i].Count; j++)
            {
                var currentTree = trees[i][j];

                var left = 0;
                var right = 0;
                var top = 0;
                var bottom = 0;

                // count visible trees to the left
                for (var j2 = j - 1; j2 >= 0; j2--)
                {
                    var nextTree = trees[i][j2];
                    left++;

                    if (nextTree.Height >= currentTree.Height) break;
                }

                // count visible trees to the right
                for (var j2 = j + 1; j2 < trees[i].Count; j2++)
                {
                    var nextTree = trees[i][j2];
                    right++;

                    if (nextTree.Height >= currentTree.Height) break;
                }

                // count visible trees to the top
                for (var i2 = i - 1; i2 >= 0; i2--)
                {
                    var nextTree = trees[i2][j];
                    top++;

                    if (nextTree.Height >= currentTree.Height) break;
                }

                // count visible trees to the bottom
                for (var i2 = i + 1; i2 < trees.Count; i2++)
                {
                    var nextTree = trees[i2][j];
                    bottom++;

                    if (nextTree.Height >= currentTree.Height) break;
                }

                var score = left * right * top * bottom;

                if (score > maxScore) maxScore = score;
            }
        }

        return maxScore;
    }
}
