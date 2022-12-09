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

    private static Point MoveTail(Point headPoint, Point tailPoint)
    {
        var movement = (X: headPoint.X - tailPoint.X, Y: headPoint.Y - tailPoint.Y);

        if (Math.Abs(movement.X) <= 1 && Math.Abs(movement.Y) <= 1)
        {
            return tailPoint;
        }

        var newTailPoint = tailPoint with { X = tailPoint.X + Math.Sign(movement.X), Y = tailPoint.Y + Math.Sign(movement.Y) };
        return newTailPoint;
    }
    private static void Simulate(Move move, List<Point> points, HashSet<Point> allTailPoints)
    {
        for (var i = 0; i < move.Steps; i++)
        {
            var head = points[0];
            head = MoveHead(move.Direction, head);
            points[0] = head;
            for (var tailI = 1; tailI < points.Count; tailI++)
            {
                head = points[tailI - 1];
                var tail = points[tailI];

                tail = MoveTail(head, tail);
                points[tailI] = tail;

                allTailPoints.Add(points.Last());
            }
        }
    }
    public static int Solve1(string data)
    {
        var moves = ParseMoves(data);

        var allTailPoints = new HashSet<Point>() { };

        var points = new List<Point>()
        {
            new (0,0),
            new (0,0)
        };

        foreach (var move in moves)
        {
            Simulate(move, points, allTailPoints);
        }

        return allTailPoints.Count;
    }
    public static int Solve2(string data)
    {
        var moves = ParseMoves(data);

        var allTailPoints = new HashSet<Point>() { };

        var points = new List<Point>();

        for (var i = 0; i < 9; i++)
        {
            points.Add(new(0, 0));
        }

        foreach (var move in moves)
        {
            Simulate(move, points, allTailPoints);
        }

        Print(allTailPoints);

        return allTailPoints.Count;
    }

    private static void Print(HashSet<Point> points)
    {
        var seen = new bool[points.Count, points.Count];
        var offSetX = 0;
        var offSetY = 0;

        foreach (var point in points)
        {
            if (-point.X > offSetX) offSetX = -point.X;
            if (-point.Y > offSetY) offSetY = -point.Y;
        }

        foreach (var point in points)
        {
            seen[point.X + offSetX, point.Y + offSetY] = true;
        }

        var entire = "";
        for (var i = 0; i < points.Count; i++)
        {
            var str = "";
            for (var j = 0; j < points.Count; j++)
            {

                var line = seen[i, j];

                if (line) str += "#";
                else str += ".";
            }

            //entire = str + "\n" + entire;
            entire += str + "\n";
        }

        Console.WriteLine(entire);
    }
}
