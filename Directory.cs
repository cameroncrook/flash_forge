public class Directory
{
    private string _name;
    private Folder _parentDirectory;

    public Directory(string name, Folder parent)
    {
        _name = name;
        _parentDirectory = parent;
    }

    public string GetName()
    {
        return _name;
    }

    public Folder GetParent()
    {
        return _parentDirectory;
    }
}