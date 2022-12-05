using System.Text;

namespace _2022._05;

internal record Move(int HowMany, int FromWhere, int ToWhere);

internal static class Solution
{
    private static List<int> GetIndexes(string indexesString)
    {
        var splitIndexes = indexesString.Split(" ").Reverse();
        // indexes count is equal to the value of the last index
        var indexesCount = 0;
        foreach (var splitIndex in splitIndexes)
        {
            var succeeded = int.TryParse(splitIndex, out var result);
            if (succeeded)
            {
                indexesCount = result;
                break;
            }
        }

        var indexes = new List<int>();
        for (var i = 1; i <= indexesCount; i++)
        {
            // all indexes are followed by a space so we include it for safety
            indexes.Add(indexesString.IndexOf(i.ToString() + " "));
        }

        return indexes;
    }

    private static List<Stack<char>> GetStartingState(string data)
    {
        var stacks = new List<Stack<char>>();

        var lines = data.Split("\r\n");

        var indexes = GetIndexes(lines.Last());

        for (var i = 0; i < indexes.Count; i++)
        {
            stacks.Add(new Stack<char>());
        }

        //lines.Length - 1 because the last line has indexes
        for (int lineI = 0; lineI < lines.Length - 1; lineI++)
        {
            var line = lines[lineI];
            for (int indexI = 0; indexI < indexes.Count; indexI++)
            {
                int index = indexes[indexI];
                var character = line[index];

                if (character == '\0' || character == ' ') continue;

                stacks[indexI].Push(character);
            }
        }

        //inverse stacks
        for (var i = 0; i < stacks.Count; i++)
        {
            var stack = stacks[i];
            var tempStack = new Stack<char>();

            while (stack.Count > 0)
            {
                var value = stack.Pop();
                tempStack.Push(value);
            }

            stacks[i] = tempStack;
        }

        return stacks;
    }

    private static List<Move> GetMoves(string data)
    {
        var moves = new List<Move>();

        var lines = data.Split("\r\n");

        foreach (var line in lines)
        {
            var moveData = line.Split(" ") switch
            {
                [_, var howMany, _, var fromWhere, _, var toWhere] => new Move(int.Parse(howMany),
                                                                               int.Parse(fromWhere),
                                                                               int.Parse(toWhere)),
                _ => throw new Exception($"failed to get data about the move from {line}")
            };

            moves.Add(moveData);
        }

        return moves;
    }

    private static (List<Stack<char>> startingState, List<Move> moves) ParseData(string data)
    {
        var splitData = data.Split("\r\n\r\n");

        var strings = splitData switch
        {
            [var drawing, var moves] => (drawing, moves),
            _ => throw new Exception("Failed to get drawing and move strings from data")
        };

        var stacks = GetStartingState(strings.drawing);
        var parsedMoves = GetMoves(strings.moves);


        return (stacks, parsedMoves);
    }

    public static string Solve1(string data)
    {
        var sb = new StringBuilder();
        var (stacks, moves) = ParseData(data);

        foreach (var move in moves)
        {
            var oldStack = stacks[move.FromWhere - 1];
            var newStack = stacks[move.ToWhere - 1];

            for (var i = 0; i < move.HowMany; i++)
            {
                var value = oldStack.Pop();
                newStack.Push(value);
            }
        }

        foreach (var stack in stacks)
        {
            sb.Append(stack.Peek());
        }

        return sb.ToString();
    }

    public static string Solve2(string data)
    {
        var sb = new StringBuilder();
        var (stacks, moves) = ParseData(data);

        foreach (var move in moves)
        {
            var oldStack = stacks[move.FromWhere - 1];
            var newStack = stacks[move.ToWhere - 1];
            var tempStack = new Stack<char>();

            for (var i = 0; i < move.HowMany; i++)
            {
                var value = oldStack.Pop();
                tempStack.Push(value);
            }

            while (tempStack.Count > 0)
            {
                var value = tempStack.Pop();
                newStack.Push(value);
            }
        }

        foreach (var stack in stacks)
        {
            sb.Append(stack.Peek());
        }

        return sb.ToString();
    }
}
