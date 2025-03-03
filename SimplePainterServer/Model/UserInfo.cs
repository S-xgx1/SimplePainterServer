namespace SimplePainterServer.Model;

public class UserInfo
{
    public UserInfo()
    {
    }

    public UserInfo(int id, string name, string language, string sex, string age, string career, string educationLevel)
    {
        ID             = id;
        Name           = name;
        Language       = language;
        Sex            = sex;
        Age            = age;
        Career         = career;
        EducationLevel = educationLevel;
    }

    public void Update(UserInfo userInfo)
    {
        Language       = userInfo.Language;
        Sex            = userInfo.Sex;
        Age            = userInfo.Age;
        Career         = userInfo.Career;
        EducationLevel = userInfo.EducationLevel;
    }

    public int                    ID             { get; set; }
    public string                 Name           { get; set; }
    public string                 Language       { get; set; }
    public string                 Sex            { get; set; }
    public string                 Age            { get; set; }
    public string                 Career         { get; set; }
    public string                 EducationLevel { get; set; }
    public ICollection<Guess>     Guesses        { get; set; }
    public ICollection<ImageInfo> Images         { get; set; }
    public ICollection<WordInfo>  Words          { get; set; }
}