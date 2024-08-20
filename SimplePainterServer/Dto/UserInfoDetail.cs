namespace SimplePainterServer.Dto;

public class UserInfoDetail(int id, string name, string language, string sex, string age, string career, string educationLevel) : UserInfoDto(id, name, language, sex, age, career, educationLevel)
{
    public int DrawCount { get; set; }
    public int GuessCount { get; set; }
    public int GuessSuccessCount { get; set; }
}