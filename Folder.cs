public class Folder : Directory
{
    private List<StudySet> _sets;
    private List<Folder> _directories;

    public Folder(string name, Directory parent)
        : base(name, parent)
    {
    }

    public void AddFolder(Folder folder)
    {
        _directories.Add(folder);

        return;
    }

    public void RemoveFolder()
    {

    }

    public void AddSet(StudySet studySet)
    {
        _sets.Add(studySet);

        return;
    }

    public void RemoveSet()
    {

    }

    public override void DisplayContents(int startingIndex)
    {
        int i = startingIndex;
        Console.WriteLine("\nFolders");
        foreach(Folder folder in _directories)
        {
            Console.WriteLine($"{i}. {folder.GetName()}");
            i++;
        }

        Console.WriteLine("\nStudy Sets");
        foreach(StudySet set in _sets)
        {
            Console.WriteLine($"{i}. {set.GetName()}");
            i++;
        }
    }
}