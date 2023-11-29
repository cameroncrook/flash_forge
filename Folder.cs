public class Folder : Directory
{
    private List<StudySet> _sets;
    private List<Folder> _directories;

    public Folder(string name, Folder parent)
        : base(name, parent)
    {
        _sets = new List<StudySet>();
        _directories = new List<Folder>();
    }

    public void AddFolder(Folder folder)
    {
        _directories.Add(folder);

        return;
    }

    public void RemoveFolder(int index)
    {
        try
        {
            _directories.RemoveAt(index);
        }
        catch (IndexOutOfRangeException err)
        {
            Console.WriteLine("Invalid answer.");
        }
    }

    public void AddSet(StudySet studySet)
    {
        _sets.Add(studySet);

        return;
    }

    public void RemoveSet(int index)
    {
        try
        {
            _sets.RemoveAt(index);
        }
        catch (IndexOutOfRangeException err)
        {
            Console.WriteLine("Invalid answer.");
        }
    }

    public void DisplayContents(int startingIndex)
    {
        int i = startingIndex;
        
        if (_directories.Count > 0)
        {
            Console.WriteLine("\nFolders");
            foreach(Folder folder in _directories)
            {
                Console.WriteLine($"{i}. {folder.GetName()}");
                i++;
            }
        }
        
        if (_sets.Count > 0)
        {
            Console.WriteLine("\nStudy Sets");
            foreach(StudySet set in _sets)
            {
                Console.WriteLine($"{i}. {set.GetName()}");
                i++;
            }
        }
    }

    public void DisplayDirectories()
    {
        for (int i = 0; i < _directories.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_directories[i].GetName()}");
        }
    }

    public void DisplayStudySets()
    {
        for (int i = 0; i < _sets.Count; i++)
        {
            Console.WriteLine($"{i+1}. {_sets[i].GetName()}");
        }
    }

    public int GetDirectoryCount()
    {
        return _directories.Count;
    }

    public Folder GetDirectory(int index)
    {
        return _directories[index];
    }

    public StudySet GetStudySet(int index)
    {
        return _sets[index];
    }
}