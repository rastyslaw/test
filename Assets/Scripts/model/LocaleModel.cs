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

        string pattern = @"%\d";
        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(key);

        if (len != matches.Count)
        {
            Debug.Log("LocaleModel: need more args in " + key);
            return key;
        }

        int index = 0;
        pattern = @"%\d{\w+}";
        regex = new Regex(pattern);

        var tagReqExp = new Regex(@"[A-Z_]+");

        foreach (Match match in matches)
        {
            if (key.Substring(match.Index + match.Length - 1, 1) == "{")
            {
                var numString = key.Substring(match.Index-1); 
                var result = regex.Match(numString);
                var localeTag = tagReqExp.Match(result.Value);
                key = key.Replace(result.Value, GameUtils.ReplaceTail(args[index], (string)GetString(localeTag.Value)));
            }
            else
            {
                key = key.Replace(match.Value, args[index]);
            }
            index++;
        }

        return key;
    }
}
