namespace SimplePainterServer.Dto;

public class ImageInfoDetail(int id, int wordId, int userId, int guessCount, int correctCount)
    : ImageInfoDto(id, wordId, userId)
{
    public int GuessCount   { get; set; }
    public int CorrectCount { get; set; }
    public string GuessWord { get; set; }
}