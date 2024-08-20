namespace SimplePainterServer.Model;

public class WordInfo
{
    public WordInfo()
    {
    }

    public WordInfo(int id, string name, string partSpeech)
    {
        ID = id;
        Name = name;
        PartSpeech = partSpeech;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string PartSpeech { get; set; }
    public ICollection<ImageInfo> Images { get; set; }
}