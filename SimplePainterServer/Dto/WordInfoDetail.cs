namespace SimplePainterServer.Dto;

public class WordInfoDetail(int id, string name, string partSpeech)
    : WordInfoDto(id, name, partSpeech)
{
    public int DrawCount { get; set; }
}