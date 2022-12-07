namespace _2022._07;

internal record File(string Name, int Size);
internal class Directory
{
    public string Name { get; }
    public List<Directory> ChildrenDirectories { get; } = new();
    public List<File> Files { get; } = new();

    public Directory(string name)
    {
        Name = name;
    }

    public int GetFilesSize()
    {
        var size = 0;

        foreach (var file in Files)
        {
            size += file.Size;
        }

        return size;
    }

    public int GetChildrenDirectoriesSize()
    {
        var size = 0;

        foreach (var directory in ChildrenDirectories)
        {
            size += directory.GetFullSize();
        }

        return size;
    }

    public int GetFullSize()
    {

        return GetFilesSize() + GetChildrenDirectoriesSize();
    }
}
