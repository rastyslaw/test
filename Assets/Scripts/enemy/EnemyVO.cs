using System;
using System.Xml;
using System.Xml.Serialization;
using Assets.Scripts.enemy;

public class EnemyVO: IClonable
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
    
    public object Clone()
    {
        var copy = (EnemyVO)MemberwiseClone();
        // Deep-copy children
        //copy.Children = Children.Select(c => c.Clone()).ToList();
        return copy;
    }
}
