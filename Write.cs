using System.Xml.XPath;

public class Write : Activity
{
    public Write(StudySet studySet)
        : base(studySet)
    {}

    public override bool StudyTerm(string term, bool showResults=true)
    {
        (string, string) displayAnswer = base.GetDisplayAnswer(term);

        Console.WriteLine("TERM");
        Console.WriteLine(displayAnswer.Item1);

        Console.Write("\nAnswer: ");
        string answer = Console.ReadLine()!;

        if (answer.ToLower() == displayAnswer.Item2.ToLower())
        {
            if (showResults)
            {
                Console.WriteLine("\nCORRECT!");
                string pause = Console.ReadLine()!;
            }

            return true;
        }
        else
        {
            if (showResults)
            {
                Console.WriteLine("\nWrong.");

                Console.WriteLine("\nThe correct answer is:");
                Console.WriteLine(displayAnswer.Item2);

                Console.Write("\nType 'c' if you got the answer correct. Otherwise press 'ENTER'");
                string response = Console.ReadLine()!;

                if (response.ToLower() == "c")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
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