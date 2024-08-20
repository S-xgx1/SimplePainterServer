namespace SimplePainterServer.Dto;

public class WordInfoDetail(int id, string name, string partSpeech, int drawCount) : WordInfoDto(id, name, partSpeech)
{
    public int DrawCount { get; set; }
}