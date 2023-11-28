public abstract class Directory
{
    private string _name;
    private Directory _parentDirectory;

    public Directory(string name, Directory parent)
    {
        _name = name;
        _parentDirectory = parent;
    }

    public string GetName()
    {
        return _name;
    }

    public abstract void DisplayContents(int startingIndex);
}