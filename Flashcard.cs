public class Flashcard : Activity
{
    public Flashcard(StudySet studySet)
        : base(studySet)
    {
    }

    public override bool StudyTerm(string term, bool showResults=true)
    {
        (string, string) displayAnswer = base.GetDisplayAnswer(term);

        Console.WriteLine("TERM:");
        Console.WriteLine(displayAnswer.Item1);

        Console.Write("\nPress any button to view definition");
        string pause = Console.ReadLine()!;

        Console.WriteLine("ANSWER:");
        Console.WriteLine(displayAnswer.Item2);

        Console.Write("\nDid you get this correct? [y/n] ");
        string response = Console.ReadLine()!;

        return response.ToLower() == "y";
    }

    public override Dictionary<string, bool> PlaySession()
    {
        List<string> studyTerms = base.GetSessionTerms();
        Dictionary<string, bool> termCategoryUpdate = new Dictionary<string, bool>();

        for(int i=0; i < studyTerms.Count; i++)
        {
            Console.Clear();
            Console.WriteLine($"{i+1}/{studyTerms.Count}\n");

            bool studyResult = StudyTerm(studyTerms[i]);

            termCategoryUpdate.Add(studyTerms[i], studyResult);
        }

        return termCategoryUpdate;
    }
}