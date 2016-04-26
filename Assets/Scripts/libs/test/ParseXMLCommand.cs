using System.Xml;
using UnityEngine;

namespace commands.test
{
    class ParseXMLCommand: Command
    {
        public override void Execute()
        {
            Debug.Log(this.ToString() + " execute");

            EnemyContainer cont = EnemyContainer.Load(Names.ENEMIES);
            DataModel.SetValue(Names.ENEMIES, cont.enemies);
           
            TextAsset xmlData = Resources.Load<TextAsset>("options");
            XmlDocument xmlDoc = new XmlDocument(); 
            xmlDoc.LoadXml(xmlData.text);
            XmlElement options = xmlDoc.DocumentElement;
            foreach (XmlNode node in options)
            {
                DataModel.SetValue(node.Name, node.InnerXml); 
            }
        }
    }
}
