using System.Linq.Expressions;
using System.Reflection;
using System;
using System.IO;

public class StudySet : Directory
{
    private Dictionary<string, string> _studyItems;
    private List<string> _mastered;
    private List<string> _stillLearning;
    private List<string> _unkown;
    private bool _answerWithTerm;

    public StudySet(string name, string parentId)
        : base(name, parentId)
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
        Console.WriteLine("INSTRUCTIONS:");
        Console.WriteLine("1. place txt file in /uploads directory.");
        Console.WriteLine("2. File must contain ';' seperated values (TERM;DEFINITION)");
        Console.WriteLine("3. Upload notes and enter full name for file (e.g. 1.3-flashcards.txt)");

        Console.WriteLine("\nUse '1.3-flashcards.txt' to test it out!");

        Console.Write("Filename: ");
        string filename = Console.ReadLine()!;

        string currentDirectory = System.IO.Directory.GetCurrentDirectory();
        string relativePath = $"upload/{filename}";
        string filePath = Path.Combine(currentDirectory, relativePath);

        using (StreamReader reader = new StreamReader(filePath))
        {
            string content = reader.ReadToEnd();

            string[] fileEntries = content.Split('\n');

            foreach(string entry in fileEntries)
            {
                if (entry.Contains(";"))
                {
                    try
                    {
                        string[] splitTerms = entry.Split(';');
                        _studyItems.Add(splitTerms[0], splitTerms[1]);

                        _unkown.Add(splitTerms[0]);
                    }
                    catch (ArgumentException)
                    {
                        continue;
                    }
                }
            }
        }

        Console.WriteLine("Load complete.");
    }

    public string UncatagorizeTerm(string term)
    {
        string category = "";
        if (_unkown.Contains(term))
        {
            _unkown.Remove(term);
            category = "unknown";
        }
        else if (_stillLearning.Contains(term))
        {
            _stillLearning.Remove(term);
            category = "stillLearning";
        }
        else if (_mastered.Contains(term))
        {
            _mastered.Remove(term);
            category = "mastered";
        }

        return category;
    }

    public void UpdateTermClassifactions(Dictionary<string, bool> termResults)
    {
        foreach(KeyValuePair<string, bool> result in termResults)
        {
            string previousCategory = UncatagorizeTerm(result.Key);

            if (result.Value == true)
            {
                if (previousCategory == "unknown")
                {
                    _stillLearning.Add(result.Key);
                }
                else if (previousCategory == "stillLearning" || previousCategory == "mastered")
                {
                    _mastered.Add(result.Key);
                }
            }
            else if (result.Value == false)
            {
                if (previousCategory == "unknown")
                {
                    _unkown.Add(result.Key);
                }
                else
                {
                    _stillLearning.Add(result.Key);
                }
            }
        }
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

    public void UpdateStudySetting(bool setting)
    {
        _answerWithTerm = setting;

        return;
    }

    public List<string> GetRandomTerms(int quantity=3)
    {
        ICollection<string> dictKeys = _studyItems.Keys;
        List<string> keys = new List<string>(dictKeys.ToList());
        List<string> terms = new List<string>();

        for(int i=0; i < quantity; i++)
        {
            if (keys.Count < 1)
            {
                keys = dictKeys.ToList();
            }
            Random random = new Random();

            int index = random.Next(keys.Count);

            terms.Add(keys[index]);
            keys.RemoveAt(index);
        }

        return terms;
    }

    public void DisplayClassification()
    {
        Console.WriteLine("\nUnknown:");
        Console.WriteLine(string.Join(" | ", _unkown));

        Console.WriteLine("\nStill Learning:");
        Console.WriteLine(string.Join(" | ", _stillLearning));

        Console.WriteLine("\nMastered:");
        Console.WriteLine(string.Join(" | ", _mastered));

        return;
    }

    public int GetTermCount()
    {
        ICollection<string> dictKeys = _studyItems.Keys;

        return dictKeys.Count;
    }
}