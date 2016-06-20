using UnityEngine;
using System.Xml;

public class UpgradeVO : MonoBehaviour 
{
    private readonly int _id; 
    private readonly string _type;
    private readonly string _tabName;

    public UpgradeVO(XmlNode data)
    {
        _id = int.Parse(data.Attributes["id"].Value ?? "");
        _type = data.Attributes["type"].Value;
        _tabName = data.ChildNodes[0].InnerText;
    }

    public int Id
    {
        get { return _id; }
    }

    public string Type
    {
        get { return _type; }
    }

    public string TabName
    {
        get { return _tabName; }
    }
}

