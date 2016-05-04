using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;

public class LocaleModel
{
    private Dictionary<string, XmlElement> _xmlDocs = new Dictionary<string, XmlElement>(); 
    private Dictionary<string, string> _data = new Dictionary<string, string>();
    
    private static LocaleModel instance;
    public static LocaleModel Instance 
    {
        get
        {
            if (instance == null) 
            {
                instance = new LocaleModel();
            }
            return instance;
        }
    }

    private string _locale;
    public string Locale
    {
        get { return _locale; }
        set
        {
            _locale = value;

            var xml = _xmlDocs[_locale];

            if (xml == null)
            {
                Debug.Log("LocaleManager: _locale \"" + value + "\" not found");
                return;
            }
            foreach (XmlNode node in xml.ChildNodes)
            {
                var tagName = node.Attributes["id"].Value;
                _data[tagName] = node.InnerText;
            }
        }
    }

    public void AddLocale(XmlElement xml)
    {
        _xmlDocs[xml.GetAttribute("name")] = xml;
    }

    public object GetString(string key)
    {
        if (_data.ContainsKey(key))
        {
            return _data[key];
        }
        return key; 
    }

    public string GetParamsString(string key, string[] args)
    {
        if (_data.ContainsKey(key))
        {
            return ReplaceStringValues(_data[key], args);  
        }
        return key;
    }

    public string ReplaceStringValues(string key, string[] args)
    {
        var len = args.Length;
        for (var i = 0; i < len; i++)
        {
            key = key.Replace("%" + (i+1), args[i]);
        }
        return key;
	}
}
