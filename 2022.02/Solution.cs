namespace _2022._02;

public record Turn(char OpponentsChoice, char YourChoice);

internal static class Solution
{
    private const char YourRock = 'X';
    private const char YourPaper = 'Y';
    private const char YourScissors = 'Z';

    private const char OpponentsRock = 'A';
    private const char OpponentsPaper = 'B';
    private const char OpponentsScissors = 'C';

    private static List<Turn> ParseData(string data)
    {
        var lines = data.Split("\r\n");
        var list = new List<Turn>();

        foreach (var line in lines)
        {
            var turn = line switch
            {
                [var opponentsChoice, _, var yourChoice] => new Turn(opponentsChoice, yourChoice),
                _ => throw new InvalidOperationException($"line '{line}' is not in the expected format!")

            };

            list.Add(turn);
        }

        return list;
    }

    public static int Solve1(string data)
    {
        var turns = ParseData(data);

        var totalPoints = 0;
        foreach (var turn in turns)
        {
            totalPoints += GetTurnPoints1(turn);
        }

        return totalPoints;
    }

    private static int GetTurnPoints1(Turn turn)
    {
        var points = 0;

        if (turn.YourChoice == YourRock)
            points += 1;
        else if (turn.YourChoice == YourPaper)
            points += 2;
        else if (turn.YourChoice == YourScissors)
            points += 3;
        else throw new Exception($"unknown {nameof(turn.YourChoice)} = '{turn.YourChoice}'");

        if (turn.YourChoice == YourRock && turn.OpponentsChoice == OpponentsRock ||
            turn.YourChoice == YourScissors && turn.OpponentsChoice == OpponentsScissors ||
            turn.YourChoice == YourPaper && turn.OpponentsChoice == OpponentsPaper)
            points += 3;
        else if (turn.YourChoice == YourRock && turn.OpponentsChoice == OpponentsScissors ||
            turn.YourChoice == YourScissors && turn.OpponentsChoice == OpponentsPaper ||
            turn.YourChoice == YourPaper && turn.OpponentsChoice == OpponentsRock)
            points += 6;
        else points += 0;

        return points;
    }

    public static int Solve2(string data)
    {
        var turns = ParseData(data);

        var totalPoints = 0;
        foreach (var turn in turns)
        {
            totalPoints += GetTurnPoints2(turn);
        }

        return totalPoints;
    }

    private static int GetTurnPoints2(Turn turn)
    {
        turn = FixTurn(turn);

        return GetTurnPoints1(turn);
    }

    private static Turn FixTurn(Turn turn)
    {
        const char Lose = 'X';
        const char Draw = 'Y';
        const char Win = 'Z';

        if (turn.YourChoice == Lose)
        {
            if (turn.OpponentsChoice == OpponentsRock) return turn with { YourChoice = YourScissors };
            if (turn.OpponentsChoice == OpponentsScissors) return turn with { YourChoice = YourPaper };
            if (turn.OpponentsChoice == OpponentsPaper) return turn with { YourChoice = YourRock };
        }
        else if (turn.YourChoice == Draw)
        {
            if (turn.OpponentsChoice == OpponentsRock) return turn with { YourChoice = YourRock };
            if (turn.OpponentsChoice == OpponentsScissors) return turn with { YourChoice = YourScissors };
            if (turn.OpponentsChoice == OpponentsPaper) return turn with { YourChoice = YourPaper };
        }
        else if (turn.YourChoice == Win)
        {
            if (turn.OpponentsChoice == OpponentsRock) return turn with { YourChoice = YourPaper };
            if (turn.OpponentsChoice == OpponentsScissors) return turn with { YourChoice = YourRock };
            if (turn.OpponentsChoice == OpponentsPaper) return turn with { YourChoice = YourScissors };
        }

        throw new Exception($"failed to fix turn: {turn}");
    }
}
