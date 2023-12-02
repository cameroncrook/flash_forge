using System.Xml.XPath;

public class Write : Activity
{
    public Write(StudySet studySet)
        : base(studySet)
    {}

    public override bool StudyTerm(string term)
    {
        (string, string) displayAnswer = base.GetDisplayAnswer(term);

        Console.WriteLine(displayAnswer.Item1);

        Console.Write("\nAnswer: ");
        string answer = Console.ReadLine()!;

        if (answer.ToLower() == displayAnswer.Item2.ToLower())
        {
            Console.WriteLine("\nCORRECT!");
            string pause = Console.ReadLine()!;

            return true;
        }
        else
        {
            Console.WriteLine("\nWrong.");

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