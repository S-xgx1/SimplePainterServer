namespace SimplePainterServer.Model;

public class ImageInfo
{
    public ImageInfo(int id, int wordId, int userId)
    {
        ID = id;
        WordId = wordId;
        UserID = userId;
    }

    public ImageInfo()
    {
    }

    public int ID { get; set; }
    public int WordId { get; set; }
    public WordInfo Word { get; set; }
    public ICollection<Guess> Guesses { get; set; }
    public int UserID { get; set; }
    public UserInfo User { get; set; }
}