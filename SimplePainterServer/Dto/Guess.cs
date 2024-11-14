namespace SimplePainterServer.Dto;

public class GuessDto(int id, int imageId, string word, int userId)
{
    public int    ID      { get; set; } = id;
    public int    ImageId { get; set; } = imageId;
    public string Word    { get; set; } = word;
    public int    UserID  { get; set; } = userId;
}