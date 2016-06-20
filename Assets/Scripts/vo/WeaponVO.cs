using System.Xml;

public class WeaponVO
{
    private readonly int _id;
    private readonly string _resource; 
    private readonly int _damage;
    private readonly float _speed;

    public WeaponVO(XmlNode data)
    {
        _id = int.Parse(data.Attributes["id"].Value ?? "");  
        _resource = data.ChildNodes[0].InnerText; 
        _damage = int.Parse(data.ChildNodes[1].InnerText);  
        _speed = float.Parse(data.ChildNodes[2].InnerText);
    }

    public int Id
    {
        get { return _id; }
    }

    public string Resource
    {
        get { return _resource; }
    }

    public int Damage
    {
        get { return _damage; }
    }

    public float Speed
    {
        get { return _speed; }
    }
}

