using UnityEngine;
using System.Xml;
using commands;

namespace Assets.Scripts.libs.test
{
    class LocaleCommand : Command
    {
        public override void Execute()
        {
            Debug.Log(GetType() + " execute");

            TextAsset xmlData = Resources.Load<TextAsset>("ru");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData.text);

            LocaleModel.Instance.AddLocale(xmlDoc.DocumentElement);
            LocaleModel.Instance.Locale = LocaleTypes.RU;
        }
    }
}