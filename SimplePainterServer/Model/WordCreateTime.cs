namespace SimplePainterServer.Model;

public class WordCreateTime
{
    public int            Id         { get; set; }
    public WordInfo       WordInfo   { get; set; } = null!;
    public int            WordInfoId { get; set; }
    public DateTime       DateTime   { get; set; }
    public CreateTimeType Type       { get; set; }

    public WordCreateTime()
    {
    }

    public WordCreateTime(int wordId, DateTime dateTime, CreateTimeType type)
    {
        WordInfoId = wordId;
        DateTime = dateTime;
        Type = type;
    }
}