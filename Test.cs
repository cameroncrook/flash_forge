public class Test : Activity
{
    public Test(StudySet studySet)
        : base(studySet)
    {}

    public override bool StudyTerm(string term)
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, bool> PlaySession()
    {
        throw new NotImplementedException();
    }
}