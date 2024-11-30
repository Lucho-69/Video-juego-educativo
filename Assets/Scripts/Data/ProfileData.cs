using System.Xml.Serialization;

[XmlRoot("ProfileData")]
public class ProfileData 
{
    [XmlElement("fileName")]
    public string fileName { get; set; }

    [XmlElement("name")]
    public string name { get; set; }

    [XmlElement("newGame")]
    public bool newGame { get; set; }

    [XmlElement("currentLevel")]
    public int currentLevel { get; set; }

    [XmlElement("score")]
    public int score { get; set; }

    public ProfileData()
    {
        this.fileName = "None";
        this.name = "None";
        this.newGame = false;
        this.currentLevel = 1;
        this.score = 0;
    }

    // Constructor parametrizado
    public ProfileData(string name, bool newGame, int currentLevel, int score)
    {
        this.fileName = name.Replace(" ", "_");
        this.name = name;
        this.newGame = newGame;
        this.currentLevel = currentLevel;
        this.score = score;
    }
}
