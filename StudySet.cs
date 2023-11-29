public class StudySet : Directory
{
    private Dictionary<string, string> _studyItems;
    private List<string> _mastered;
    private List<string> _stillLearning;
    private List<string> _unkown;
    private Dictionary<string, bool> _settings;

    public StudySet(string name, Folder parent)
        : base(name, parent)
    {
        _studyItems = new Dictionary<string, string>();

        _settings = new Dictionary<string, bool>();
        _settings.Add("answer-Term", true);
        _settings.Add("shuffleTerms", true);
    }

    public void AddTerm(string term, string definition)
    {
        _studyItems.Add(term, definition);

        return;
    }

    public void RemoveTerm(string term)
    {
        _studyItems.Remove(term);

        return;
    }

    public void DisplayTerms(int startingIndex)
    {
        int i = startingIndex;

        foreach(KeyValuePair<string, string> term in _studyItems)
        {
            Console.WriteLine($"\n{i}. {term.Key}: {term.Value}");
            i++;
        }

        Console.ReadLine();
    }

    public void UploadNotes()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();

        using (StreamReader reader = new StreamReader(filename))
        {
            string content = reader.ReadToEnd();

            string[] fileEntries = content.Split('\n');

            foreach(string entry in fileEntries)
            {
                if (entry.Contains(";"))
                {
                    string[] splitTerms = entry.Split(';');
                    _studyItems.Add(splitTerms[0], splitTerms[1]);
                }
            }
        }

        Console.WriteLine("Load complete.");
    }
}