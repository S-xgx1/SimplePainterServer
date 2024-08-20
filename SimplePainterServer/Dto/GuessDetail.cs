namespace SimplePainterServer.Dto;

public class GuessDetail(int id, int imageId, string word, int userId) : GuessDto(id, imageId, word, userId)
{
    public string SuccessWord { get; set; }
}