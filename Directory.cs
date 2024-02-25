public class Directory
{
    private string _name;
    private string? _parentId;

    public Directory(string name, string? parentId)
    {
        _name = name;

        if (parentId == "")
        {
            _parentId = null;
        }
        else
        {
            _parentId = parentId;
        }
    }

    public void setAttributes(string name, string parentId)
    {
        _name = name;
        _parentId = parentId;

        return;
    }

    public string? GetParentId()
    {
        return _parentId;
    }

    public void DisplayData()
    {
        Console.WriteLine(_name);
        Console.WriteLine(_parentId);

        return;
    }

    // public string GetName()
    // {
    //     return _name;
    // }

    // public Folder GetParent()
    // {
    //     return _parentDirectory;
    // }
}