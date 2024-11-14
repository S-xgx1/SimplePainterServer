namespace SimplePainterServer.Dto;

public class WordInfoDto(int id, string name, string partSpeech)
{
    public int    ID         { get; set; } = id;
    public string Name       { get; set; } = name;
    public string PartSpeech { get; set; } = partSpeech;
}