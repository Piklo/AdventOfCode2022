namespace _2022._09;

internal enum MoveDirection
{
    Right,
    Left,
    Up,
    Down,
}

internal record Move(MoveDirection Direction, int Steps);
internal record Point(int X, int Y);

internal static class Solution
{
    private static Move ParseMove(string direction, string steps)
    {
        var parsedDirection = direction switch
        {
            "R" => MoveDirection.Right,
            "L" => MoveDirection.Left,
            "U" => MoveDirection.Up,
            "D" => MoveDirection.Down,
            _ => throw new Exception($"unknown movement = '{direction}'")
        };

        var parsedSteps = int.Parse(steps);

        return new Move(parsedDirection, parsedSteps);
    }
    private static List<Move> ParseMoves(string data)
    {
        var moves = new List<Move>();

        var lines = data.Split("\r\n");
        foreach (var line in lines)
        {
            var split = line.Split(" ");
            var move = split switch
            {
                [var direction, var steps] => ParseMove(direction, steps),
                _ => throw new Exception($"failed to get movement and steps from line = '{line}'")
            };

            moves.Add(move);
        }

        return moves;
    }

    private static Point MoveHead(MoveDirection direction, Point currentHeadPoint)
    {
        switch (direction)
        {
            case MoveDirection.Left:
                {
                    return currentHeadPoint with { X = currentHeadPoint.X - 1 };
                }
            case MoveDirection.Right:
                {
                    return currentHeadPoint with { X = currentHeadPoint.X + 1 };
                }
            case MoveDirection.Up:
                {
                    return currentHeadPoint with { Y = currentHeadPoint.Y + 1 };
                }
            case MoveDirection.Down:
                {
                    return currentHeadPoint with { Y = currentHeadPoint.Y - 1 };
                }
            default: throw new Exception($"unhandled MoveDirection: {direction}");
        }
    }

    private static Point MoveTail(Point headPoint, Point previousHeadPoint, Point tailPoint)
    {
        var movement = (X: headPoint.X - tailPoint.X, Y: headPoint.Y - tailPoint.Y);

        if (Math.Abs(movement.X) <= 1 && Math.Abs(movement.Y) <= 1)
        {
            return tailPoint;
        }

        //return previousHeadPoint;
        var newTailPoint = tailPoint with { X = tailPoint.X + Math.Sign(movement.X), Y = tailPoint.Y + Math.Sign(movement.Y) };
        return newTailPoint;
    }
    private static (Point currentHeadPoint, Point currentTailPoint) Simulate(Move move,
                                                                             Point currentHeadPoint,
                                                                             Point currentTailPoint,
                                                                             HashSet<Point> allTailPoints)
    {
        for (var i = 0; i < move.Steps; i++)
        {
            var previousHeadPoint = currentHeadPoint;
            currentHeadPoint = MoveHead(move.Direction, currentHeadPoint);
            currentTailPoint = MoveTail(currentHeadPoint, previousHeadPoint, currentTailPoint);
            allTailPoints.Add(currentTailPoint);
        }

        return (currentHeadPoint, currentTailPoint);
    }
    public static int Solve1(string data)
    {
        var moves = ParseMoves(data);

        var currentHeadPoint = new Point(0, 0);
        var currentTailPoint = new Point(0, 0);
        var allTailPoints = new HashSet<Point>() { currentTailPoint };

        foreach (var move in moves)
        {
            (currentHeadPoint, currentTailPoint) = Simulate(move, currentHeadPoint, currentTailPoint, allTailPoints);
        }

        return allTailPoints.Count;
    }
}
