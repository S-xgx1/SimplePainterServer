namespace SimplePainterServer.Model;

public class Guess
{
    public Guess()
    {
    }

    public Guess(int imageId, string word, int userId)
    {
        ImageId = imageId;
        Word = word;
        UserID = userId;
    }
    public int ID { get; set; }
    public int ImageId { get; set; }
    public ImageInfo Image { get; set; }
    public string Word { get; set; }
    public int UserID { get; set; }
    public UserInfo User { get; set; }
}