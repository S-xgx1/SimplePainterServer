namespace SimplePainterServer.Dto;

public class UserInfoDto(int    id, string name, string language, string sex, string age, string career,
                         string educationLevel
)
{
    public int    ID             { get; set; } = id;
    public string Name           { get; set; } = name;
    public string Language       { get; set; } = language;
    public string Sex            { get; set; } = sex;
    public string Age            { get; set; } = age;
    public string Career         { get; set; } = career;
    public string EducationLevel { get; set; } = educationLevel;
}