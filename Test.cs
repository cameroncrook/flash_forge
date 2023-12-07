public class Test : Activity
{
    public Test(StudySet studySet)
        : base(studySet)
    {}

    public override bool StudyTerm(string term, bool showResults=false)
    {
        StudySet studySet = base.ShareSet();

        List<Activity> type = new List<Activity>();
        Flashcard flashcard = new Flashcard(studySet);
        type.Add(flashcard);
        Write write = new Write(studySet);
        type.Add(write);
        MultipleChoice multipleChoice = new MultipleChoice(studySet);
        type.Add(multipleChoice);

        Random random = new Random();
        int index = random.Next(type.Count);

        bool result = type[index].StudyTerm(term, showResults);

        return result;
    }

    public override Dictionary<string, bool> PlaySession()
    {
        List<string> studyTerms = base.GetSessionTerms(15);
        Dictionary<string, bool> termCategoryUpdate = new Dictionary<string, bool>();

        for(int i=0; i < studyTerms.Count; i++)
        {
            Console.Clear();
            Console.WriteLine($"{i+1}/{studyTerms.Count}\n");

            bool studyResult = StudyTerm(studyTerms[i]);

            termCategoryUpdate.Add(studyTerms[i], studyResult);
        }

        Console.Clear();
        int correctCounter = 0;
        Console.WriteLine("\nRESULTS:");
        int ind = 1;
        foreach(KeyValuePair<string, bool> result in termCategoryUpdate)
        {
            string translatedResult = "";
            if (result.Value)
            {
                translatedResult = "Correct";
                correctCounter++;
            }
            else
            {
                translatedResult = "Wrong";
            }

            Console.WriteLine($"{ind}. {result.Key} - {translatedResult}");

            ind++;
        }

        Console.WriteLine($"Score: {correctCounter}/{termCategoryUpdate.Count}");

        Console.Write("Press 'Enter' to continue...");
        Console.ReadLine();

        return termCategoryUpdate;
    }
}