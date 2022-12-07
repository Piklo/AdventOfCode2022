using System.Text;

namespace _2022._07;
internal static class Solution
{
    private static void ParseCd(string command, Stack<string> directories)
    {
        var nextDirectory = command.Trim().Split(" ") switch
        {
            [_, var directory] => directory,
            _ => throw new Exception($"failed to get next directory from '{command}'")
        };

        if (nextDirectory == "/")
        {
            while (directories.Count > 0)
            {
                directories.Pop();
            }

            directories.Push(nextDirectory);
        }
        else if (nextDirectory == "..")
        {
            directories.Pop();
        }
        else
        {
            directories.Push(nextDirectory);
        }
    }

    private static string GetParentLocation(Stack<string> directories)
    {
        var stringBuilder = new StringBuilder();

        foreach (var directory in directories.Reverse().SkipLast(1))
        {
            stringBuilder.Append(directory);

            if (directory != "/")
                stringBuilder.Append('/');
        }

        return stringBuilder.ToString();
    }

    private static void ParseLs(string command, Stack<string> currentDirectories, Dictionary<string, Directory> directories)
    {
        var parentLocation = GetParentLocation(currentDirectories);

        var currentName = currentDirectories.Peek();

        var currentLocation = parentLocation + currentName;
        if (!currentLocation.EndsWith("/"))
            currentLocation += "/";

        var currentDirectoryExists = directories.TryGetValue(currentLocation, out var currentDirectory);

        if (!currentDirectoryExists) throw new Exception($"failed to get current directory = '{currentLocation}'");

        var listedItems = command.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)[1..]; //first item is the command

        foreach (var item in listedItems)
        {
            if (item.StartsWith("dir"))
            {
                var newDirectoryName = item.Split(' ') switch
                {
                    [_, var name] => name,
                    _ => throw new Exception($"failed to get dir name from {item}")
                };

                var newFullLocation = currentLocation + newDirectoryName + '/';
                var newDirectory = new Directory(newDirectoryName);

                currentDirectory.ChildrenDirectories.Add(newDirectory);

                directories.TryAdd(newFullLocation, newDirectory);
            }
            else
            {
                var (size, name) = item.Split(' ') switch
                {
                    [var sizeString, var nameString] => (int.Parse(sizeString), nameString),
                    _ => throw new Exception($"failed to get size and name from {item}")
                };

                var file = new File(name, size);

                currentDirectory.Files.Add(file);
            }
        }
    }

    private static Dictionary<string, Directory> ParseData(string data)
    {
        var currentDirectories = new Stack<string>();
        var directories = new Dictionary<string, Directory>
        {
            { "/", new Directory("/") }
        };

        var commands = data.Split("$ ", StringSplitOptions.RemoveEmptyEntries);


        foreach (var command in commands)
        {
            if (command.StartsWith("cd"))
            {
                ParseCd(command, currentDirectories);
            }
            else if (command.StartsWith("ls"))
            {
                ParseLs(command, currentDirectories, directories);
            }
            else
            {
                throw new Exception($"unknown command '{command}'");
            }
        }

        return directories;
    }

    public static int Solve1(string data)
    {
        var result = 0;

        var directories = ParseData(data);

        foreach (var (key, directory) in directories)
        {
            var size = directory.GetFullSize();

            if (size <= 100_000)
            {
                result += size;
            }
        }

        return result;
    }

    public static int Solve2(string data)
    {
        const int spaceAvailable = 70_000_000;
        const int spaceRequired = 30_000_000;

        var directories = ParseData(data);

        var totalSpaceUsed = directories["/"].GetFullSize();
        var freeSpace = spaceAvailable - totalSpaceUsed;
        var spaceMissing = spaceRequired - freeSpace;

        var currentSmallest = totalSpaceUsed;
        foreach (var (key, directory) in directories)
        {
            var size = directory.GetFullSize();

            if (size >= spaceMissing && size < currentSmallest)
            {
                currentSmallest = size;
            }
        }

        return currentSmallest;
    }
}
