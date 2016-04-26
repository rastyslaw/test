using System.Xml;
using System.Xml.Serialization;

public class EnemyVO
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("stageId")]
    public int stageId = 1;

    [XmlAttribute("type")]
    public string type;

    [XmlElement("resource")]
    public string resource;

    [XmlElement("hp")]
    public float hp;

    [XmlElement("power")]
    public int power;

    [XmlElement("damage")]
    public int damage;

    [XmlElement("speed")]
    public float speed;

    [XmlElement("attackSpeed")]
    public float attackSpeed;

    [XmlElement("distance")]
    public float distance;
}
