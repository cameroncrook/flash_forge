public class StudySet : Directory
{
    private Dictionary<string, string> _studyItems;
    private List<string> _mastered;
    private List<string> _stillLearning;
    private List<string> _unkown;
    private Dictionary<string, bool> _settings;

    public StudySet(string name, Directory parent)
        : base(name, parent)
    {
        _settings.Add("answerWTerm", true);
        _settings.Add("shuffleTerms", true);
    }

    public void AddTerm(string term, string definition)
    {
        _studyItems.Add(term, definition);

        return;
    }

    public void RemoveTerm(string term)
    {

    }

    public void UploadNotes()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();
    }

    public override void DisplayContents(int startingIndex)
    {
        return;
    }
}