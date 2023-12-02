public abstract class Activity
{
    private StudySet _studySet;

    public Activity(StudySet studySet)
    {
        _studySet = studySet;
    }

    public abstract bool StudyTerm(string term);

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

        do
        {
            if (unknownCount < unknownReq && categories.Item1.Count > 0)
            {
                string term = GetRandomCategory(categories.Item1);

                studyTerms.Add(term);
                categories.Item1.Remove(term);

                unknownCount++;
            }
            else if (learningCount < learningReq && categories.Item2.Count > 0)
            {
                string term = GetRandomCategory(categories.Item2);
                studyTerms.Add(term);
                categories.Item2.Remove(term);

                learningCount++;
            }
            else if (masteredCount < masteredReq && categories.Item3.Count > 0)
            {
                string term = GetRandomCategory(categories.Item3);

                studyTerms.Add(term);
                categories.Item3.Remove(term);

                masteredCount++;
            }
            else
            {
                // Default to unknown, then to learning, then to mastered
                if (categories.Item1.Count > 0)
                {
                    string term = GetRandomCategory(categories.Item1);

                    studyTerms.Add(term);
                    categories.Item1.Remove(term);
                }
                else if (categories.Item2.Count > 0)
                {
                    string term = GetRandomCategory(categories.Item2);

                    studyTerms.Add(term);
                    categories.Item2.Remove(term);
                }
                else if (categories.Item2.Count > 0)
                {
                    string term = GetRandomCategory(categories.Item3);

                    studyTerms.Add(term);
                    categories.Item3.Remove(term);
                }
                else
                {
                    break;
                }
            }
        } while (studyTerms.Count < size || (categories.Item1.Count < 1 && categories.Item2.Count < 1 && categories.Item3.Count < 1));

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
            display = term;
            answer = definition;
        }
        else
        {
            display = definition;
            answer = term;
        }

        return (display, answer);
    }

    public List<string> GetRandomTerms()
    {
        return _studySet.GetRandomTerms(3);
    }
}