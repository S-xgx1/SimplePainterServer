namespace SimplePainterServer.Dto;

public class ImageInfoDto(int id, int wordId, int userId)
{
    public int ID { get; set; } = id;
    public int WordId { get; set; } = wordId;
    public int UserID { get; set; } = userId;
}