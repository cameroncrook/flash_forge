public abstract class Activity
{
    private StudySet _studySet;

    public Activity(StudySet studySet)
    {
        _studySet = studySet;
    }

    public abstract bool StudyTerm(string term, bool showResults);

    public abstract Dictionary<string, bool> PlaySession();

    public List<string> GetSessionTerms(int size=7)
    {
        // Performs the following algorithm to get terms to study.

        double masteredRatio = 0.14;
        double learningRatio = 0.29;
        double unknownRatio = 0.57;

        int unknownReq = (int)Math.Round(unknownRatio * size);
        int learningReq = (int)Math.Round(learningRatio * size);
        int masteredReq = (int)Math.Round(masteredRatio * size);

        List<string> studyTerms = new List<string>();
        int unknownCount = 0;
        int learningCount = 0;
        int masteredCount = 0;

        (List<string>, List<string>, List<string>) categories = _studySet.GetClassifications();
        List<string> unknown = new List<string>(categories.Item1);
        List<string> learning = new List<string>(categories.Item2);
        List<string> mastered = new List<string>(categories.Item3);

        do
        {
            if (unknownCount < unknownReq && unknown.Count > 0)
            {
                string term = GetRandomCategory(unknown);

                studyTerms.Add(term);
                unknown.Remove(term);

                unknownCount++;
            }
            else if (learningCount < learningReq && learning.Count > 0)
            {
                string term = GetRandomCategory(learning);
                studyTerms.Add(term);
                learning.Remove(term);

                learningCount++;
            }
            else if (masteredCount < masteredReq && mastered.Count > 0)
            {
                string term = GetRandomCategory(mastered);

                studyTerms.Add(term);
                mastered.Remove(term);

                masteredCount++;
            }
            else
            {
                // Default to unknown, then to learning, then to mastered
                if (unknown.Count > 0)
                {
                    string term = GetRandomCategory(unknown);

                    studyTerms.Add(term);
                    unknown.Remove(term);
                }
                else if (learning.Count > 0)
                {
                    string term = GetRandomCategory(learning);

                    studyTerms.Add(term);
                    learning.Remove(term);
                }
                else if (mastered.Count > 0)
                {
                    string term = GetRandomCategory(mastered);

                    studyTerms.Add(term);
                    mastered.Remove(term);
                }
                else
                {
                    break;
                }
            }
        } while (studyTerms.Count < size || (unknown.Count < 1 && learning.Count < 1 && mastered.Count < 1));

        return studyTerms;
    }

    public string GetRandomCategory(List<string> category)
    {
        Random random = new Random();

        int index = random.Next(category.Count);
        string randomValue = category[index];
        
        return randomValue;
    }

    public (string, string) GetDisplayAnswer(string term)
    {
        string definition = _studySet.GetTermDefinition(term);

        bool answerWithTerm = _studySet.GetStudySetting();
        string display = "";
        string answer = "";

        if (answerWithTerm)
        {
            display = definition;
            answer = term;
        }
        else
        {
            display = term;
            answer = definition;
        }

        return (display, answer);
    }

    public List<string> GetRandomTerms()
    {
        return _studySet.GetRandomTerms(3);
    }

    public StudySet ShareSet()
    {
        return _studySet;
    }
}