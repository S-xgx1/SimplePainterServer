namespace SimplePainterServer.Model;

public class WordCreateTimeDto
{
    public int            Id         { get; set; }
    public int            WordInfoId { get; set; }
    public DateTime       DateTime   { get; set; }
    public CreateTimeType Type       { get; set; }

    public WordCreateTimeDto(int wordId, DateTime dateTime, CreateTimeType type)
    {
    }
}