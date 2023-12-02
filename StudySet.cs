using System.Linq.Expressions;
using System.Reflection;

public class StudySet : Directory
{
    private Dictionary<string, string> _studyItems;
    private List<string> _mastered;
    private List<string> _stillLearning;
    private List<string> _unkown;
    private bool _answerWithTerm;

    public StudySet(string name, Folder parent)
        : base(name, parent)
    {
        _studyItems = new Dictionary<string, string>();
        _mastered = new List<string>();
        _stillLearning = new List<string>();
        _unkown = new List<string>();

        _answerWithTerm = true;
    }

    public void AddTerm(string term, string definition)
    {
        _studyItems.Add(term, definition);
        _unkown.Add(term);

        return;
    }

    public void RemoveTerm(int index)
    {
        List<string> keysList = new List<string>(_studyItems.Keys);

        string term = keysList[index];

        _studyItems.Remove(term);

        UncatagorizeTerm(term);

        return;
    }

    public string GetTermDefinition(string term)
    {
        return _studyItems[term];
    }

    public void DisplayTerms(int startingIndex)
    {
        int i = startingIndex;

        foreach(KeyValuePair<string, string> term in _studyItems)
        {
            Console.WriteLine($"\n{i}. {term.Key}: {term.Value}");
            i++;
        }
    }

    public void UploadNotes()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine()!;

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

                    _unkown.Add(splitTerms[0]);
                }
            }
        }

        Console.WriteLine("Load complete.");
    }

    public void AddtoMastered(string term)
    {
        _mastered.Add(term);

        return;
    }

    public void AddtoLearning(string term)
    {
        _stillLearning.Add(term);

        return;
    }

    public void AddtoUnknown(string term)
    {
        _unkown.Add(term);

        return;
    }

    public void UncatagorizeTerm(string term)
    {
        if (_unkown.Contains(term))
        {
            _unkown.Remove(term);
        }
        else if (_stillLearning.Contains(term))
        {
            _stillLearning.Remove(term);
        }
        else if (_mastered.Contains(term))
        {
            _mastered.Remove(term);
        }

        return;
    }

    public void UpdateTermClassifactions(Dictionary<string, bool> termResults)
    {
        
    }

    public (List<string>, List<string>, List<string>) GetClassifications()
    {
        return (_unkown, _stillLearning, _mastered);
    }

    public List<int> GetClassificationSizes()
    {
        List<int> sizes = new List<int>{_mastered.Count, _stillLearning.Count, _unkown.Count};

        return sizes;
    }

    public bool GetStudySetting()
    {
        return _answerWithTerm;
    }

    public List<string> GetRandomTerms(int quantity=1)
    {
        ICollection<string> dictKeys = _studyItems.Keys;
        List<string> keys = dictKeys.ToList();
        List<string> terms = new List<string>();

        for(int i=0; i < quantity; i++)
        {
            Random random = new Random();

            int index = random.Next(keys.Count);

            terms.Add(keys[index]);
        }

        return terms;
    }
}