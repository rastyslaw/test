using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace commands.test
{
    class ParseXMLCommand: Command
    {
        public override void Execute()
        {
            Debug.Log(GetType() + " execute");

            EnemyContainer cont = EnemyContainer.Load(Names.ENEMIES);
            DataModel.SetValue(Names.ENEMIES, cont.enemies);

            XmlDocument xmlDoc = new XmlDocument();
            ParseOptions(xmlDoc);
            ParseWeapons(xmlDoc);
            ParseUpgrades(xmlDoc); 
        }

        void ParseOptions(XmlDocument xmlDoc)
        {
            TextAsset xmlData = Resources.Load<TextAsset>("options");
            xmlDoc.LoadXml(xmlData.text);
            XmlElement element = xmlDoc.DocumentElement;
            foreach (XmlNode node in element)
            {
                DataModel.SetValue(node.Name, node.InnerXml);
            }
        }

        void ParseWeapons(XmlDocument xmlDoc)
        {
            TextAsset xmlData = Resources.Load<TextAsset>("weapons");
            xmlDoc.LoadXml(xmlData.text);
            XmlElement element = xmlDoc.DocumentElement;
            List<WeaponVO> weapons = new List<WeaponVO>();
            foreach (XmlNode node in element)
            {
                weapons.Add(new WeaponVO(node));
            }
            DataModel.SetValue(Names.WEAPONS, weapons);
        }

        void ParseUpgrades(XmlDocument xmlDoc)
        {
            TextAsset xmlData = Resources.Load<TextAsset>("upgrades");
            xmlDoc.LoadXml(xmlData.text);
            XmlElement element = xmlDoc.DocumentElement;
            List<UpgradeVO> upgrades = new List<UpgradeVO>();
            foreach (XmlNode node in element)
            {
                upgrades.Add(new UpgradeVO(node));
            }
            DataModel.SetValue(Names.UPGRADES, upgrades);
        }
    }
}
