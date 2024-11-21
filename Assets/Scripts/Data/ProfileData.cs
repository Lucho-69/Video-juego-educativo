using System.Xml.Serialization;

[XmlRoot("ProfileData")]
public class ProfileData 
{
    [XmlElement("fileName")] // Mapea el campo 'fileName' con el elemento 'fileName' del XML
    public string fileName { get; set; }

    [XmlElement("name")] // Mapea el campo 'name' con el elemento 'name' del XML
    public string name { get; set; }

    [XmlElement("newGame")] // Mapea el campo 'newGame' con el elemento 'newGame' del XML
    public bool newGame { get; set; }

    [XmlElement("x")] // Mapea el campo 'x' con el elemento 'x' del XML
    public float x { get; set; }

    [XmlElement("y")] // Mapea el campo 'y' con el elemento 'y' del XML
    public float y { get; set; }

    public ProfileData() {
        this.fileName = "None.xml";
        this.name = "None.xml";
        this.newGame = false;
        this.x = this.y = 0;
    }

    public ProfileData (string name, bool newGame, float x, float y)
    {
        this.fileName = name.Replace(" ", "_") + ".xml"; // Corregido el reemplazo
        this.name = name;
        this.newGame = newGame;
        this.x = x;
        this.y = y;
    }
}
