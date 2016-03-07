using System.Xml;
using System.Xml.Serialization;

public class EnemyVO
{
    [XmlAttribute("id")]
    public int id;

    [XmlAttribute("type")]
    public string type;

    [XmlElement("resource")]
    public string resource;

    [XmlElement("hp")]
    public int hp;

    [XmlElement("damage")]
    public int damage;

    [XmlElement("speed")]
    public float speed;

    [XmlElement("attackSpeed")]
    public float attackSpeed;

    [XmlElement("distance")]
    public float distance;
}
