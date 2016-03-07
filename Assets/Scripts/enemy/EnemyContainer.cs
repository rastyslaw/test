using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("enemiesCollection")]
public class EnemyContainer
{
    [XmlArray("enemies")]
    [XmlArrayItem("enemy")]
    public List<EnemyVO> enemies = new List<EnemyVO>();

    public static EnemyContainer Load(string path) 
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(EnemyContainer));

        StringReader reader = new StringReader(_xml.text);

        EnemyContainer enemies = serializer.Deserialize(reader) as EnemyContainer;  

        return enemies;
    }
}
