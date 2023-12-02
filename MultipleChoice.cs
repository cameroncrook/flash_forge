public class MultipleChoice : Activity
{
    public MultipleChoice(StudySet studySet)
        : base(studySet)
    {}

    public override bool StudyTerm(string term)
    {
        (string, string) displayAnswer = base.GetDisplayAnswer(term);

        Console.WriteLine(displayAnswer.Item1);

        List<string> terms = base.GetRandomTerms();
        terms.Add(displayAnswer.Item1);

        // Get multiple choice options
        Dictionary<char, string> groupOfAnswer = new Dictionary<char, string>();
        for(char letter = 'a'; letter <= 'd'; letter++)
        {
            Random random = new Random();
            int randomTermIndex = random.Next(terms.Count);

            string randomTerm = terms[randomTermIndex];
            terms.Remove(randomTerm);

            (string, string) termDef = base.GetDisplayAnswer(randomTerm);
            groupOfAnswer.Add(letter, termDef.Item2);
        }

        foreach(var option in groupOfAnswer)
        {
            Console.WriteLine($"{option.Key}. {option.Value}");
        }

        Console.Write("\nSelect an answer: ");
        char response = char.Parse(Console.ReadLine()!.ToLower());

        if (groupOfAnswer[response] == displayAnswer.Item2)
        {
            Console.WriteLine("\nCORRECT!");

            string pause = Console.ReadLine()!;
            return true;
        }
        else
        {
            Console.WriteLine("Wrong");
            Console.WriteLine("\nThe correct answer is:");
            Console.WriteLine(displayAnswer.Item2);

            string pause = Console.ReadLine()!;
            return false;
        }
    }

    public override Dictionary<string, bool> PlaySession()
    {
        List<string> studyTerms = base.GetSessionTerms();
        Dictionary<string, bool> termCategoryUpdate = new Dictionary<string, bool>();

        for(int i=0; i < studyTerms.Count; i++)
        {
            Console.WriteLine($"{i+1}/{studyTerms.Count}\n");

            bool studyResult = StudyTerm(studyTerms[i]);

            termCategoryUpdate.Add(studyTerms[i], studyResult);
        }

        return termCategoryUpdate;
    }
}